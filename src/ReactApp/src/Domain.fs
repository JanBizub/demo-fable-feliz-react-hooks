namespace Domain

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

type ArticleId = Guid
type Article = {
    Id    : ArticleId
    Title : string
    Body  : string
    Comments: Comment array
    }

[<RequireQualifiedAccess>]
module Article =
    let createDummies () : Article array= [|
        { Id = Guid.NewGuid(); Title = "Article About Cars"; Body = "Cars can move you around."; Comments = Comment.createDummies() }
        { Id = Guid.NewGuid(); Title = "Article About Guns"; Body = "Guns can remove you."; Comments = [||] }
        { Id = Guid.NewGuid(); Title = "Article About Drugs"; Body = "Drugs can move with you."; Comments = Comment.createDummies() }
    |]
    
    let addDummyArticle articleId =
        { Id = articleId; Title = "Article About Gardening"; Body = "Gardening is about gardening."; Comments = [||] }

type ArticleState = {
    SelectedArticleId : ArticleId option
    Articles: Article array
    }

type MessengerState = {
    SelectedMessageId : MessageId option
    Messages: Message array
    }        
        
