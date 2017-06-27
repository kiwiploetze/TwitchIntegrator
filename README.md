Twitch Viewer Integrator for Cities: Skylines by kiwiploetze
======

This mod integrates your Twitch viewers to your Cities: Skylines game.
It retrieves the current viewer list of your channel when a game is started/loaded and stores them to an internal list from which the names will be generated.
Additionally you can provide som suffixes for the names which will be picked randomly.
The internal list of viewers will be periodically updated (which means new viewers will be appended) unless configured otherwise. 

##Config file location##
###Windows###
```
C:\Users\<username>\AppData\Local\Colossal Order\Cities_Skylines\Addons\Mods\TwitchIntegrator\config.txt
```

##Detailed instructions##
* Configure the Mod via the config.txt
  * The format of the config file is JSON
  * The "user" value should be your Twitch Name (Default: "gronkh")
  * The "*Adds" are some suffix lists for each building category and the roads which will be appended randomly to the viewers name (Default: Some typical german suffixes)
  * The "updateRate" is a number, which indicates how many simulation ticks should be waited before new viewer names will be added to the internal list. If you don't want an update set the value to "-1".
 

The Mod is based on the functional idea of Andrew Wu's WufireNameGenerator:
https://github.com/wufire/CitiesSkylinesBuildingNameGenerator

Therefore, it also uses the detour functions of Sebastian Schöner:
https://github.com/sschoener/cities-skylines-detour

Most credits go to those two developers.

This Mod was developed for the famous german YTer "Gronkh", that is why his user name is the default.