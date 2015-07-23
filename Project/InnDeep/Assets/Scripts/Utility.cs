using UnityEngine;
using System.Collections;

namespace InnDeep.Util
{
    public struct Vector2i
    {
        public int x;
        public int y;

        public Vector2i(Vector2 v)
        {
            x = (int)v.x;
            y = (int)v.y;
        }

        public Vector2i(Vector3 v)
        {
            x = (int)v.x;
            y = (int)v.y;
        }

        public Vector2i(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator==(Vector2i rhs, Vector2i lhs)
        {
            return (rhs.x == lhs.x && rhs.y == lhs.y);
        }

        public static bool operator!=(Vector2i rhs, Vector2i lhs)
        {
            return (rhs.x != lhs.x || rhs.y != lhs.y);
        }

        public static Vector2i zero
        {
            get
            {
                return new Vector2i(0, 0);
            }
        }
    }

    public class ConvertSpace
    {
        public static Vector2 tileToWorld(Vector2 v, int rows, int cols)
        {
            if(rows > 0 && cols > 0)
            {
                v.x *= cols;
                v.y *= rows;
            }
            return v;
        }

        public static Vector3 tileToWorld(Vector3 v, int rows, int cols)
        {
            if (rows > 0 && cols > 0)
            {
                v.x *= cols;
                v.y *= rows;
            }
            return v;
        }

        public static Vector2i worldToTile(Vector3 v, float tileWidth, float tileHeight)
        {
            if (tileWidth > 0 && tileHeight > 0)
            {
                v.x = Mathf.Round(v.x / tileWidth);
                v.y = Mathf.Round(v.y / tileHeight);
            }
            return new Vector2i(v);
        }

        public static Vector2i indexToTile(int index, int rows, int cols)
        {
            return new Vector2i(
                0,
                0
                );
        }

        public static int tileToIndex(Vector2i v, int rows, int cols)
        {
            return tileToIndex(v.x, v.y, rows, cols);
        }

        public static int tileToIndex(int x, int y, int rows, int cols)
        {
            return y + x * rows;
        }
    }

}