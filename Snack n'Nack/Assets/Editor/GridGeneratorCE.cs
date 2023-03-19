using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [CustomEditor(typeof(GridGenerator))]
    public class GridGeneratorCE : Editor
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //GridGeneratorCE - (0.1)
        //State: Functional - (Needs Refactoring, Needs Coments)
        //This code represents (Code functionality ou code meaning)

        public override void OnInspectorGUI()
        {
            GridGenerator gridGenerator = (GridGenerator)target;

            if (GUILayout.Button("Regenerate Grid"))
            {
                if (Application.isPlaying) gridGenerator.GenerateGridInGameAction();
            }
            if (GUILayout.Button("Update and Regenerate")) gridGenerator.GenerateGridInGameAction(gridGenerator.newMatrixFactor);

            DrawDefaultInspector();
        }
    }
}