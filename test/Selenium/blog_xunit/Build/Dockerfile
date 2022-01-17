#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["blog_xunit/blog_xunit.csproj", "blog_xunit/"]
RUN dotnet restore "blog_xunit/blog_xunit.csproj"
COPY . .
WORKDIR "/src/blog_xunit"
RUN dotnet build "blog_xunit.csproj" -c Release -o /app/build
#RUN dotnet test "blog_xunit.csproj" --test-adapter-path:. --logger:xunit

FROM build AS publish
RUN dotnet publish "blog_xunit.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN dotnet test
ENTRYPOINT ["dotnet", "blog_xunit.dll"]