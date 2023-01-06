module Main

open Feliz
open Browser.Dom
open Fable.Core.JsInterop

importSideEffects "./styles/global.scss"

// Windows command prompt-
// set NODE_OPTIONS=--openssl-legacy-provider
// Windows PowerShell-
// $env:NODE_OPTIONS = "--openssl-legacy-provider"


// todo: what is the difference between ReactDOM.createRoot and RectDOM.render
ReactDOM.createRoot(document.getElementById "feliz-app")
|> fun reactRoot -> Application.Render()
                    |> reactRoot.render 