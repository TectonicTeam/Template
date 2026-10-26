FROM rgs.entekhabgroup.com:5008/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM rgs.entekhabgroup.com:5008/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["nuget.config", "."]
COPY ["src", "."]


ARG Environment
ARG PRODUCT_VERSION=1.0.0
#ARG Db_HOST
ARG ELASTIC_ADDRESS
ARG ELASTIC_USER
ARG ELASTIC_PASS

RUN dotnet restore "DS.FleetManagement.Api/DS.FleetManagement.Api.csproj" 
COPY . .
WORKDIR "/src/DS.Requests.Api"
#RUN dotnet build "DekaNotification.Api.csproj" -c Release  -p:Version=$PRODUCT_VERSION -o /app/build
#RUN dotnet build "DekaNotification.Api.csproj" -c Release -p:Db_host=Database   -o /app/build
RUN dotnet build "DS.FleetManagement.Api.csproj" -c Release  -o /app/build



FROM build AS publish
#RUN dotnet publish "DekaNotification.Api.csproj" -c Release  -p:Version=$PRODUCT_VERSION -o /app/publish /p:UseAppHost=false
#RUN dotnet publish "DekaNotification.Api.csproj" -c Release  -p:Db_host=Database  -o /app/publish /p:UseAppHost=false
RUN dotnet publish "DS.FleetManagement.Api.csproj" -c Release  -o /app/publish /p:UseAppHost=false


ENV ASPNETCORE_ENVIRONMENT=${Environment}
ENV ELASTIC_ADDRESS=${ELASTIC_ADDRESS}
ENV ELASTIC_USER=${ELASTIC_USER}
ENV ELASTIC_PASS=${ELASTIC_PASS}

RUN echo "ASPNETCORE_ENVIRONMENT Is: ${Environment}"
#ENV PATH="${PATH}:/root/.dotnet/tools"
#RUN dotnet tool install --global dotnet-ef --version 7.0.14
#RUN dotnet tool restore
#RUN dotnet ef database update --context ApplicationDbContext

FROM base AS final
WORKDIR /app

RUN apt-get update && apt-get install -y libgssapi-krb5-2


COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DS.FleetManagement.Api.dll"]
