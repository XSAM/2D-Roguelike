using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public float turnDelay = 0.4f;
    public static GameManager instance = null;
    private BoardManager boardScript;
    private int level = 3;
    public int playerFoodPoint = 100;
    [HideInInspector]
    public bool playersTurn = true;

    [HideInInspector]public List<Enemy> enemies;
    private bool enemiesMoving;
    

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    
    public void Start()
    {
        enabled=true;;
        Debug.Log("Invoke");
        boardScript = GetComponent<BoardManager>();
        enemiesMoving = false;
        InitGame();
    }

    void InitGame()
    {
        //enemies.Clear();
        boardScript.SetupScene(level);
    }

    public void GameOver()
    {
        Debug.Log("Ri");
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("GameManager.instance.playerFoodPoint:" + GameManager.instance.playerFoodPoint);
        if(playersTurn||enemiesMoving)
            return;
        StartCoroutine(MoveEnemies());
    }
    
    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }
    
    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        Debug.Log("DelayTime:" + turnDelay);
        yield return new WaitForSeconds(turnDelay);
        if(enemies.Count==0)
            yield return new WaitForSeconds(turnDelay);
        for(int i=0;i<enemies.Count;++i)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        Debug.Log("Enemy:" + Time.realtimeSinceStartup);
        playersTurn = true;
        enemiesMoving = false;
    }
}
