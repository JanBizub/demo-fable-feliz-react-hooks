namespace App

open Feliz
open Feliz.Router
open Feliz.UseElmish

[<RequireQualifiedAccess>]
module Menu =
    let Render() =
        Html.div [
            prop.classes [ "menu" ]
            prop.children [
                Html.h1 "Menu"
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
    let Render() =
        Html.div [
            prop.classes [ "application" ]
            prop.children [
                Menu.Render ()
                Content.Render ()
            ]
        ]
        

[<RequireQualifiedAccess>]
module Router =
    /// <summary>
    /// A React component that uses Feliz.Router
    /// to determine what to show based on the current URL
    /// </summary>
    [<ReactComponent>]
    let Apply() =
        let (currentUrl, updateUrl) = React.useState(Router.currentUrl())
        React.router [
            router.onUrlChanged updateUrl
            router.children [
                match currentUrl with
                | [ ] -> Application.Render()
                // | [ "hello" ] -> Components.HelloWorld()
                // | [ "counter" ] -> Components.Counter()
                | otherwise -> Html.h1 "Not found"
            ]
        ]