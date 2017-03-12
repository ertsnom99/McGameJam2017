using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public int sceneToStart = 1;
    public GameObject menuPanel;
    public GameObject creditsPanel;
    
	void Start () {

	}
	
	void Update () {
	
	}

    public void CreditsButtonClicked()
    {
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackCreditsButtonClicked()
    {
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
}
