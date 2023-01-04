namespace App

open Fable.Core
open Feliz
open Feliz.UseElmish
open Elmish

type ArticleId = int

type Article = {
    Id    : ArticleId
    Title : string
    Body  : string
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
    type State = {
        SelectedArticleId : int option
        Articles: Article array
    }
    
    type Msg =
        | SelectArticle of ArticleId
        | DeselectArticle
    
    let init articles =
        {
            SelectedArticleId = None
            Articles = [||]
        },
        Cmd.none
    
    let update msg state =
        match msg with
        | SelectArticle articleId ->
            { state with SelectedArticleId = Some articleId }, Cmd.none
            
        | DeselectArticle ->
            { state with SelectedArticleId = None }, Cmd.none
    
    [<ReactComponent>]
    let displayArticleName (article: Article) =
        Html.p [ prop.text article.Title ]

    [<ReactComponent>]    
    let Render (articles: Article array) =
        let state, dispatch = React.useElmish(articles |> init, update, [| |])
        // let articles, setArticles = React.useState(articles)
        
        Html.div [
            prop.classes [ "menu" ]
            prop.children [
                Html.h1 "Menu"
                
                articles
                |> Array.map displayArticleName
                |> React.fragment
            ]
        ]
        
        
[<RequireQualifiedAccess>]
module Content =
    [<ReactComponent>]
    let Render () =
        Html.div [
            prop.classes [ "content" ]
            prop.children [
                Html.h1 "Content"
            ]
        ]
        
        
[<RequireQualifiedAccess>]
module Application =
    type State = {
        SelectedArticleId : int
        Articles: Article array
    }
    
    [<ReactComponent>]
    let Render () =
        let (articles, setState) = React.useState(Article.createDummies())
        
        Html.div [
            prop.classes [ "application" ]
            prop.children [
                Menu.Render articles
                Content.Render ()
            ]
        ]