using Assets.Scripts.Menus.ContextMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.Networking;

namespace Assets.Scripts.RoomObjects
{
    public class Fan : MonoBehaviour, ISmartObject
    {

        public GameObject Menu;
        public GameObject Vent;
        public MenuMemory menuMemory;

        Transform trans;


        bool isRotate = false;

        // Use this for initialization
        public void Start()
        {
            trans = Vent.GetComponent<Transform>();
        }

        // Update is called once per frame
        public void Update()
        {
            if(isRotate)
                trans.Rotate(new Vector3(0, 0, 10), Space.World);
        }

        public void FanOn()
        {
            isRotate = true;
            StartCoroutine(Post("6")); // Выполниь асинхронный Http запрос
        }

        public void FanOff()
        {
            isRotate = false;
            StartCoroutine(Post("7")); // Выполниь асинхронный Http запрос
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
                Debug.Log("Отправил: " + MenuMemory.ServiceAddress);
            }
        }
    }
}