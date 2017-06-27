using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchIntegrator
{
    internal class CitizenManagerMod : CitizenManager
    {
        public string GetCitizenName(uint citizenID)
        {
            return TwitchNames.GetName((int) citizenID);            
        }
    }
}
