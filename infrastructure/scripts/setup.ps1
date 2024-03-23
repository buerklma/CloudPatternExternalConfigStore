$global:AzResourceGroupName="AppConfigResource-rg"
$global:AzAppConfigName="AppConfigResource-config"


# Create the test resource group
az group create --name $global:AzResourceGroupName --location westeurope
# Create the Azure App Config Store
az appconfig create --location centralus --name $global:AzAppConfigName --resource-group $global:AzResourceGroupName

Write-Host "Created application configuration service " $global:AzAppConfigName

az appconfig kv set --name $global:AzAppConfigName --key Logging:LogLevel:Default --value Information --yes
Write-Host "Added key to " $global:AzAppConfigName

az appconfig credential list --name $global:AzAppConfigName
