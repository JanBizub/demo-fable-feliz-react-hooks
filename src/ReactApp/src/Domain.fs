namespace Domain

type ArticleId = int

type Article = {
    Id    : ArticleId
    Title : string
    Body  : string
    }

type ArticleState = {
    SelectedArticleId : int option
    Articles: Article array
    }

[<RequireQualifiedAccess>]
module Article =
    let createDummies () : Article array= [|
        { Id = 1; Title = "Article About Cars"; Body = "Cars can move you around." }
        { Id = 2; Title = "Article About Guns"; Body = "Guns can remove you." }
        { Id = 3; Title = "Article About Drugs"; Body = "Drugs can move with you." }
    |]
    
    let addDummyArticle articleId =
        { Id = articleId; Title = "Article About Gardening"; Body = "Gardening is about gardening." }


        
        
