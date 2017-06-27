using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchIntegrator
{
    class RoadAIMod : RoadAI
    {
        public override string GenerateName(ushort segmentID, ref NetSegment data)
        {
            return TwitchNames.GetRoadName((int)segmentID);
        }
    }
}
