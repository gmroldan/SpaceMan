using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text scoreText;
    public Text maxScoreText;
    public Text coinsText;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        this.playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.INSTANCE.IsInGame())
        {
            int coins = GameManager.INSTANCE.collectedObjects;
            float score = playerController.GetTravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxScore", 0f);

            this.scoreText.text = "Score: " + score.ToString("f1");
            this.coinsText.text = coins.ToString();
            this.maxScoreText.text = "Score: " + maxScore.ToString("f1");
            
        }

    }
}
