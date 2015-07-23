using UnityEngine;
using System.Collections;

namespace InnDeep.Util
{
    public class Convert
    {
        public static Vector3 tileToWorldSpace(Vector3 v, float rows, float cols)
        {
            if (rows > 0 && cols > 0)
            {
                v.x *= cols;
                v.y *= rows;
            }
            return v;
        }

        public static Vector3 worldToTileSpace(Vector3 v, float tileWidth, float tileHeight)
        {
            if (tileWidth > 0 && tileHeight > 0)
            {
                v.x = Mathf.Round(v.x / tileWidth);
                v.y = Mathf.Round(v.y / tileHeight);
            }
            return v;
        }
    }

}