module Main

open Feliz
open App
open Browser.Dom
open Fable.Core.JsInterop

importSideEffects "./styles/global.scss"

ReactDOM.createRoot(document.getElementById "feliz-app")
|> fun reactRoot -> Components.HelloWorld()
                    |> reactRoot.render 