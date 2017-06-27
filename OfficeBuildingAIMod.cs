using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchIntegrator
{
    class OfficeBuildingAIMod : OfficeBuildingAI
    {
        public override string GenerateName(ushort buildingID, InstanceID caller)
        {
            return TwitchNames.GetOfficeName((int)buildingID);
        }
    }
}
