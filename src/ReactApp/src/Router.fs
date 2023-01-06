[<RequireQualifiedAccess>]
module Router

open Feliz
open Feliz.Router

[<ReactComponent>]
let Apply() =
    let currentUrl, updateUrl = React.useState(Router.currentUrl())
    React.router [
        router.onUrlChanged updateUrl
        router.children [
            match currentUrl with
            | [ ] -> ApplicationComponent.Render()
            | [ "test" ] -> Html.h1 "test"
            | [ "users"; Route.Int userId ] -> Html.h1 (sprintf "User ID %d" userId)
            | otherwise -> Html.h1 "Not found"
        ]
    ]