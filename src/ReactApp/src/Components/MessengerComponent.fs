[<RequireQualifiedAccess>]
module MessengerComponent

open System
open Feliz
open Elmish
open Domain.Messenger

type MessengerMsg =
    | ReceiveMessages
    | DeleteMessages
    | AddMessage
    
let messengerUpdate msg (state: MessengerState) =
    match msg with
    | ReceiveMessages ->
        { state with Messages = Message.createDummies() },
        Cmd.none
        
    | DeleteMessages ->
        { state with Messages = [||] },
        Cmd.none
        
    | AddMessage ->
        { state with Messages = state.Messages |> Array.append [| Message.createDummy (Guid.NewGuid()) |]},
        Cmd.none

[<ReactComponent>]
let Render (state: MessengerState, onMessagesReceive, onMessageAdd, onMessagesRemove) =
    [
        Html.h1 "Messages"
        
        Html.button [
            prop.text "Receive"
            prop.onClick (fun _ -> onMessagesReceive ())
        ]
        Html.button [
            prop.text "Add"
            prop.onClick (fun _ -> onMessageAdd ())
        ]
        Html.button [
            prop.text "Remove"
            prop.onClick (fun _ -> onMessagesRemove ())
        ]
        Html.hr []
        state.Messages
        |> Array.map (fun message ->
            Html.div [
                prop.classes [ "messenger-message" ]
                prop.children [
                    Html.p [ prop.text $"Message from {message.From}: {message.Body}" ]
                ]
            ]
        )
        |> React.fragment
    ]
    |> Html.div

