namespace Domain

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
    SelectedArticleId : Guid option
    Articles: Article array
    }
        
        
