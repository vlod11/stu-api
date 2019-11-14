## 1.Info about
### Prerequisites
Sample ASP.NET Core reference application, made by Vlad Odinets, demonstrating a single-process (monolithic) application architecture and deployment model.

## 2.Deploy project
#### 2.1 Docker installation
Install docker desktop from [Install docker](https://docs.docker.com/docker-for-windows/install/)
Check installation:

> docker -v

Install Docker Compose

#### 2.3 Deploy in docker
go to __.../unihub-api-example__

Execute
> docker-compose build
> docker-compose up

Open browser and go to http://localhost/swagger/index.html 
Also go to http://localhost/helloWorld to check __nginx__











