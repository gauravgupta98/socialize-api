![Azure Deployment](https://github.com/gauravgupta98/socialize-api/workflows/Azure%20Deployment/badge.svg?branch=master)
![Code Analysis](https://github.com/gauravgupta98/socialize-api/workflows/Code%20Analysis/badge.svg?branch=master)

# Socialize API

### GraphQL API build on .NET 5 using C#, HotChocolate, Entity Framework and SQL Server.

***Important - Except `login` and `createUser` mutations, all GraphQL requests need Authorization header. To get the authorization token, run `login` mutation.***

### Queries
* Get all users -
```graphql
query {
    user {
        id
        name
        createdTime
        posts {
            postData
            createdTime
        }
    }
}
```
* Get all posts -
```graphql
query {
    posts {
        id
        postData
        createdTime
        user {
            id
            name
        }
    }
}
```

### Mutations
* Login -
```graphql
mutation {
    login (credentials: {
        email: "gg@gmail.com"
        password: "P@ssw0rd"
    })
    {
        token
    }
}
```
* Create new user -
```graphql
mutation {
    createUser (userInput: {
        name: "Test User"
        email: "test@email.com"
        password: "P@ssw0rd"
    })
    {
        user {
            id
            name
            createdTime
        }
    }
}
```
* Create new post -
```graphql
mutation {
    createPost (postData: {
        postData: "Yayyyyy! Hello GraphQL."
        userId: "d10cac6672994ba4eb0b08d8db1413c5"
    })
    {
        post {
            id
            postData
            createdTime
            user {
                name
            }
        }
    }
}
```
