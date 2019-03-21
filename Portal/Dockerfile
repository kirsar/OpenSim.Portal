FROM microsoft/dotnet:2.2-aspnetcore-runtime

COPY dist /app

COPY entrypoint.sh /app/entrypoint.sh

WORKDIR /app

EXPOSE 80/tcp

ENTRYPOINT ["dotnet", "OpenSim.Portal.dll"]

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh