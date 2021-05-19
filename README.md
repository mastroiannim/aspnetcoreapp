# aspnetcoreapp

### installare git su alpine (se non è presente)
`apk add git`
### clonare il repo
`git clone https://github.com/mastroiannim/aspnetcoreapp`
### cambiare la dir corrente con quella del nuovo progetto appena clonato
`cd aspnetcoreapp`
## alternativa 1) eseguire l'app in un container
```
docker container run --name app1 --rm \
	-v ${PWD}:/app \
	-p 5000:5000 \
	-w /app \
	-it \
	mcr.microsoft.com/dotnet/sdk:5.0 \
	dotnet watch run
```
## alternativa 2) in alternativa eseguire due container per fare load balancing con nginx

### eseguire l'app in un primo container
```
docker container run --name app1 --rm \
	-v ${PWD}:/app \
	-w /app \
	-d \
	mcr.microsoft.com/dotnet/sdk:5.0 \
	dotnet watch run 
```

### eseguire l'app in un secondo container
```
docker container run --name app2 --rm \
	-v ${PWD}:/app \
	-w /app \
	-d \
	mcr.microsoft.com/dotnet/sdk:5.0 \
	dotnet watch run 
```

### verifichiamo che entrambe le web applications siano in esecuzione. 
`docker ps -a`

### configuriamo una rete per il load balancer
`docker network create load_balancing_network`

### colleghiamo il primo container alla rete
`docker network connect load_balancing_network app1`

### colleghiamo il secondo container alla rete
`docker network connect load_balancing_network app2`

### mandiamo in esecuzione il balancer e colleghiamolo alla rete
```
docker run --name nginx --net load_balancing_network \
	-v ${PWD}/nginx.conf:/etc/nginx/nginx.conf \
	-p 8080:80 \
	-P \
	-d \
	nginx 
```

### Now go to browser and navigate to http://localhost:8080


## Deploy su Heroku
### Prerequisiti:
#### account free su heroku.com
#### heroku-cli 

### Login su heroku 
`heroku login`

### Login su heroku container
`heroku container:login`

### Creare una nuova applicazione su heroku
`heroku apps:create aspnetcoreapp-mastroiannim` 

### Fare la build di una nuova immagine a partire dalla cartella corrente .
`docker build -t aspnetcoreapp-mastroiannim .` 

### Tag dell'immagine appena creata
`docker tag aspnetcoreapp-mastroiannim registry.heroku.com/aspnetcoreapp-mastroiannim/web`

### Push dell'immagine sul registry di heroku (aspnetcoreapp-mastroiannim è il nome dell'app heroku)
`docker push registry.heroku.com/aspnetcoreapp-mastroiannim/web`

### Completiamo il deploy con il release del container su Heroku
`heroku container:release web -a aspnetcoreapp-mastroiannim`

### Now go to browser and navigate to https://aspnetcoreapp-mastroiannim.herokuapp.com






