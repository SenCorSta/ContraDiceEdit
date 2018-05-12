using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartBtn : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        var button = transform.gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(go);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void go() 
    {
        Application.LoadLevel("Test");
    }
}
