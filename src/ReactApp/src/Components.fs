namespace App

open Feliz
open Feliz.Router
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
    let createDummies () = [
        { Id = 1; Title = "Article About Cars"; Body = "Cars can move you around." }
        { Id = 2; Title = "Article About Guns"; Body = "Guns can remove you." }
        { Id = 3; Title = "Article About Drugs"; Body = "Drugs can move with you." }
    ] 

[<RequireQualifiedAccess>]
module Menu =
    type State = {
        SelectedArticleId : int option
        Articles: Article list
    }
    
    type Msg =
        | SelectArticle of ArticleId
        | DeselectArticle
    
    let init articles =
        {
            SelectedArticleId = None
            Articles = []
        },
        Cmd.none
    
    let update msg state =
        match msg with
        | SelectArticle articleId -> { state with SelectedArticleId = Some articleId }, Cmd.none
        | DeselectArticle -> { state with SelectedArticleId = None }, Cmd.none
    
    let private displayArticleName (article: Article) =
        Html.p [
            prop.text article.Title
        ]
    
    let Render articles =
        let state, dispatch = React.useElmish(articles |> init, update, [| |])
        
        Html.div [
            prop.classes [ "menu" ]
            prop.children [
                Html.h1 "Menu"
                state.Articles
                |> List.map displayArticleName
                |> React.fragment
            ]
        ]
        
        
[<RequireQualifiedAccess>]
module Content =
    let Render() =
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
        Articles: Article list
    }
    
    let Render() =
        let articles, setArticles = Article.createDummies() |> React.useState 
        
        Html.div [
            prop.classes [ "application" ]
            prop.children [
                Menu.Render articles
                Content.Render ()
            ]
        ]
        

// [<RequireQualifiedAccess>]
// module Router =
//     /// <summary>
//     /// A React component that uses Feliz.Router
//     /// to determine what to show based on the current URL
//     /// </summary>
//     [<ReactComponent>]
//     let Apply() =
//         let (currentUrl, updateUrl) = React.useState(Router.currentUrl())
//         React.router [
//             router.onUrlChanged updateUrl
//             router.children [
//                 match currentUrl with
//                 | [ ] -> Application.Render()
//                 // | [ "hello" ] -> Components.HelloWorld()
//                 // | [ "counter" ] -> Components.Counter()
//                 | otherwise -> Html.h1 "Not found"
//             ]
//         ]