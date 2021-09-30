using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager INSTANCE;
    public Canvas menuCanvas;
    public Canvas gameCanvas;

    void Awake() 
    {
        if (INSTANCE == null)
            INSTANCE = this;
    }

    public void ShowMainMenu()
    {
        this.menuCanvas.enabled = true;
        this.gameCanvas.enabled = false;
    }

    public void HideMainMenu()
    {
        this.menuCanvas.enabled = false;
        this.gameCanvas.enabled = true;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
