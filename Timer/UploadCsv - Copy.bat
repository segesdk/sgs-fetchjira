CsvToSqlServer.exe -F "CHR_Samlesteder.csv" -E 65001 -T "CHR_Samlesteder" -L ";" -C:"en-US" -ocu -S "localhost" -D "ds-pigcoin-prod" 
CsvToSqlServer.exe -F "slagterier_dk_2020-01-10.csv" -E 65001 -T "slagterier_dk_2020_01_10" -L ";" -C:"en-US" -ocu -S "localhost" -D "ds-pigcoin-prod" 
pause