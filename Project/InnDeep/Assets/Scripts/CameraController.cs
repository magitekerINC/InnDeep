using InnDeep.Patterns;
using InnDeep.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InnDeep.Game
{



    public class CameraController : MonoBehaviour
    {
        private GameServices game;
        private Transform tCamera;

        #region Fields
        private Rect world;

        public Rect frustrum;


        [SerializeField]
        [Range(1f, 20f)]
        private float fSpeed = 1f;

        [SerializeField]
        [Range(-20f, -1f)]
        private float minZoom = -1f, maxZoom = -10f;
        private float targetZoom;

        private Vector3 targetPos;
        #region Properties
        public Rect World { set { world = value; } }
        public Vector3 Position { get { return tCamera.position; } }
        #endregion
        #endregion

        void Awake()
        {

            tCamera = transform;
            frustrum = Camera.main.pixelRect;
            frustrum.size = frustrum.size * (1 / 30f);

        }

        void Start()
        {
            game = GameServices.Instance;

            game.addMoveEvent(setTarget);
            game.addZoomEvent(setZoom);

            InvokeRepeating("moveCamera", 0, Time.deltaTime);
            //InvokeRepeating("zoomCamera", 0, Time.deltaTime);
            targetPos = Position;
            targetZoom = Position.z;
        }

        public void setZoom(float z)
        {
            targetZoom = z;
        }

        public void setTarget(Vector3 t)
        {
            t.z = -Position.z;
            t = Camera.main.ScreenToWorldPoint(t);
            t.z = Position.z;
 
            targetPos = t;
        }

        private void moveCamera()
        {
            if (Position == targetPos)
                return;

            var step = Vector3.MoveTowards(Position, targetPos, fSpeed * Time.deltaTime);
            var frame = frustrum;
            frame.center = step;
            frustrum = frame;
            tCamera.position = step;
          

        }

        private void zoomCamera()
        {
            if (targetZoom == Position.z)
                return;
            var step = Mathf.Clamp(
                Mathf.MoveTowards(Position.z, targetZoom, fSpeed * Time.deltaTime),
                minZoom, maxZoom
                );
            tCamera.position = new Vector3(Position.x, Position.y, step);
        }

        private bool boundCheck(Rect frust)
        {
            return world.ContainsRect(frust);
        }


        void Update()
        {

        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {

        }
#endif
    }
}