module Main

open Feliz
open Browser.Dom
open Fable.Core.JsInterop
open Feliz.Router

importSideEffects "./styles/global.scss"

// Windows command prompt-
// set NODE_OPTIONS=--openssl-legacy-provider
// Windows PowerShell-
// $env:NODE_OPTIONS = "--openssl-legacy-provider"

ReactDOM.createRoot(document.getElementById "feliz-app")
|> fun reactRoot ->
    Router.Apply()
    |> reactRoot.render