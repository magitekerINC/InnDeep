using System;
using System.Collections.Generic;
using UnityEngine;

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
            tileData = new int[cols, rows];
        }

        void ClearTiles()
        {
            for(int i=0; i < tiles.Count; ++i)
            {
                Destroy(tiles[i].gameObject);
            }
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