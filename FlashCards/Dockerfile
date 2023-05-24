FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /FlashCards
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "FlashCards.dll", "--urls=http://0.0.0.0:5000"]