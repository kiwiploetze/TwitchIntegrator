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
        TwitchNames.Initialize("gronkh");

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
