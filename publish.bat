@echo off
setlocal

echo  Publishing project...
dotnet publish StockIT\StockIT.csproj -c Release -o publish
if errorlevel 1 (
    echo  Publish failed.
    exit /b 1
)

echo  Creating ZIP archive...
tar -a -c -f stockit.zip publish
if errorlevel 1 (
    echo  Zip creation failed.
    exit /b 1
)

echo  Deploying to Azure...
az webapp deploy --resource-group stockit-rg --name stockit --src-path stockit.zip
if errorlevel 1 (
    echo  Deployment failed.
    exit /b 1
)

echo  Deployment completed successfully!
echo  Visit: https://stockit.azurewebsites.net

endlocal
pause
