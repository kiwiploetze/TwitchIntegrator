using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchIntegrator
{
    class IndustrialBuildingAIMod : IndustrialBuildingAI
    {
        public override string GenerateName(ushort buildingID, InstanceID caller)
        {
            return TwitchNames.GetIndustrialName((int)buildingID);
        }
    }
}
