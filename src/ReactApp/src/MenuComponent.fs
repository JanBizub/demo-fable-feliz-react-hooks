[<RequireQualifiedAccess>]
module Content
open Feliz
open Domain
type ContentState = ArticleState

[<ReactComponent>]
let Render (state: ContentState, onArticleSelect, onAddArticle) =
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
