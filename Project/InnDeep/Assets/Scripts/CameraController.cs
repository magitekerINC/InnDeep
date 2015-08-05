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

        private const float MINZOOM = -1f, MAXZOOM = -10f;
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
            frustrum.center = new Vector2(Position.x, Position.y);
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
            if (boundCheck(frame))
            {
                frustrum = frame;
                tCamera.position = step;
            }
            else
            {
                targetPos = Position;
            }
          

        }

        private void zoomCamera()
        {
            if (targetZoom == Position.z)
                return;
            var step = Mathf.Clamp(
                Mathf.MoveTowards(Position.z, targetZoom, fSpeed * Time.deltaTime),
                MINZOOM, MAXZOOM
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
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(frustrum.center, frustrum.size);
        }
#endif
    }
}