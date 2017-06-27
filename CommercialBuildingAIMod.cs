using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchIntegrator
{
    class CommercialBuildingAIMod : CommercialBuildingAI
    {
        public override string GenerateName(ushort buildingID, InstanceID caller)
        {
            return TwitchNames.GetCommercialName((int)buildingID);
        }
    }
}
