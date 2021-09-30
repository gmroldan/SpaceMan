using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text scoreText;
    public Text maxScoreText;
    public Text coinsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.INSTANCE.IsInGame())
        {
            int coins = GameManager.INSTANCE.collectedObjects;
            float score = 0;
            float maxScore = 0;

            this.scoreText.text = "Score: " + score.ToString("f1");
            this.coinsText.text = coins.ToString();
            this.maxScoreText.text = "Score: " + maxScore.ToString("f1");
            
        }

    }
}
