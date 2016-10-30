using System;
using System.Collections.Generic;
using Assets.Scripts.Menus.ContextMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.RoomObjects
{
    public class Tv : MonoBehaviour, ISmartObject
    {
        public GameObject Menu;
        public GameObject Video;

        public void Start()
        {

        }

        public void Update()
        {

        }

        public void TvOn()
        {
            Light[] lights = GetComponentsInChildren<Light>();

            foreach (var light in lights)
            {
                light.enabled = true;
            }

            var zzz = ((MovieTexture)Video.GetComponent<Renderer>().material.mainTexture);
            zzz.Play();
        }

        public void TvOff()
        {
            Light[] lights = GetComponentsInChildren<Light>();

            foreach (var light in lights)
            {
                light.enabled = false;
            }
            ((MovieTexture)Video.GetComponent<Renderer>().material.mainTexture).Stop();
        }

        public void TvPause()
        {
           ((MovieTexture)Video.GetComponent<Renderer>().material.mainTexture).Pause();
        }
        public void ShowMenu()
        {
            Menu.SetActive(true);
            MenuMemory.menuItems.Add(Menu);
        }
        public void HideMenu()
        {
            Menu.SetActive(false);
        }
    }
}