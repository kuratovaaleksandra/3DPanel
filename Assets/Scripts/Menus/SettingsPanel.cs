using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour {

    public InputField _inputField;

	void Start () {
        _inputField.text = @"http://localhost:7777/api/hall";
    }

	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    ///  Нажади кнопку Ok
    /// </summary>
    public void OkBtn()
    {
        MenuMemory.ServiceAddress = _inputField.text;
        gameObject.SetActive(false);
    }
}