[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/024bd0606c3218105991)

![Azure Deployment](https://github.com/gauravgupta98/socialize-api/workflows/Azure%20Deployment/badge.svg?branch=master)
![Code Analysis](https://github.com/gauravgupta98/socialize-api/workflows/Code%20Analysis/badge.svg?branch=master)

# Socialize API

### GraphQL API build on .NET 5 using C#, HotChocolate, Entity Framework and SQL Server.
Click on above `Run in Postman` button to test and play with the API.

## API URL - https://socialize.azurewebsites.net/graphql/

### Queries
* Get all users -
```graphql
query {
    user {
        id
        name
        posts {
            postData
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
        user {
            id
            name
        }
    }
}
```

### Mutations
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
        }
    }
}
```
* Create new post -
```graphql
mutation {
    createPost (postData: {
        postData: "Test Post - Say Hello to GraphQL"
        userId: "d45661c3d36f492ccfa108d8d72073af"
    })
    {
        post {
            id
            postData
            user {
                name
            }
        }
    }
}
```
