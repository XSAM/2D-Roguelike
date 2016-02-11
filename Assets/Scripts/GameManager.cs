using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance=null;
    private BoardManager boardScript;
    private int level = 3;
    
    void Awake()
    {
        if(instance==null)
            instance = null;
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }
    
    void InitGame()
    {
        boardScript.SetupScene(level);
    }
    
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
