using UnityEngine;
using UnityEditor;
using System.Collections;

namespace InnDeep.Config
{

    public class TileSheetAsset
    {

        [MenuItem("Assets/Create/TileSheet")]
        public static void CreateAsset()
        {
            CreateScriptableObject.CreateAsset<TileSheet>();
        }
    }
}