using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace NekraliusDevelopmentStudio
{
    public class MenuManager : MonoBehaviour
    {
        //Code made by Victor Paulo Melo da Silva - Game Developer - GitHub - https://github.com/Necralius
        //MenuManager - (Code Version)
        //State: Functional - (Needs Coments)
        //This code represents (Code functionality or code meaning)

        #region - Singleton Pattern -
        public static MenuManager Instance;
        private void Awake() => Instance = this;
        #endregion

        #region - Main Data Declaration -
        public List<Menu> menuList = new List<Menu>();
        #endregion

        #region - Menu Activation -
        public void OpenMenu(string menuName)
        {
            foreach(Menu menu in menuList)
            {
                if (menu.menuName == menuName) menu.ActivateMenu();
                else menu.DeactivateMenu();
            }
        }
        public void OpenMenu(Menu menuItem)
        {
            foreach (Menu menu in menuList)
            {
                if (menu == menuItem) menu.ActivateMenu();
                else menu.DeactivateMenu();
            }
        }
        #endregion

        #region - Deativate Menu -
        public void CloseMenu(string menuName)
        {
            foreach (Menu menu in menuList)
            {
                if (menu.menuName == menuName) menu.DeactivateMenu();
                else menu.ActivateMenu();
            }
        }
        public void CloseMenu(Menu menuItem)
        {
            foreach (Menu menu in menuList)
            {
                if (menu == menuItem) menu.DeactivateMenu();
                else menu.ActivateMenu();
            }
        }
        #endregion
    }
}