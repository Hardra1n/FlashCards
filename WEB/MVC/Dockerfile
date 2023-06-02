FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /FlashCardsUIMVC
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "ViewUI.dll", "--urls=http://0.0.0.0:5001"]