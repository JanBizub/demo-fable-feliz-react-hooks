[<RequireQualifiedAccess>]
module MessengerComponent

open System
open Feliz
open Elmish
open Domain.Messenger

type State = {
    SelectedMessageId : MessageId option
    Messages: Message array
    }   

type MessengerMsg =
    | ReceiveMessages
    | DeleteMessages
    | AddMessage
    
let messengerUpdate msg (state: State) =
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
let Render (state: State, onMessagesReceive, onMessageAdd, onMessagesRemove) =
    Html.div [
        prop.classes [ "messenger" ]
        prop.children [
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
    ]
    
