using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject chesspiece;

    //Matrices needed, positions of each of the GameObjects
    //Also separate arrays for the players in order to easily keep track of them all
    //Keep in mind that the same objects are going to be in "positions" and "playerBlack"/"playerWhite"
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    //current turn
    private string currentPlayer = "white";

    //Game Ending
    private bool gameOver = false;

    private float stage = 1;
    public static float WEnemy = 0;
    private float maxStage = 2;

    public void Awake()
    {
        stage = 1;
        WEnemy = 0;
    }

    //Unity calls this right when the game starts, there are a few built in functions
    //that Unity can call for you
    public void Start()
    {
        if(stage == 1)
        {
            Stage1();
        }
    }

    public void Stage1()
    {
        WEnemy = 1;

        playerWhite = new GameObject[] { Create("white_pawn", 0, 0) };
        playerBlack = new GameObject[] { Create("black_pawn", 1, 2) };

        SetPosition(playerBlack[0]);
        SetPosition(playerWhite[0]);
    }
    public void Stage2()
    {
        WEnemy = 2;

        playerWhite = new GameObject[] { Create("white_knight", 0, 0) };
        playerBlack = new GameObject[] { Create("black_pawn", 1, 2), Create("black_pawn", 4, 5) };

        SetPosition(playerWhite[0]);
        for(int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
        }
        
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        pon cm = obj.GetComponent<pon>(); //We have access to the GameObject, we need the script
        cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        pon cm = obj.GetComponent<pon>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void Update()
    {
        if(WEnemy == 0)
        {
            NextStage();
        }
    }

    public void NextStage()
    {
        if (stage == maxStage)
            Winner(currentPlayer);
        Destroy(playerWhite[0]);
        WEnemy = 100;
        stage++;
        if (stage == 2)
        {
            Stage2();
        }
    }
    
    public void Winner(string playerWinner)
    {
        gameOver = true;

        //Using UnityEngine.UI is needed here
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}
