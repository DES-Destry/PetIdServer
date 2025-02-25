#!/bin/sh

dotnet user-secrets set "JwtAccessTokenSecret" "51619f4020c4eb1a72200b2d41d17940cf8972362e4405b4b9ef39a1ad43e3316abc0715e61b96a3785092e168dab369e547135fc0b06ce28bb4091253558b25"
dotnet user-secrets set "JwtAccessTokenTtl" "2 minutes"
dotnet user-secrets set "JwtAccessTokenTtlManager" "10 minutes"
dotnet user-secrets set "JwtRefreshTokenSecret" "3a87e80c1d810f22eda5756ccd9b2336a32324be079c2a06822c40fd2f7fd9539d943fb1857f59c52d3d4f3f1154057feae82fce1ee22c19ca0ec849ef15a33b"
dotnet user-secrets set "JwtRefreshTokenTtl" "90 days"
dotnet user-secrets set "JwtRefreshTokenTtlManager" "0"
dotnet user-secrets set "JwtIssuer" "https://api-dev.petidapp.com"
dotnet user-secrets set "JwtAudience" "com.jaschaco.petid"
dotnet user-secrets set "RequestSecurityKey" "49913c7c2e3e99088e89dbdf7c4a771a2ca2d439ad0d6470cab936de84a32cee"
dotnet user-secrets set "DatabaseConnectionString" "PetIdPostgresDb"
