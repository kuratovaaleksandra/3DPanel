using Assets.Scripts.Menus.ContextMenu;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    ///     Описывает вращение главной камеры в сцене
    /// </summary>
    public class PlayerCamera : MonoBehaviour
    {
        public GameObject SettingsPanel; // Основные настройки приложения
        public Transform Target; // Обьект вокруг котрого вращается камера
        private GameObject _player;

        // Параметры вращаения камеры
        public int SpeedRotation = 1;
        public float TargetHeight = 2.0f;
        public float Distance = 2.8f;
        public float MaxDistance = 10f;
        public float MinDistance = 0.5f;
        public float XSpeed = 250.0f;
        public float YSpeed = 120.0f;
        public float YMinLimit = -40.0f;
        public float YMaxLimit = 80.0f;
        public float ZoomRotate = 20.0f;
        public float RotationDamping = 3.0f;

        private float _x = 0.0f;
        public float Y = 0.0f;

        /// <summary>
        ///     Главная камера
        /// </summary>
        private Camera _camera;

        /// <summary>
        ///  Вызывается при старте скрипта
        /// </summary>
        public void Start()
        {
            Vector3 angels = transform.eulerAngles;
            _x = angels.y;
            Y = angels.x;

            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().freezeRotation = true;

            // Получаем камеру
            _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        /// <summary>
        ///  Вызывается каждый кадр, после выполнения всех остальных частей кода
        /// </summary>
        public void LateUpdate()
        {
            if (!Target)
                return;

            if (Input.GetMouseButton(0))
            {
                _x += Input.GetAxis("Mouse X") * XSpeed * 0.02f;
                Y -= Input.GetAxis("Mouse Y") * YSpeed * 0.02f;
            }
            Distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * ZoomRotate * Mathf.Abs(Distance);
            Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);

            Y = ClampAngel(Y, YMinLimit, YMaxLimit);

            Quaternion rotation = Quaternion.Euler(Y, _x, 0);
            transform.rotation = rotation;

            Vector3 position = Target.position - (rotation * Vector3.forward * Distance + new Vector3(0, -TargetHeight, 0));

            transform.position = position;

            RaycastHit hit;

            Vector3 trueTargetPosition = Target.transform.position - new Vector3(0, -TargetHeight, 0);

            if (Physics.Linecast(trueTargetPosition, transform.position, out hit))
            {
                float tempDistance = Vector3.Distance(trueTargetPosition, hit.point) - 0.28f;
                position = Target.position - (rotation * Vector3.forward * tempDistance + new Vector3(0, -TargetHeight, 0));

                transform.position = position;
            }
        }

        public static float ClampAngel(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;

            return Mathf.Clamp(angle, min, max);
        }

        /// <summary>
        ///     Вызывается в каждом кадре
        /// </summary>
        public void Update()
        {
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) // Бросаем луч
                {
                    GameObject objectHit = hit.transform.gameObject; // Получаем обьект на который попали лучем
                    ISmartObject smartObjectMenu = objectHit.GetComponent<ISmartObject>();
                    if (smartObjectMenu != null) // Если этот объект поддерживает контекстное меню
                    {
                        if (MenuMemory.menuItems.Count > 0)
                            foreach (var item in MenuMemory.menuItems)
                            {
                                item.SetActive(false);
                            }
                        smartObjectMenu.ShowMenu();
                    }
                }
            }
        }

        /// <summary>
        ///      Выход из приложения
        /// </summary>
        public void ExitBtn()
        {
            Application.Quit();
        }

        /// <summary>
        ///     Обображает настройки приложения
        /// </summary>
        public void ShowSettings()
        {
            SettingsPanel.SetActive(true);
        }
    }
}