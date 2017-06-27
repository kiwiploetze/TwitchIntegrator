/*
The MIT License (MIT)

Copyright (c) 2016 Andrew Wu

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
*/

using ICities;
using System;
using System.Reflection;
using System.Collections.Generic;
using Moledozer;

namespace TwitchIntegrator
{
  public class LoadingExtension : LoadingExtensionBase
  {

    private List<RedirectCallsState> redirectStates = new List<RedirectCallsState>();

    public override void OnLevelLoaded (LoadMode mode)
    {
      if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
      {
        TwitchNames.Initialize();

        Dictionary<Type, Type> componentRemap = new Dictionary<Type, Type> ();
        //componentRemap.Add (typeof(CitizenManager), typeof(CitizenManagerMod));
        componentRemap.Add (typeof(RoadAI), typeof(RoadAIMod));
        componentRemap.Add(typeof(CommercialBuildingAI), typeof(CommercialBuildingAIMod));
        componentRemap.Add(typeof(ResidentialBuildingAI), typeof(ResidentialBuildingAIMod));
        componentRemap.Add(typeof(IndustrialBuildingAI), typeof(IndustrialBuildingAIMod));
        componentRemap.Add(typeof(OfficeBuildingAI), typeof(OfficeBuildingAIMod));


        foreach (var pair in componentRemap)
        {
          redirectStates.Add(NewRedirectState(pair.Key, pair.Value));
        }

        redirectStates.Add(RedirectionHelper.RedirectCalls(
            typeof(CitizenManager).GetMethod("GetCitizenName", BindingFlags.Instance | BindingFlags.Public),
            typeof(CitizenManagerMod).GetMethod("GetCitizenName", BindingFlags.Instance | BindingFlags.Public)
        ));

      }
    }

    private RedirectCallsState NewRedirectState(Type originalType, Type modType)
    {
      return RedirectionHelper.RedirectCalls(
        originalType.GetMethod("GenerateName", BindingFlags.Instance | BindingFlags.Public),
        modType.GetMethod("GenerateName", BindingFlags.Instance | BindingFlags.Public)
      );
    }

    public override void OnLevelUnloading()
    {
      base.OnLevelUnloading();
      foreach (RedirectCallsState redirectState in redirectStates)
      {
        RedirectionHelper.RevertRedirect(redirectState);
      }
    }
  }
}
