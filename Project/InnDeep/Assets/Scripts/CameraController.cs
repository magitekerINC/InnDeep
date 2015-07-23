using UnityEngine;
using System.Collections;

namespace InnDeep.Game
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif

    public class CameraController : MonoBehaviour
    {
        private Transform tCamera;

        void Awake()
        {
            tCamera = transform;
        }

        public enum Space { World, Tile };

        public void moveToPosition(Vector3 pos, Space spaceType = Space.World)
        {
            switch(spaceType)
            {
                case Space.World:
                    break;
                case Space.Tile:
                    break;
            }
        }
    }
}