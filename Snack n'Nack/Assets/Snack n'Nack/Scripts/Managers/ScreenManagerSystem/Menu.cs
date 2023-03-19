using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    public class Menu : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //Menu - (Code Version)
        //State: Functional - (Needs Coments)
        //This code represents (Code functionality or code meaning)

        #region - Item Data -
        public bool menuActive;
        public string menuName;
        #endregion

        #region - Menu Item Manegment -
        public void ActivateMenu()
        {
            gameObject.SetActive(true);
            menuActive = true;
        }
        public void DeactivateMenu()
        {
            gameObject.SetActive(false);
            menuActive = false;
        }
        #endregion
    }
}