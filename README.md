### Rest API
- využívá LTS verzi .NET Core 3.1  
- využívá Entity Framework  
- využívá Swashbuckle Swagger / Open API

## příprava databáze před 1. spuštěním:
*dotnet ef --startup-project WebApi.RestApi/WebApi.RestApi.csproj migrations remove -p WebApi.DataLayerEF/WebApi.DataLayerEF.csproj*  
*dotnet ef --startup-project WebApi.RestApi/WebApi.RestApi.csproj migrations add InitialModel -p WebApi.DataLayerEF/WebApi.DataLayerEF.csproj*  
*dotnet ef --startup-project WebApi.RestApi/WebApi.RestApi.csproj database update* 
*dotnet ef --startup-project WebApi.RestApi/WebApi.RestApi.csproj migrations remove -p WebApi.DataLayerEF/WebApi.DataLayerEF.csproj*  

## Swagger
-dostupný na '/swagger'  
Nepodařilo se jej zprovoznit, pokud je k dispozici další HttpGet metoda ve stejném controlleru, využívající jinou verzi API.  
Pokud se metoda 'GetAllProductsV2([FromQuery][Required] int pageNumber, [FromQuery][Required] int pageSize)' zakomentuje, Swagger funguje korektně.

## testování
V projektu WebApi.UnitTests je pár testů na controller.  
Bylo by dobré dopsat i testy, využívající volání přes HTTP/HTTPS, aby byl ověřen také "prostup" požadavku ke kontrolleru např. přes middleware, zpracování verze atp.  

## popis řešení
Systém je rozdělen do 4 projektů + 1 testovací projekt.  
Doménová vrstva obsahuje entitu Product, obecné rozhraní, které by každá entita měla implementovat (CRUD operace), dále rozhraní pro službu a také popis rozhraní, které musí implementovat datová vrsta.  
Datová vrsta obsahuje konfiguraci pro entitu Product, vč. naplnění 3 položkami. Vrstva implementuje rozhraní IUnitOfWork.  
Business vrstva řeší logiku jednotlivých operací. V tomto projektu jen to, aby se nevolal update položky, pokud nedošlo ke změně popisu.  
Aktualizaci popisu produktu by bylo vhodné omezit jen povoleným uživatelům (např. za pomoci JWT tokenů).  

## spuštění
spuštění verze s testovací databází v souboru:  
*dotnet run --project WebApi.RestApi/WebApi.RestApi.csproj --launch-profile "WebApi.RestApi"*  
spuštění verze s produkční databází:  
*dotnet run --project WebApi.RestApi/WebApi.RestApi.csproj --launch-profile "WebApi.RestApi Prod"*
spuštění testů:  
*dotnet test* (v adresáři projektu WebApi.UnitTests)
