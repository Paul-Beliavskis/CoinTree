# Overview

Seeing as this problem was a fairly simple problem, I have decided to just create two projects. One for the Api and one for the Tests.
The Api project will also be used to serve up a ReactJS application that will call Api endpints on the Api project. 

# Api project

## Folder structure

### API
This folder contains all logic for handling user requests<br>

**wwwroot** - contains all the built ReactJS files ready to be served up to the client(These files will typically be build and moved to this folder during the deployment/build pipeline).

**Controllers** - The API endpoints for serving data.

**Middleware** - Holds all middleware compnents. Currently we have two middleware components, first the exception handling middleware is used so we can then have all the error handling consistant for the service such as the error models and codes that are returned to the user/calling application. Now we don't need try catch blocks everywhere for common errors. The second middleware component is the RouteHandlerMiddleware which is used identify if the request is for the default ReactJS page. This helps avoid errors such as misspelling the static resource path.

**ViewModels** - Holds data models that are designed specifically to be sent to the UI to avoid

### Application
This folder is home to all the application logic such as ochestrating calls to the Infrastructure or Persistence layer (which I didn't include seeing as it is a small app so no need to split out into more layers than required). Intefaces that are implemented by lower layers belong here too.

**Dtos** - Hold data that can come from and layers below and can be sent to layers above. Such data includes data returned from an Api. The application layer may also choose to trim the data it makes available to the layer above (api layer). 

**Exceptions** - Custom exceptions that can be thrown and caught at a upper layer.

**Interfaces** - Normally would hold intefaces that are implemented by the Infrastructure layer (adhearing to clean architecture) which would make it easy to swap

### UI
The source reactJS files where changes can be made to the files served up to the frontend. It is only the built files that get transfered wwwroot folder in the API folder.

## Exception handling
Exceptions can be thrown from anywhere using custom exceptions and handling them in the exception middleware pipeline. If an unhandled exception occurs (which let's hope it doesn't) then the worst case happens which a 500 is returned, error is logged and a standard error model is returned nice and clean.

## Final thoughts

If the project was bigger I would consider breaking it down into Api, Application, Infrastructure, Domain and persistance layers. This would ensure it is easier to test and maintain.

# Test project

This project has all the code to test the Api project.

I used FakeItEasy as the mocking framework because it is much simpler to use than Moq. 
To test the HttpClient there are a few options. One is to use a wrapper around the client and mock the wrappers interface which would mean having an extra class in the project just for mocking (can be a bit confusing). Second is to create a class inheriting from the HttpMessageHandler which overrides what gets returned by the HttpContext. So each time you need a HttpClient in your test just need to create a mocked response and you're golden. 