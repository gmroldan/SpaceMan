using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType {
    healthBar,
    manaBar
}

public class PlayerBar : MonoBehaviour
{
    public BarType type;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        this.slider = GetComponent<Slider>();

        if (BarType.healthBar.Equals(this.type))
        {
            this.slider.maxValue = PlayerController.MAX_HEALTH;
        }
        else
        {
            this.slider.maxValue = PlayerController.MAX_MANA;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BarType.healthBar.Equals(this.type))
        {
            this.slider.value = GameObject.Find("Player")
                    .GetComponent<PlayerController>().GetHealth();
        }
        else
        {
            this.slider.value = GameObject.Find("Player")
                    .GetComponent<PlayerController>().GetMana();
        }
    }
}
