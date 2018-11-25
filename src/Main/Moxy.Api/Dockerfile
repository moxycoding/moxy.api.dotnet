

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /
COPY . .
RUN cd /src/Main/Moxy.Api/ \
	&& dotnet restore \
	&& dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "Moxy.Api.dll"]