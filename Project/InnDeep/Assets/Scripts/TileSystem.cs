using System;
using System.Collections.Generic;
using UnityEngine;
using InnDeep.Util;
using InnDeep.Config;

namespace InnDeep.Game
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif

    public class TileSystem : MonoBehaviour
    {

        #region Fields
        [SerializeField]
        private GameObject tilePrefab;
        [SerializeField]
        private Rect tileArea;
        [SerializeField]
        private Bounds bounds;

        [SerializeField]
        private int rows, cols;
        private List<SpriteRenderer> tiles;
        private int[,] tileData;

        private TileSheet tileSheet;
        #endregion

        #region Properties
        public float TileHeight { get { return bounds.size.y; } }
        public float TileWidth { get { return bounds.size.x; } }
        #endregion

        void Awake()
        {
            tiles = new List<SpriteRenderer>(rows * cols);
        }

        void Start()
        {

        }

        #region Tile Functions
        private void SetupTiles()
        {
            tileData = new int[cols, rows];
            var pos = new Vector3(tileArea.xMin, tileArea.yMin);

            pos.x += TileWidth * 0.5f;
            pos.y += TileHeight * 0.5f;

            for(int x=0; x < cols; ++x)
            {
                for(int y=0; y < rows; ++y)
                {
                    var gObj = Instantiate(tilePrefab, pos, Quaternion.identity) as GameObject;
                    tiles.Add(gObj.GetComponent<SpriteRenderer>());
                    tileData[x, y] = 1;
                    pos.y += TileHeight;
                }
                pos.x += TileWidth;
                pos.y = tileArea.yMin + TileHeight * 0.5f;
            }

            UpdateTiles();
        }

        private void UpdateTiles()
        {
            for(int x=0; x < cols; ++x)
            {
                for(int y=0; y < rows; ++y)
                {
                    tileData[x, y] = getTileValue(x, y);
                }
            }
        }

        private void setTileSprites()
        {
            if (tileSheet == null)
                return;

            for(int x=0; x < cols; ++x)
            {
                for(int y=0; y < rows; ++y)
                {
                    var ind = ConvertSpace.tileToIndex(x, y, rows, cols);
                    tiles[ind].sprite = tileSheet.getTile(tileData[x, y]);
                }
            }
        }

        private int getTileValue(int x, int y)
        {
            int result = 0;

            if (tileData[x, y] > 0)
            {
                if (y - 1 >= 0 &&
                    tileData[x, y - 1] != 0)
                {
                    result += 2;
                }

                if (x - 1 >= 0 &&
                    tileData[x - 1, y] != 0)
                {
                    result += 16;
                }

                if (y + 1 < rows &&
                    tileData[x, y + 1] != 0)
                {
                    result += 8;
                }

                if (x + 1 < cols &&
                    tileData[x + 1, y] != 0)
                {
                    result += 4;
                }
            }


            return result;
        }

        private void ClearTiles()
        {
            for(int i=0; i < tiles.Count; ++i)
            {
                Destroy(tiles[i].gameObject);
            }
        }
        #endregion

        public bool checkWorldBounds(Vector3 pos)
        {
            return tileArea.Contains(pos);
        }



#if UNITY_EDITOR
        void OnValidate()
        {
            if (tilePrefab == null)
                return;
            var sRender = tilePrefab.GetComponent<SpriteRenderer>();

            if (sRender == null)
            {
                tilePrefab = null;
                return;
            }

            ClearTiles();
            bounds = sRender.bounds;

            if (rows < 0)
                rows = 0;
            if (cols < 0)
                cols = 0;

            tileArea = new Rect(0, 0, TileWidth * cols, TileHeight * rows);
            SetupTiles();
        }



        void OnDrawGizmos()
        {
            if (tilePrefab == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(tileArea.center, tileArea.size);
            Gizmos.color = Color.white;
            DrawGrid();
        }

        void DrawGrid()
        {
            if (bounds.size.magnitude == 0f)
                return;

            Vector3 vFrom = new Vector3(tileArea.xMin, tileArea.yMin);
            Vector3 vTo = new Vector3(tileArea.xMin, tileArea.yMax);

            Vector3 hFrom = new Vector3(tileArea.xMin, tileArea.yMin);
            Vector3 hTo = new Vector3(tileArea.xMax, tileArea.yMin);

            for(int x=0; x < rows; ++x)
            {
                Gizmos.DrawLine(hFrom, hTo);
                hFrom.y = hTo.y += bounds.size.y;
            }

            for(int y=0; y < cols; ++y)
            {
                Gizmos.DrawLine(vFrom, vTo);
                vFrom.x = vTo.x += bounds.size.x;
            }
        }
#endif
    }
}