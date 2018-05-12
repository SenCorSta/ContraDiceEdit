using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    var button = transform.gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(quitgame);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void quitgame() 
    {
        Debug.Log("游戏退出!");
        Application.Quit(); 
    }
    
}
