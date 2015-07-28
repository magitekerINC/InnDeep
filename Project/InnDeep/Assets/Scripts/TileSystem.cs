using System;
using System.Collections.Generic;
using UnityEngine;
using InnDeep.Util;
using InnDeep.Config;
using System.IO;
using System.Collections;

namespace InnDeep.Game
{
    public struct TileData
    {
        public int value;
        public int sheetIndex;
        public TileData(int _val, int _index)
        {
            value = _val;
            sheetIndex = _index;
        }
    }
#if UNITY_EDITOR
    //[ExecuteInEditMode]
#endif
    public class TileSystem : MonoBehaviour
    {

        #region Fields
        [SerializeField]
        private GameObject tilePrefab;
        private Rect tileArea;
        public Rect Area { get { return tileArea; } }
        private Bounds bounds;

        [SerializeField]
        private int rows, cols;
        [SerializeField]
        private int groundLevel;
        private List<SpriteRenderer> tiles;
        private TileData[,] tileData;

        [SerializeField]
        private TileSheet[] tileSheet;
        #endregion

        #region Properties
        public float TileHeight { get { return bounds.size.y; } }
        public float TileWidth { get { return bounds.size.x; } }
        #endregion

        void Awake()
        {
            //tiles = new List<SpriteRenderer>(rows * cols);
        }

        void Start()
        {
            //SetupTiles();
        }

        public void StartTileSystem()
        {

        }

        public void LoadTileSystem()
        {

        }
        
        #region Tile Functions
        private void SetupTiles()
        {
            tileData = new TileData[cols, rows];
            var pos = new Vector3(tileArea.xMin, tileArea.yMin);

            pos.x += TileWidth * 0.5f;
            pos.y += TileHeight * 0.5f;

            for(int x=0; x < cols; ++x)
            {
                for(int y=0; y < rows; ++y)
                {
                    var gObj = Instantiate(tilePrefab, pos, Quaternion.identity) as GameObject;
                    gObj.name = tilePrefab.name + x + y;
                    gObj.transform.parent = transform;
                    tiles.Add(gObj.GetComponent<SpriteRenderer>());
                    tileData[x, y] = new TileData(1,
                        (y > groundLevel? 0 : 1));
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
                    tileData[x, y].value = getTileValue(x, y);
                }
            }

            setTileSprites();
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
                    tiles[ind].sprite = tileSheet[tileData[x,y].sheetIndex].getTile(tileData[x, y].value);
                }
            }
        }

        private int getTileValue(int x, int y)
        {
            int result = 0;
            var index = tileData[x, y].sheetIndex;

            if (tileData[x, y].value > 0)
            {
                if (y - 1 >= 0 &&
                    index == tileData[x,y-1].sheetIndex &&
                    tileData[x, y - 1].value != 0)
                {
                    result += 2;
                }

                if (x - 1 >= 0 &&
                    index == tileData[x-1,y].sheetIndex &&
                    tileData[x - 1, y].value != 0)
                {
                    result += 16;
                }

                if (y + 1 < rows &&
                    index == tileData[x,y+1].sheetIndex &&
                    tileData[x, y + 1].value != 0)
                {
                    result += 8;
                }

                if (x + 1 < cols &&
                    index == tileData[x+1,y].sheetIndex &&
                    tileData[x + 1, y].value != 0)
                {
                    result += 4;
                }
            }


            return result;
        }

        private void ClearTiles()
        {
            if (tiles == null || tiles.Count == 0)
                return;

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

            bounds = sRender.bounds;

            if (rows < 0)
                rows = 0;
            if (cols < 0)
                cols = 0;

            tileArea = new Rect(0, 0, TileWidth * cols, TileHeight * rows);
            groundLevel = Mathf.Min(groundLevel, rows);
        }



        void OnDrawGizmos()
        {
            if (tilePrefab == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(tileArea.center, tileArea.size);
            Gizmos.color = Color.grey;
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

            for(int x=0; x <= rows; ++x)
            {
                Gizmos.DrawLine(hFrom, hTo);
                hFrom.y = hTo.y += bounds.size.y;
            }

            for(int y=0; y <= cols; ++y)
            {
                Gizmos.DrawLine(vFrom, vTo);
                vFrom.x = vTo.x += bounds.size.x;
            }
        }
#endif
    }
}