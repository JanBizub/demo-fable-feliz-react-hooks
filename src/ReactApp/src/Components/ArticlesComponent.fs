[<RequireQualifiedAccess>]
module ArticlesComponent

open System
open Feliz
open Domain.Article
open Domain.Comment

type State = {
    SelectedArticleId : ArticleId option
    Articles: Article array
    }

[<ReactComponent>]    
let RenderMenu (state: State, onArticleSelect, onAddArticle, onAddComment) =
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

[<ReactComponent>]
let RenderArticles (state: State, onArticleSelect, onAddArticle, onAddComment) =
    // let articles, setArticles = React.useState(articles)

    let displayArticleName (article: Article) =
        React.fragment [
            Html.h4 [
                prop.classes [
                    state.SelectedArticleId
                    |> Option.map (fun selectedArticleId ->
                        if selectedArticleId = article.Id then "selected" else ""
                    )
                    |> Option.defaultValue ""
                ]
                prop.text article.Title
                prop.onClick (fun _ -> article.Id |> onArticleSelect)
            ]
            Html.p [ prop.text article.Body ]
            
            match article.Comments with
            | [|  |] ->
                Html.button [
                    prop.text "Add Comment"
                    prop.onClick (fun _ -> onAddComment article.Id (Guid.NewGuid() |> Comment.createDummy))
                ]
            | comments ->
                [
                    Html.p [ prop.text $"Article has: {comments.Length} comments." ]
                    Html.button [ prop.text "Open Comments" ]
                    Html.button [
                        prop.text "Add Comment"
                        prop.onClick (fun _ -> onAddComment article.Id (Guid.NewGuid() |> Comment.createDummy))
                    ]
                ]
                |> React.fragment
                
            Html.hr []
        ]
        
    let btnAddArticle =
        Html.button [
            prop.text "Add Article"
            prop.onClick (fun _ -> onAddArticle())
        ]
    
    Html.div [
        prop.classes [ "content" ]
        prop.children [
            Html.h1 "Content"
            btnAddArticle
            state.Articles
            |> Array.map displayArticleName
            |> React.fragment
        ]
    ]
