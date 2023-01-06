namespace Domain.Messenger

open System

type MessageId = Guid
type Message = {
   Id: MessageId
   From: string
   Body: string
   }

[<RequireQualifiedAccess>]
module Message =
    let createDummies () = [|
        { Id = Guid.NewGuid(); From = "Krishnu"; Body = "Hare Hare" }
        { Id = Guid.NewGuid(); From = "Shiva"; Body = "Now I am become Death, the destroyer of worlds" }
    |]
    
    let createDummy messageId =
        { Id = messageId; From = "Gopnik"; Body = "You have been gopstopped." }
        
  