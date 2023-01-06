namespace Domain.Comment

open System

type CommentId = Guid
type Comment = {
   Id: CommentId
   Body: string
   }

[<RequireQualifiedAccess>]
module Comment =
    let createDummies () = [|
        { Id = Guid.NewGuid(); Body = "Rude comment about article" }
        { Id = Guid.NewGuid(); Body = "Polite comment about article" }
        { Id = Guid.NewGuid(); Body = "Praising comment about article" }
    |]
    
    let createDummy commentId =
        { Id = commentId; Body = "Created Comment Body" }


