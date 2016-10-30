using Assets.Scripts.Menus.ContextMenu;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.RoomObjects
{
    /// <summary>
    ///     Описывает управления люстрой
    /// </summary>
    public class Lamp : MonoBehaviour, ISmartObject
    {
        public GameObject Menu; // Меню управления лампой


        /// <summary>
        ///     Включить люстру
        /// </summary>
        public void LampOn()
        {
            Debug.Log("Лампа включена");
            Light[] lights = GetComponentsInChildren<Light>();
            StartCoroutine(Post("1")); // Выполниь асинхронный Http запрос

            foreach (var light in lights)
            {
                light.enabled = true;
            }
        }

        /// <summary>
        ///     Выключить люстру
        /// </summary>
        public void LampOff()
        {
            StartCoroutine(Post("0")); // Выполниь асинхронный Http запрос

            Debug.Log("Лампа выключена");

            Light[] lights = GetComponentsInChildren<Light>();

            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }

        public string Name { get; set; }

        /// <summary>
        ///     Показать меню управления люстрой
        /// </summary>
        public void ShowMenu()
        {
            Menu.SetActive(true);
            MenuMemory.menuItems.Add(Menu);
        }
        public void HideMenu()
        {
            Menu.SetActive(false);
        }

        /// <summary>
        ///     Отправка Post запроса на сервер
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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