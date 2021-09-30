using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MENU, IN_GAME, GAME_OVER
}

public class GameManager : MonoBehaviour
{


    public static GameManager INSTANCE;
    public GameState currentGameState = GameState.MENU;
    PlayerController playerController;

    void Awake() 
    {
        if (INSTANCE == null)
            INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit")
                && !IsInGame())
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.IN_GAME);
    }

    public void GameOver()
    {
        SetGameState(GameState.GAME_OVER);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.MENU);
    }

    public bool IsInGame()
    {
        return currentGameState == GameState.IN_GAME;
    }

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.MENU)
        {
            // TODO: menu
        }
        else if (newGameState == GameState.IN_GAME)
        {
            LevelManager.INSTANCE.RemoveAllBlocks();
            this.currentGameState = newGameState;
            Invoke("ReloadLevel", 0.1f);
            MenuManager.INSTANCE.HideMainMenu();
        }
        else
        {
            MenuManager.INSTANCE.ShowMainMenu();
        }

        this.currentGameState = newGameState;
    }

    void ReloadLevel()
    {
        LevelManager.INSTANCE.GenerateInitialBlocks();
        this.playerController.StartGame();
        this.ResetCamera();
    }

    void ResetCamera() 
    {
        var mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }
}
