@ECHO OFF

ECHO Packing GNaP.Owin.Authentication.Jwt
nuget pack src\GNaP.Owin.Authentication.Jwt\GNaP.Owin.Authentication.Jwt.csproj -Build -Prop Configuration=Release -Exclude gnap.ico
