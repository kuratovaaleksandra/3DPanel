using Assets.Scripts.Menus.ContextMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.RoomObjects
{
    public class Door : MonoBehaviour, ISmartObject
    {
        public GameObject Menu;

        public void HideMenu()
        {
            Menu.SetActive(false);
        }

        public void ShowMenu()
        {
            Menu.SetActive(true);
        }
    }
}
