namespace App

open System
open Fable.Core
open Feliz
open Feliz.UseElmish
open Elmish

// https://beta.reactjs.org/learn/managing-state

type ArticleId = int

type Article = {
    Id    : ArticleId
    Title : string
    Body  : string
    }

type ArticleState = {
    SelectedArticleId : int option
    Articles: Article array
    }

[<RequireQualifiedAccess>]
module Article =
    let createDummies () : Article array= [|
        { Id = 1; Title = "Article About Cars"; Body = "Cars can move you around." }
        { Id = 2; Title = "Article About Guns"; Body = "Guns can remove you." }
        { Id = 3; Title = "Article About Drugs"; Body = "Drugs can move with you." }
    |]

[<RequireQualifiedAccess>]
module Menu =
    type MenuState = ArticleState
    
    [<ReactComponent>]    
    let Render (state: MenuState, onArticleSelect) =
        // let articles, setArticles = React.useState(articles)
        
        let displayArticleName (article: Article) =
            Html.p [
                prop.classes [ state.SelectedArticleId
                               |> Option.map (fun selectedArticleId ->
                                   if selectedArticleId = article.Id then "selected" else "")
                               |> Option.defaultValue "" ]
                prop.text article.Title
                prop.onClick (fun _ -> article.Id |> onArticleSelect)
            ]
        
        Html.div [
            prop.classes [ "menu" ]
            prop.children [
                Html.h1 "Menu"
                
                state.Articles
                |> Array.map displayArticleName
                |> React.fragment
            ]
        ]
        
        
[<RequireQualifiedAccess>]
module Content =
    type ContentState = ArticleState
    
    [<ReactComponent>]
    let Render (state: ContentState, onArticleSelect) =
        // let articles, setArticles = React.useState(articles)

        let displayArticleName (article: Article) =
            Html.p [
                prop.classes [ state.SelectedArticleId
                               |> Option.map (fun selectedArticleId ->
                                   if selectedArticleId = article.Id then "selected" else "")
                               |> Option.defaultValue "" ]
                prop.text article.Title
                prop.onClick (fun _ -> article.Id |> onArticleSelect)
            ]
        
        Html.div [
            prop.classes [ "content" ]
            prop.children [
                Html.h1 "Content"
                
                state.Articles
                |> Array.map displayArticleName
                |> React.fragment
            ]
        ]
        
        
[<RequireQualifiedAccess>]
module Application =
    type ApplicationState = {
        ArticleState: ArticleState
    }
    
    type Msg =
        | SelectArticle of ArticleId
        | DeselectArticle
        // | AddArticle
    
    let init articles =
        {
            ArticleState = { Articles = articles; SelectedArticleId = None }
        },
        Cmd.none
    
    let update msg (state: ApplicationState) =
        match msg with
        | SelectArticle articleId ->
            { state with ArticleState = { state.ArticleState with SelectedArticleId = Some articleId } },
            Cmd.none
            
        | DeselectArticle ->
            { state with ArticleState = { state.ArticleState with SelectedArticleId = None } },
            Cmd.none
            
        // | AddArticle ->
        //     let id =
        //         state.ArticleState.Articles
        //         |> Array.map ( fun article -> article.Id)
        //         |> Array.max
        //         |> fun max -> max + 1
        //     
        //     let newArticle = { Id = id; Title = "Added article"; Body = "Added article" }
        //     
        //     { state with ArticleState =  { state.ArticleState with SelectedArticleId = None } },
        //     Cmd.none
    
    [<ReactComponent>]
    let Render () =
        let (state, dispatch) = React.useElmish(Article.createDummies() |> init, update , [|  |])
        let onArticleSelect articleId = articleId |> SelectArticle |> dispatch
        // let onArticleCreate = AddArticle |> dispatch
        
        Html.div [
            prop.classes [ "application" ]
            prop.children [
                Html.p [ prop.text $"Selected article id: {state.ArticleState.SelectedArticleId}" ]
                Menu.Render (state.ArticleState, onArticleSelect)
                Content.Render (state.ArticleState, onArticleSelect)
            ]
        ]