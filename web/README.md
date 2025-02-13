
# ETH testbed
Version: 2.0.0

## How to run everything in production
Step 1: install all the dependencies and build the `client` app.
```
npm run setup
```
Step 2: run the server
```
npm run server
```
That should be it.

# Misc commands
## How to install both dependencies
Run the following command to install both the `client` and the `server` projects.
```
npm run install
```
Don't forget the `run` part in this one.


## How to build the client for development
Live reload applies only to the `client` part of the project, however by running this command also a the `server` project is started
```
npm run start
```


## How to build the client for production
The command will insert a compiled `client` app and put it into the `server/client_dist` folder.
```
npm run build
```


## How to run only the server app
The command will expose the app at the port `3000`.
```
npm run server
```
