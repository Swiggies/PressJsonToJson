# PressJsonToJson
My tool to make it easier to make a voting.json file for Eldewrito.

![Preview](https://i.imgur.com/acivcXr.png)

# Installation
Unzip the folder into your Halo Online (Or whatever you renamed it to, the one with ldorado.exe).

# Should I use voting.json or veto.json?
If your server is running on game modes that only need 1 map play, use veto.json.
If your server has at least 2 game modes that can be played on many maps, use voting.json

# How To Use
Make sure your maps and variants folders have the game modes you want to use (comes with some default variants and all default maps) otherwise you won't be able to select any.

## Voting
Select the maps you want your server to use in the top left list and select game variants in the top right.
When you select a game variant another window will show. You can select specific maps for that variant to use with this and input commands to be run when your server loads that variant.

If you want your variant to only run with a specific map, don't select the map in the main window otherwise that map will be run with other variants.

## Veto
Select the game modes you want to use in the top right list. When the advanced game types window is shows select the map(s) to go with that game mode. Do not select any maps in the top right pane otherwise a voting.json will be generated.

# My json file isn't working. Why
Check your dorito.logs file for any errors.
