using UnityEngine;
using UnityEditor;
using System.Collections;

namespace InnDeep.Config
{
    public class GameValuesAsset
    {

        [MenuItem("Assets/Create/GameValues")]
        public static void CreateAsset()
        {
            CreateScriptableObject.CreateAsset<GameValues>();
        }
    }
}