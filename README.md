## 1.Getting Started
### Prerequisites
* [Node.js](https://nodejs.org/en/) (>= 12.4.0)
* npm (>= 6.4.1) (before installing npm you need to install console git)
### 1.2 Start client
Copy unihub repo to your computer:
> git clone https://github.com/vlod11/unihub.git

Open client folder "../unihub/UniHub.Client/Client" and execute:
> npm install

Start client:
> npm run start

### 1.3 Start server
will be soon
__P.S.__ API was build using .NET Core 2.2

## 2.Docker
#### 2.1 Docker installation
Install docker desktop from [Install docker](https://docs.docker.com/docker-for-windows/install/)
Check installation:

> docker -v

#### 2.2 Usefull docker commands
| Command | Description |
|---------|-------------|
|docker ps| показать все запущенные контейнеры|
|docker images|показать все образы|
|docker rmi $(docker images -f “dangling=true" -q)|удаляет все образы с тегом <none>|
|docker stop $(docker ps -aq)|остановить все запущенные контейнеры|
|docker rm $(docker ps -a -q)|удалить все контейнеры |
|docker rmi $(docker images -q)|удалить все образы |
#### 2.3 Deploy in docker
go to __.../unihub/Deploy__
run __build-and-run-images.cmd__
Open browser and go to http://localhost/swagger/index.html 
Also go to http://localhost/helloWorld to check __nginx__
#### 2.4 Portainer
For better user expiriance you should use portainer. Open PowerShell and execute next comand:
> docker run -d -p 9000:9000 --name portainer --restart always -v /var/run/docker.sock:/var/run/docker.sock -v C:\ProgramData\Portainer:/data portainer/portainer

Or you should go to __./unihub/Deploy/docker-scripts__ 
and run __start-portainer.cmd__
Then open browser and go to localhost:9000
### 2.5 Adminer
It is possibe that you might want to check data in database without server or client usage. If so you can run unihub in docker (see __1.3 Deploy__) and go to __Adminer__ page (http://localhost:8080). Just select PostgesDb in combobox and specify username/password 










