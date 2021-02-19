$toolDir = "$env:USERPROFILE\.dotnet\tools"
if (!(Test-Path "$toolDir/jb.exe")) {
    dotnet tool install -g JetBrains.ReSharper.GlobalTools
}
