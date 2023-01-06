[<RequireQualifiedAccess>]
module MenuComponent

open System
open Feliz
open Domain.Article
open Domain.Comment

[<ReactComponent>]    
let Render (state: ArticleState, onArticleSelect, onAddArticle, onAddComment) =
    // let articles, setArticles = React.useState(articles)
    
    let displayArticleName (article: Article) =
        [
        Html.p [
            prop.classes [
                state.SelectedArticleId
                |> Option.map (fun selectedArticleId -> if selectedArticleId = article.Id then "selected" else "")
                |> Option.defaultValue ""
            ]
            prop.text $"{article.Title} - {article.Comments.Length} comments"
            prop.onClick (fun _ -> article.Id |> onArticleSelect)
        ]
        Html.button [
            prop.text "Add Comment"
            prop.onClick (fun _ -> onAddComment article.Id (Guid.NewGuid() |> Comment.createDummy))
        ]
        ] |> React.fragment
        
    let btnAddArticle = Html.button [
        prop.text "Add Article"
        prop.onClick (fun _ -> onAddArticle())
    ]
    
    Html.div [
        prop.classes [ "menu" ]
        prop.children [
            Html.h1 "Menu"
            btnAddArticle
            state.Articles
            |> Array.map displayArticleName
            |> React.fragment
        ]
    ]