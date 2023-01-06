﻿[<RequireQualifiedAccess>]
module Menu 
open Feliz
open Domain

type MenuState = ArticleState

[<ReactComponent>]    
let Render (state: MenuState, onArticleSelect, onAddArticle) =
    // let articles, setArticles = React.useState(articles)
    
    let displayArticleName (article: Article) =
        Html.p [
            prop.classes [
                state.SelectedArticleId
                |> Option.map (fun selectedArticleId -> if selectedArticleId = article.Id then "selected" else "")
                |> Option.defaultValue ""
            ]
            prop.text article.Title
            prop.onClick (fun _ -> article.Id |> onArticleSelect)
        ]
        
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