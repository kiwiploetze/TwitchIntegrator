using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchIntegrator
{
    class ResidentialBuildingAIMod : ResidentialBuildingAI
    {
        public override string GenerateName(ushort buildingID, InstanceID caller)
        {
            return TwitchNames.GetResidentialName((int)buildingID);
        }
    }
}
