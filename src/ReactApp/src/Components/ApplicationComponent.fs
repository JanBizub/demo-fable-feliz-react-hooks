[<RequireQualifiedAccess>]
module ApplicationComponent
open System
open Elmish
open Feliz
open Feliz.UseElmish
open Domain.Article
open Domain.Messenger
open Domain.Comment

type ApplicationState = {
    ArticleState: ArticleState
    MessengerState: MessengerState
    ErrorMessage: string option
    }

type Msg =
    | SelectArticle of Guid
    | DeselectArticle
    | AddArticle
    | AddComment of ArticleId * Comment
    
type MessengerMsg = MessengerComponent.MessengerMsg

let private init articles =
    { ArticleState = { Articles = articles; SelectedArticleId = None }
      MessengerState = { SelectedMessageId = None; Messages = [||] }
      ErrorMessage = None },
    Cmd.none

let private update msg (state: ApplicationState) =
    match msg with
    | SelectArticle articleId ->
        { state with ArticleState = { state.ArticleState with SelectedArticleId = Some articleId } },
        Cmd.none
        
    | DeselectArticle ->
        { state with ArticleState = { state.ArticleState with SelectedArticleId = None } },
        Cmd.none
        
    | AddArticle ->
        let newArticle = Guid.NewGuid() |> Article.addDummyArticle
        { state with
           ArticleState = {
            state.ArticleState with
                Articles = state.ArticleState.Articles |> Array.append [| newArticle |] } },
        Cmd.none
        
    | AddComment (articleId, comment) ->
        let updatedState =
            state.ArticleState.Articles
            |> Array.tryFind (fun article -> article.Id = articleId)
            |> Option.map (fun article -> { article with Comments = (article.Comments |> Array.append [| comment |]) })
            |> Option.map (fun updatedArticle ->
                state.ArticleState.Articles
                |> Array.map (fun article ->
                    if article.Id = articleId then updatedArticle else article)
                |> fun updatedArticles ->
                    { state with ArticleState = { state.ArticleState with Articles = updatedArticles } }
            )
            |> Option.defaultValue
                { state with ErrorMessage = Some "Unable to add comment to article. Target Article has not been found."}
        updatedState,
        Cmd.none
        

[<ReactComponent>]
let Render () =
    // https://zaid-ajaj.github.io/Feliz/#/Hooks/UseElmish
    let state, dispatch =
        React.useElmish(Article.createDummies() |> init, update, [|  |])
    let onArticleSelect articleId = articleId |> SelectArticle |> dispatch
    let onAddArticle () = AddArticle |> dispatch
    let onAddComment articleId commentId = (articleId, commentId) |> AddComment |> dispatch

    // lets have two useElmishes :D
    let messengerState, messengerDispatch =
        React.useElmish((state.MessengerState,Cmd.none), MessengerComponent.messengerUpdate, [|  |])
    let onMessagesReceive () = MessengerMsg.ReceiveMessages |> messengerDispatch
    let onMessageAdd () = MessengerMsg.AddMessage |> messengerDispatch
    let onMessagesRemove () = MessengerMsg.DeleteMessages |> messengerDispatch
        
    let sidebar (children : ReactElement list) = Html.div [
        prop.classes [ "application-sidebar" ]
        prop.children children
    ]
    
    let mainContent  (children : ReactElement list) = Html.div [
        prop.classes [ "application-content" ]
        prop.children children
    ]
    
    let applicationMessenger (children : ReactElement list) = Html.div [
        prop.classes [ "application-messenger" ]
        prop.children children
    ]
    
    let messengerComponentView =
        MessengerComponent.Render(
            messengerState,
            onMessagesReceive,
            onMessageAdd,
            onMessagesRemove
        )
    
    let menuComponentView =
        MenuComponent.Render(
            state.ArticleState,
            onArticleSelect,
            onAddArticle,
            onAddComment
        )

    let contentComponentView =
        ContentComponent.Render(
            state.ArticleState,
            onArticleSelect,
            onAddArticle,
            onAddComment
        )
        
    Html.div [
        prop.classes [ "application-main" ]
        prop.children [
            [ menuComponentView ] |> sidebar
            [ contentComponentView ] |> mainContent
            [ messengerComponentView ] |> applicationMessenger
        ]
    ]
    |> fun appContent ->
        Html.div [
            prop.children [
                Html.div [
                    prop.classes [ "application-header" ]
                    prop.children [
                        Html.h1 "Parent child composition demo using react hooks"
                        let totalComments =
                            state.ArticleState.Articles
                            |> Array.map (fun article -> article.Comments.Length)
                            |> Array.reduce (+)
                               
                        Html.p [ prop.text
                                   $"Selected article id: {state.ArticleState.SelectedArticleId}.
                                     | Received Messages count: {messengerState.Messages.Length}.
                                     | Total comments to articles: {totalComments}." ]
                    ]
                ]
                appContent
            ]
        ]