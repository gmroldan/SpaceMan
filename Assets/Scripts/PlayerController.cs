using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const int INITIAL_HEALTH = 100;
    const int INITIAL_MANA = 15;
    public const int MAX_HEALTH = 200;
    public const int MAX_MANA = 30;
    const int MIN_HEALTH = 10;
    const int MIN_MANA = 0;
    const int SUPER_JUMP_COST = 5;
    const float SUPER_JUMP_FORCE = 1.5f;
    
    public float jumpForce = 6f;
    public float runningSpeed = 2f;
    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 startPosition;

    int healthPoints;
    int manaPoints;


    public LayerMask groundMask;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ActionJumpTriggered())
        {
            Jump(IsSuperJump());
        }

        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        Debug.DrawRay(this.transform.position, 
                        Vector2.down * 1.5f, 
                        Color.red);
    }

    bool ActionJumpTriggered()
    {
        return GameManager.INSTANCE.IsInGame() 
                && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Super Jump"))
                && IsTouchingTheGround();
    }

    bool IsSuperJump()
    {
        return Input.GetButtonDown("Super Jump");
    }

    void Jump(bool superJump) 
    {
        float jumpForceFactor = jumpForce;

        if (superJump 
            && manaPoints >= SUPER_JUMP_COST)
        {
            manaPoints -= SUPER_JUMP_COST;
            jumpForceFactor *= SUPER_JUMP_FORCE;
        }


        rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
    }

    void FixedUpdate() 
    {
        if (GameManager.INSTANCE.IsInGame())
        {
            Walk();
        }
        else 
        {
            StopWalking();
        }
        
    }

    void Walk()
    {
        if (rigidBody.velocity.x < runningSpeed)
        {
            var xAxisValue = Input.GetAxis("Horizontal");
            GetComponent<SpriteRenderer>().flipX = xAxisValue < 0;
            rigidBody.velocity = new Vector2(xAxisValue * runningSpeed, rigidBody.velocity.y);
        }
    }

    void StopWalking()
    {
        rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
    }

    bool IsTouchingTheGround()
    {
        return Physics2D.Raycast(this.transform.position, 
                                Vector2.down,
                                1.5f,
                                groundMask);
    }

    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        this.healthPoints = INITIAL_HEALTH;
        this.manaPoints = INITIAL_MANA;

        Invoke("RestartPosition", 0.15f);
    }

    void RestartPosition()
    {
        this.transform.position = this.startPosition;
        this.rigidBody.velocity = Vector2.zero;
    }

    public void Die()
    {
        UpdateMaxScore();

        animator.SetBool(STATE_ALIVE, false);
        GameManager.INSTANCE.GameOver();
    }

    void UpdateMaxScore()
    {
        float travelledDistance = GetTravelledDistance();
        float maxScore = PlayerPrefs.GetFloat("maxScore", 0f);
        if (travelledDistance > maxScore)
        {
            PlayerPrefs.SetFloat("maxScore", travelledDistance);
        }
    }

    public void CollectHealth(int points)
    {
        this.healthPoints += points;

        if (this.healthPoints > MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;

        if (this.manaPoints > MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }
}
