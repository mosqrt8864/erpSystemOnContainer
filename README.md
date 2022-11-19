# 前言

此專案是小弟練習使用DDD與Clean Architecture來開發實作ERP系統，並且熟悉.Net Core的使用方式，目前初步將各個模組系統拆分出微服務(WebApi)，利用MVC網頁做簡單操作畫面

## 技術

* ASP.NET Core 6
* Entity Framework Core
* MediatR
* AutoMapper
* Autofac

## 使用

物料服務啟動方法

```command line
dotnet run --project src/Services/MaterialsManagement/MaterialsManagement.Api/MaterialsManagement.Api.csproj
```

採購服務啟動方法

```command line
dotnet run --project src/Services/PurchaseManagement/PurchaseManagement.Api/PurchaseManagement.Api.csproj
```

ERP系統(WebMVC)啟動方法

```command line
dotnet run --project src/Webs/WebMVC/WebMVC.csproj 
```

開啟瀏覽器輸入

```url
http://localhost:5000/
```

## 後記

實作過程中還是有些地方做的很亂(Mvc&Ajax)，希望能藉由多寫讓自己進步，專案會繼續更新下去，目前感覺要做的東西太亂，先統整好再來一步一步慢慢完成。

### AutoMapper

使用上參考ca-sln專案範例做法，了解到動態的功能可以使用Reflection來實作

### 未完善的東西

* Database
* Domain Event的傳遞辦法
* Logger
* Id的設計：高併發的情況，又想要有能識別溝通的單號
* 測試
* SPA
* Docker

## 參考

* [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)
* [Clean Architecture Solution Template](https://github.com/jasontaylordev/CleanArchitecture/tree/413fb3a68a0467359967789e347507d7e84c48d4)
