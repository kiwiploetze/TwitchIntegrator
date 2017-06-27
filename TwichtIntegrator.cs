/*
The MIT License (MIT)

Copyright (c) 2017 Andreas Lund

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

The code is based upon the WufireNameGenerator Mod from Andrew Wu:
  * https://github.com/wufire/CitiesSkylinesBuildingNameGenerator

RedirectionHelper.cs was taken from the Moledozer mod found here:
  * https://github.com/fadster/Moledozer/blob/master/Moledozer/RedirectionHelper.cs

*/

using ICities;
namespace TwitchIntegrator
{
  public class TwitchIntegrator : IUserMod
  {
    public string Description
    {
      get
      {
        return "Integrate your Twitch viewers into Cities: Skylines. Building, Street and Citizen Names will be auto-generated with names of your Twitch viewers! Please configure this Mod by editing the <Mod-Path>/config.txt (JSON Format)";
      }
    }

    public string Name
    {
      get
      {
        return "Twitch Integrator";
      }
    }
  }
}
