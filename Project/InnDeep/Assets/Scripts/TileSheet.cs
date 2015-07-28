using UnityEngine;
using System.Collections;

namespace InnDeep.Config
{

    public class TileSheet : ScriptableObject
    {

        public Sprite bottomRight;
        public Sprite bottomMid;
        public Sprite bottomLeft;

        public Sprite midRight;
        public Sprite middle;
        public Sprite midLeft;

        public Sprite topRight;
        public Sprite topMid;
        public Sprite topLeft;

        public Sprite blank;
        public Sprite up;
        public Sprite down;
        public Sprite left;
        public Sprite right;

        public Sprite horizontal;
        public Sprite vertical;

        public Sprite getTile(int value)
        {
            switch (value)
            {
                case 2:
                    return up;
                case 4:
                    return right;
                case 8:
                    return down;
                case 16:
                    return left;
                case 6:
                    return topRight;
                case 10:
                    return vertical;
                case 12:
                    return bottomRight;
                case 14:
                    return midRight;
                case 18:
                    return topLeft;
                case 20:
                    return horizontal;
                case 22:
                    return topMid;
                case 24:
                    return bottomLeft;
                case 26:
                    return midLeft;
                case 28:
                    return bottomMid;
                case 30:
                    return middle;
                default:
                    return blank;
            }

        }
    }
}