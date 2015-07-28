using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using InnDeep.Game;
using System;

namespace InnDeep.Config
{
    public class TileSystemWindow : EditorWindow
    {

        private readonly string WINDOWTITLE = "Tile System Manager";
        private string filePath = Application.dataPath;

        private GUILayoutOption FIELDWIDTH = GUILayout.Width(40);
        private int tileColumns = 0, tileRows = 0;
        private int groundLevel = 0, edgeWidth = 0;

        [MenuItem("Window/Tile System Manager")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(TileSystemWindow));
         
        }

        void OnFocus()
        {

        }

        void OnGUI()
        {
            DrawTitle();

            GUILayout.BeginHorizontal();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Values", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Columns:\t", EditorStyles.boldLabel);
            ControlTextField(out tileColumns, tileColumns, 100, 10);
            GUILayout.Label("Rows:\t", EditorStyles.boldLabel);
            ControlTextField(out tileRows, tileRows, 100, 10);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Ground:\t", EditorStyles.boldLabel);
            ControlTextField(out groundLevel, groundLevel, tileRows);
            GUILayout.Label("Edge:\t", EditorStyles.boldLabel);
            ControlTextField(out edgeWidth, edgeWidth, (int)(Mathf.Min(tileColumns, tileRows) * 0.5f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();


        }

        void ControlTextField(out int value, int curVal, int max, int min = 0)
        {
            var text = GUILayout.TextField(curVal.ToString(),
                FIELDWIDTH);
            text = Regex.Replace(text, @"[^0-9]", "");

            if (!int.TryParse(text, out value))
            {
                value = min;
            }

            if(value > max)
            {
                value = max;
            }
        }

        void ControlPath()
        {
            if (GUILayout.Button("Set Path"))
            {
                var selection = Selection.activeObject;
                if (selection == null)
                    return;

                filePath = AssetDatabase.GetAssetPath(selection);
                filePath = Path.GetDirectoryName(filePath);

            }
        }

        void DrawPath()
        {
            GUILayout.Label("Path: ", EditorStyles.boldLabel);
            GUILayout.Label(filePath, EditorStyles.boldLabel);
   
        }

        void DrawTitle()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(WINDOWTITLE, EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

        }

    }
}