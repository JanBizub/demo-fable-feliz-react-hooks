module Main

open Feliz
open App
open Browser.Dom
open Fable.Core.JsInterop

importSideEffects "./styles/global.scss"

// todo: what is the difference between ReactDOM.createRoot and RectDOM.render
ReactDOM.createRoot(document.getElementById "feliz-app")
|> fun reactRoot -> Application.Render()
                    |> reactRoot.render 