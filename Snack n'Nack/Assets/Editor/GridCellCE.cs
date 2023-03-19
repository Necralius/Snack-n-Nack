using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [CustomEditor(typeof(GridCell))]
    public class GridCellCE : Editor
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GridCellCE - (0.1)
        //State: Functional - (Needs Refactoring, Needs Coments)
        //This code represents (Code functionality or code meaning)

        public override void OnInspectorGUI()
        {
            GridCell gridCell = (GridCell)target;

            if (GUILayout.Button("Show Cell")) gridCell.ShowObject();
            if (GUILayout.Button("Hide Cell")) gridCell.HideObject();

            DrawDefaultInspector();
        }
    }
}