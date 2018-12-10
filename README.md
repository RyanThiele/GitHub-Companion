# GitHub-Companion
A GitHub companion written in c# for various platforms.

## At the moment
I am creating the wrapper for the API (note: the API v4 is online and uses a different format. Please be aware when extensing the project).

### Pull request validation
Currently there is a validation in place using Azure pipelines (continuous integration). The validation will build the project and run the xUnit tests). Please be aware that all validaiton will take a couple of minutes before showing it's status.

### Schedule builds
Aftere there is a foothold on the projects, there will be two morepipelines in place - a sceduled build (depending on the frequency of the updates/additions), and a release pipeline that will release the project into the wild blu yonder.

Setup:
There will be some data files that XUnit is using to test the service methods. All sensitive data files will have an extension of cvsd (*.cvsd). There is an entry in the gotnore file to ignore these extensions. Please use *.cvsd extensions when dealing with sensitive data (usernames, passwords, etc...). 
