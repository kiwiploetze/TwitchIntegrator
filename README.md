Twitch Viewer Integrator for Cities: Skylines by kiwiploetze
======

This mod integrates your Twitch viewers to your Cities: Skylines game.
It retrieves the current viewer list of your channel when a game is started/loaded and stores them to an internal list from which the names will be generated.
Additionally you can provide some suffixes for the names which will be picked randomly.
The internal list of viewers will be periodically updated (which means new viewers will be appended) unless configured otherwise. 

Only tested on Win10!

##Config file location##
###Windows###
```
C:\Users\<username>\AppData\Local\Colossal Order\Cities_Skylines\Addons\Mods\TwitchIntegrator\TwitchIntegrator.conf
```

##Detailed instructions##
* Configure the Mod either by the built-in option menu or the config file
* UpdateRate is a number which indicates how many simulation frames shoul be waited until an viewer update is performed
* If you don't want an update set the UpdateRate to -1

* Config file syntax:
	* "key": "value"
	* "value" of *Adds is a comma separated list



The Mod is based on the functional idea of Andrew Wu's WufireNameGenerator:
https://github.com/wufire/CitiesSkylinesBuildingNameGenerator

Therefore, it also uses the detour functions of Sebastian Schöner:
https://github.com/sschoener/cities-skylines-detour

Most credits go to those two developers.

This Mod was developed for the german YTer "Gronkh", that is why his user name is the default.