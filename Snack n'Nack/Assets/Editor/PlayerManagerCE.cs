using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    [CustomEditor(typeof(PlayerManager))]
    public class PlayerManagerCE : Editor
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //CompleteCodeName - (Code Version)
        //Code State - (Needs Refactoring, Needs Coments, Needs Improvement)
        //This code represents (Code functionality or code meaning)

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Reset Data")) PlayerManager.Instance.playerData.ResetScore();

            base.OnInspectorGUI();
        }
    }
}