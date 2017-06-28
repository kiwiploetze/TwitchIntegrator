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

using System;
using ICities;
using UnityEngine;
using System.Collections;

namespace TwitchIntegrator
{
    public class TwitchIntegrator : IUserMod
    {
        public string Description
        {
            get
            {
                return "Integrate your Twitch viewers. Building, Street and Citizen Names will be generated from your Twitch viewers!";
            }
        }

        public string Name
        {
            get
            {
                return "Twitch Integrator";
            }
        }

        private void EventTextSubmitted(string c)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "sub:"+c);
        }

        private void UserTextChanged(string c)
        {
            TwitchNamesSettings.username = c;
        }

        private void RateTextChanged(string c)
        {
            TwitchNamesSettings.updateRate = int.Parse(c);
        }

        private void StreetTextChanged(string c)
        {
            TwitchNamesSettings.streets = new ArrayList(c.Split(','));
        }


        private void IndustrialTextChanged(string c)
        {
            TwitchNamesSettings.industrial = new ArrayList(c.Split(','));
        }

        private void CommercialTextChanged(string c)
        {
            TwitchNamesSettings.commercial = new ArrayList(c.Split(','));
        }

        private void ResidentialTextChanged(string c)
        {
            TwitchNamesSettings.residential = new ArrayList(c.Split(','));
        }

        private void OfficeTextChanged(string c)
        {
            TwitchNamesSettings.office = new ArrayList(c.Split(','));
        }


        private void EventClick()
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "click");
            TwitchNamesSettings.SaveConfig();
        }


        public void OnSettingsUI(UIHelperBase helper)
        {
            TwitchNamesSettings.init();
            TwitchNamesSettings.LoadConfig();
            UIHelperBase group = helper.AddGroup("TwitchIntegrator");
            group.AddTextfield("Twitch Channel Name", TwitchNamesSettings.username, UserTextChanged, EventTextSubmitted);
            group.AddTextfield("Update Rate (in Simulation Frames)", TwitchNamesSettings.updateRate.ToString(), RateTextChanged, EventTextSubmitted);
            group.AddTextfield("Street Suffixes (comma separated)", TwitchNamesSettings.GetStringRepresentation(TwitchNamesSettings.streets), StreetTextChanged, EventTextSubmitted);
            group.AddTextfield("Industrial Suffixes (comma separated)", TwitchNamesSettings.GetStringRepresentation(TwitchNamesSettings.industrial), IndustrialTextChanged, EventTextSubmitted);
            group.AddTextfield("Commercial Suffixes (comma separated)", TwitchNamesSettings.GetStringRepresentation(TwitchNamesSettings.commercial), CommercialTextChanged, EventTextSubmitted);
            group.AddTextfield("Residential Suffixes (comma separated)", TwitchNamesSettings.GetStringRepresentation(TwitchNamesSettings.residential), ResidentialTextChanged, EventTextSubmitted);
            group.AddTextfield("Office Suffixes (comma separated)", TwitchNamesSettings.GetStringRepresentation(TwitchNamesSettings.office), OfficeTextChanged, EventTextSubmitted);

            group.AddButton("Save", EventClick);
        }
    }
}
