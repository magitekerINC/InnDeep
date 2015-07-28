using UnityEngine;
using System.Collections;
using InnDeep.Config;
using InnDeep.Util;
using System;

namespace InnDeep.Game
{

    public class GameServices : MonoBehaviour
    {
        public static GameServices Instance;

        private Action<float> zoomEvent;
        private Action<Vector3> moveEvent;

        [SerializeField]
        private TileSystem tileSystem;

        [SerializeField]
        private CameraController cControls;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        // Use this for initialization
        void Start()
        {

        }


#if UNITY_EDITOR
        public void ReloadScene()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
#endif

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                MoveEvent(Input.mousePosition);
            }
        }

        public void ZoomEvent(float z)
        {
            if (zoomEvent != null)
                zoomEvent(z);
        }

        public void MoveEvent(Vector3 v)
        {
            if (moveEvent != null)
                moveEvent(v);
        }

        public void addZoomEvent(Action<float> e)
        {
            zoomEvent += e;
        }

        public void removeZoomEvent(Action<float> e)
        {
            zoomEvent -= e;
        }

        public void addMoveEvent(Action<Vector3> e)
        {
            moveEvent += e;
        }

        public void removeMoveEvent(Action<Vector3> e)
        {
            moveEvent -= e;
        }
  
    }
}