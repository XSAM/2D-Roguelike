using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MovingObject 
{
    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int food;
	
	// Use this for initialization
	protected override void Start () 
	{
        GameManager.instance.enabled = true;
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoint;
        base.Start();
	}

    private void OnDisable()
    {
        Debug.Log("disable:" + food);
        GameManager.instance.playerFoodPoint = food;
    }

    void OnDestroy()
    {
        Debug.Log("Destroy");
    }
	
	// Update is called once per frame
	void Update () 
	{
        if (!GameManager.instance.playersTurn)
            return;
        int horizontal = 0;
        int vertical = 0;

        if(Input.GetButtonDown("Horizontal")||Input.GetButtonDown("Vertical"))
        {
            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");
        }


        if (horizontal != 0)
            vertical = 0;
        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }
            
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;
        base.AttemptMove<T>(xDir, yDir);
        //RaycastHit2D hit;
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled=false;
        }
        else if(other.tag=="Food")
        {
            food += pointsPerFood;
            other.gameObject.SetActive(false);
        }
        else if(other.tag=="Soda")
        {
            food += pointsPerSoda;
            other.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
        //throw new System.NotImplementedException();
    }

    private void Restart()
    {
        GameManager.instance.enemies = new List<Enemy>();
        GameManager.instance.enabled = false;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("playerHit");
        food -= loss;
        CheckIfGameOver();
    }


    private void CheckIfGameOver()
    {
        if (food <= 0)
            GameManager.instance.GameOver();
    }
}
