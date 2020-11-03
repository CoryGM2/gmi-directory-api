FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder
COPY . /tmp
WORKDIR /tmp
RUN dotnet publish DirectoryApi/Server/DirectoryApi.Server.csproj -c Release -o output

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine

WORKDIR /app
EXPOSE 8080

COPY --from=builder /tmp/output/ .

ENTRYPOINT ["dotnet", "DirectoryApi.Server.dll"]

