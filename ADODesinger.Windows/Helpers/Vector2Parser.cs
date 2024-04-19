using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ADODesigner.Windows.Helpers
{
    public static class Vector2Parser
    {
        public static Vector2 Parse(JsonNode node)
        {
            float? xParse = (float?)node[0];
            float? yParse = (float?)node[1];

            float x = 0;
            float y = 0;

            if (xParse is not null) x = (float)xParse;
            if (yParse is not null) y = (float)yParse;

            return new Vector2(x, y);
        }
    }
}
