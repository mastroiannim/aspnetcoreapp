# aspnetcoreapp

git clone https://github.com/mastroiannim/aspnetcoreapp

cd aspnetcoreapp

docker container run --name app1 --rm \
	-v ${PWD}:/app \
	-v ${PWD}/Https:/root/.aspnet/https/ \
	-v ${PWD}/DataProtection-Keys:/root/.aspnet/DataProtection-Keys \
	-p 5001:5001 \
	-p 5000:5000 \
	-w /app \
	-it \
	mcr.microsoft.com/dotnet/sdk:5.0 \
	dotnet watch run \
