[<RequireQualifiedAccess>]
module CommentComponent
open Feliz
open Domain.Comment

[<ReactComponent>]
let Render () =
    // let articles, setArticles = React.useState(articles)

    Html.h1 "Comment Component"

