# GitHub-Companion
[![Pull Request Status](https://dynamensions.visualstudio.com/GitHub%20Companion/_apis/build/status/GitHub%20Companion-Universal%20Windows%20Platform-CI)](https://dynamensions.visualstudio.com/GitHub%20Companion/_build/latest?definitionId=-1)

A GitHub companion written in c# for various platforms.

Pull-Reqest Validation:
[![Pull Request Status](https://dynamensions.visualstudio.com/GitHub%20Companion/_apis/build/status/GitHub%20Companion-Universal%20Windows%20Platform-CI)](https://dynamensions.visualstudio.com/GitHub%20Companion/_build/latest?definitionId=18)

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
=======
## Contributing
### Branching
Currently there is a rule to protect the master branch. All changes and bug fixes must go through a pull request process. So be sure to branch from the master before pushing changes to the repository.

### Pull Requests
When creating a pull request, an Azure Pipeline will run checks against the branch and see if the branch ios valid. This may take several minutes. Please be patient. One the checks have been made, the status of the pull request will be updated. 

