using UnityEngine;
using System.Collections.Generic;

public class MenuMemory : MonoBehaviour {

    public static List<GameObject> menuItems { get; set; }
    public static string ServiceAddress { get; set; }

    // Use this for initialization
    void Start () {
        menuItems = new List<GameObject>();
        ServiceAddress = "http://localhost:7777/api/hall";
    }

	// Update is called once per frame
	void Update () {

	}
}