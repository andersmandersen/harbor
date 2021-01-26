![Harbor - Easy Docker Containers](banner.png?version=1)

[![Version](https://img.shields.io/nuget/v/harbor)](https://www.nuget.org/packages/harbor/)

# Harbor
Harbor is Dotnet CLI tool that are meant to help you with handling development dependencies like databases and cache services.

Harbor spins up small docker containers for each enabled services

## Requirements

- MacOS, Linux, Windows
- [Dotnet installed](https://dotnet.microsoft.com/download)
- Docker installed (MacOS: [Docker for Mac](https://docs.docker.com/docker-for-mac/), Windows: [Docker for Windows](https://docs.docker.com/docker-for-windows/))

## Installation
Install harbor with dotnet tool by running: 

```bash
dotnet tool install --global harbor
```

Validate your install with the following command
```bash
harbor help
```

## Usage

### Enable service
Enables a giving service

```bash
harbor enable mysql
```

### Disable a service
Disable a giving service. And will delete all related data to the service. Note this action can't be reserved. If you whise to stop a service. See [Stop service]

```bash
harbor disable {container_id}
```

### Disable all services
Disable all services. And will delete all related data to the service. Note this action can't be reserved. If you whise to stop a service. See [Stop service]

```bash
harbor disable --all
```

### Stop a service
Stop a running service

```bash
harbor stop {container_id}
```

### Stop all services
Stops all running service

```bash
harbor stop --all
```

### Start a service
Start a existing service

```bash
harbor start {container_id}
```

### Start all services
Start all services

```bash
harbor start --all
```

### List enabled service
List all enabled services. Will provide you with the container id used for start/stopping a service

```bash
harbor list
```

### Logs from service
Enables you to show the logs from within the service

```bash
harbor log {container_id}
```

# How to Contribute

Feel free to contribute with pull requests, bug reports or enhancement suggestions. We love PR's

# Future plan

- Add additional services
