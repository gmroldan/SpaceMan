using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType {
    healthPotion,
    manaPotion,
    money
}

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.money;
    public int value = 1;
    SpriteRenderer sprite;
    CircleCollider2D itemCollider;
    bool isCollected = false;

    void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();    
    }

    void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
        isCollected = false;
    }

    void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;
    }

    void Collect()
    {
        Hide();
        isCollected = true;

        switch (this.type) 
        {
            case CollectableType.money:
                GameManager.INSTANCE.CollectObject(this);
                break;
            case CollectableType.healthPotion:
                break;
            case CollectableType.manaPotion:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Collect();
        }
    }
}
