#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app
COPY Portal.csproj ./
RUN dotnet restore
#COPY . ./

#WORKDIR "/src/Portal"
#RUN dotnet build "Portal.csproj" -c Release -o /app

#FROM build AS publish
#RUN dotnet publish "Portal.csproj" -c Release -o /app
COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "OpenSim.Portal.dll"]

EXPOSE 80/tcp
#RUN chmod +x ./entrypoint.sh
#CMD /bin/bash ./entrypoint.sh