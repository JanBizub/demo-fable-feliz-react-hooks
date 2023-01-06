[<RequireQualifiedAccess>]
module ApplicationComponent
open System
open Elmish
open Feliz
open Feliz.UseElmish
open Domain

type ApplicationState = {
    ArticleState: ArticleState
    ErrorMessage: string option
}

type Msg =
    | SelectArticle of Guid
    | DeselectArticle
    | AddArticle
    | AddComment of ArticleId * Comment

let private init articles =
    { ArticleState = { Articles = articles; SelectedArticleId = None }; ErrorMessage = None },
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
    // todo: how about have 2 useElmish hooks and separate update functions? :-)
    let state, dispatch = React.useElmish(Article.createDummies() |> init, update , [|  |])
    let onArticleSelect articleId = articleId |> SelectArticle |> dispatch
    let onAddArticle () = AddArticle |> dispatch
    let onAddComment articleId commentId = (articleId, commentId) |> AddComment |> dispatch
    
    let sidebar (children : ReactElement list) = Html.div [
        prop.classes [ "application-sidebar" ]
        prop.children children
    ]
    
    let mainContent  (children : ReactElement list) = Html.div [
        prop.classes [ "application-content" ]
        prop.children children
    ]
    
    Html.div [
        prop.classes [ "application-main" ]
        prop.children [
            Html.div [
                Html.h1 "Parent child composition demo"
                Html.p [ prop.text $"Selected article id: {state.ArticleState.SelectedArticleId}" ]
            ]
            [ MenuComponent.Render (state.ArticleState, onArticleSelect, onAddArticle) ] |> sidebar
            [ ContentComponent.Render (state.ArticleState, onArticleSelect, onAddArticle, onAddComment) ] |> mainContent
        ]
    ]