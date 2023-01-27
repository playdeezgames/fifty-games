dotnet publish ./src/FiftyGames/FiftyGames.vbproj -o ./pub-linux -c Release --sc -r linux-x64
dotnet publish ./src/FiftyGames/FiftyGames.vbproj -o ./pub-windows -c Release --sc -r win-x64
butler push pub-windows thegrumpygamedev/fitty:windows
butler push pub-linux thegrumpygamedev/fitty:linux
git add -A
git commit -m "shipped it!"