using System;
using System.Collections.Generic;
using Assets.Scripts.Menus.ContextMenu;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Networking;
using System.Collections;

namespace Assets.Scripts.RoomObjects
{
    public class Lamp2 : MonoBehaviour, ISmartObject
    {


        public GameObject Menu;

        public void Start()
        {

        }

        public void Update()
        {

        }

        public void Red()
        {
            Debug.Log("Включил");
            Light[] lights = GetComponentsInChildren<Light>();
            StartCoroutine(Post("2")); // Выполниь асинхронный Http запрос

            foreach (var light in lights)
            {
                light.enabled = true;
                light.color = Color.red;
            }
        }

        public void Green()
        {
            StartCoroutine(Post("3")); // Выполниь асинхронный Http запрос

            Debug.Log("Выключил");

            Light[] lights = GetComponentsInChildren<Light>();

            foreach (var light in lights)
            {
                light.enabled = true;
                light.color = Color.green;
            }
        }
        public void Blue()
        {
            StartCoroutine(Post("4")); // Выполниь асинхронный Http запрос

            Debug.Log("Выключил");

            Light[] lights = GetComponentsInChildren<Light>();

            foreach (var light in lights)
            {
                light.enabled = true;
                light.color = Color.blue;
            }
        }

        public void LampOff()
        {
            StartCoroutine(Post("5")); // Выполниь асинхронный Http запрос

            Debug.Log("Выключил");

            Light[] lights = GetComponentsInChildren<Light>();

            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }

        public void ShowMenu()
        {
            Menu.SetActive(true);
        }
        public void HideMenu()
        {
            Menu.SetActive(false);
            MenuMemory.menuItems.Add(Menu);
        }

        IEnumerator Post(string value)
        {
            WWWForm form = new WWWForm();
            form.AddField("DeviceId", "1");
            form.AddField("Value", value);

            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Post(MenuMemory.ServiceAddress, form);
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}