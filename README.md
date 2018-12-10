# GitHub-Companion
A GitHub companion written in c# for various platforms.

## At the moment
I am creating the wrapper for the API (note: the API v4 is online and uses a different format. Please be aware when extending the project).

## Branching, Pull Request, and Automated Builds

### Branching
Please do not code on the master branch. There is a check in place to prevent check-ins on the master. Branch off the master to extend or update the project(s). You can branch however you like. That is the idea behind git (at least to my understanding) is that the branching is left to the user, and not dictated by the server. 

Please keep in mind that there are others that may be branching from your branch. Please keep it legitable, and descriptive.

### Pull request validation
Currently there is a validation in place using Azure pipelines (continuous integration). The validation will build the project and run the xUnit tests). Please be aware that all validaiton will take a couple of minutes before showing it's status.

### Schedule builds
After there is a foothold on the projects, there will be two more pipelines in place - a scheduled build (depending on the frequency of the updates/additions), and a release pipeline that will release the project into the wild blue yonder.

## Contribute
### Setup
There will be some data files that XUnit is using to test the service methods. All sensitive data files will have an extension of cvsd (*.cvsd). There is an entry in the gotnore file to ignore these extensions. Please use *.cvsd extensions when dealing with sensitive data (usernames, passwords, etc...). 
