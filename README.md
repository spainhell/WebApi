EF:
dotnet ef --startup-project WebApi.RestApi/WebApi.RestApi.csproj migrations add InitialModel -p WebApi.DataLayerEF/WebApi.DataLayerEF.csproj 
dotnet ef --startup-project WebApi.RestApi/WebApi.RestApi.csproj database update
