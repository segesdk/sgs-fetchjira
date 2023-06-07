Clear-Host

$CsvToSqlServer =  "$PSScriptRoot\CsvToSqlServer.exe"

$Server = "(localdb)\MSSQLLocalDB"
$Database = "JiraTimer"

$filename = "timer20230122171132.csv"
$tablename = "timer"
& $CsvToSqlServer -F "$PSScriptRoot\$filename" -T "$tablename" -L ";" -E 65001 -C:da-DK -cut -D $Database -S $Server #-U $User -P $Pass

$filename = "timer20230124082131.csv"
$tablename = "timerupdate"
& $CsvToSqlServer -F "$PSScriptRoot\$filename" -T "$tablename" -L ";" -E 65001 -C:da-DK -cut -D $Database -S $Server #-U $User -P $Pass
