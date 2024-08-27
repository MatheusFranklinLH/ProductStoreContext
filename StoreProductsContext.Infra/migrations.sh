#!/bin/bash
export PATH="$PATH:$HOME/.dotnet/tools/"
Name="$1"
Params="$2"

echo "[BASH] Creating migration..."
dotnet ef migrations add $Name --startup-project ../StoreProductsContext.Api/ $Params

echo "[BASH] Generating migration SQL script..."
Filename=$(TZ=GMT date +"%Y%m%d%H%M%S")\_$Name.sql
dotnet ef migrations script --output Migrations/$Filename --startup-project ../StoreProductsContext.Api/ -i $Params

echo "[BASH] Script generated at Migrations/$Filename"
echo "[BASH] All done! Please, review the generated SQL script before applying the migration. You may apply the script manually, or let Entity Framework do the work for you, using:
>> dotnet ef database update --startup-project ../StoreProductsContext.Api/"
