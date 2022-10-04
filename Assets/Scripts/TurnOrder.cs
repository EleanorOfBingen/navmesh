using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : MonoBehaviour
{
    static TurnOrder instance; 
    [SerializeField]private GameObject[] player1;

    
    private string playerName;
    private int currentPlayer;

    [SerializeField] int currentAmountOfPlayers = 3;
    private ControllerNavMesh conNavMesh;
    [SerializeField] private int worms;
    [SerializeField] private int wormsPlayed;


    [SerializeField] string action1 = "Move";
    [SerializeField] string action2 = "Attack";
    [SerializeField] string action3 = "Dig";
    [SerializeField] string action4;


    [SerializeField] float timer;
    [SerializeField] float timerMax = 5f;


    [SerializeField] bool endTurn;

    [SerializeField] private CameraController cc;
    private void Awake()
    {
        
        if(instance == null)
        {

            instance = this;

        }

        //player1 = GameObject.FindGameObjectsWithTag(CurrentPlayerName());
    }


    // Start is called before the first frame update
    void Start()
    {

       // CameraController cameraController = GetComponent<CameraController>();
        conNavMesh = GetComponent<ControllerNavMesh>();

        currentPlayer = 0;

        //SetAllWorms();

        //Debug.Log(CurrentPlayerName());

        //player1 = GameObject.FindGameObjectsWithTag(CurrentPlayerName());

        timer = timerMax;
        conNavMesh.RestartFocus();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && PlayerAnimationDone())
        {

            //Debug.Log("FINDTHEWORMS");
            //FindAllWorms();
            endTurn = true;
          
        }

        if (endTurn)
        {

            timer -= Time.deltaTime;

        }
        else
        {

            timer = timerMax;

        }


        if(timer < 0 && endTurn)
        {

            FindAllWorms();
            //cc.ChangeTagForFocus();
            conNavMesh.RestartFocus();
        }


         wormsPlayed = WormCount(0);

        //player1 = GameObject.FindGameObjectsWithTag(CurrentPlayerName());

 


    }

    public static TurnOrder GetInstance()
    {


        return instance;
    }


    void FindAllWorms()
    {

        //Debug.Log("komsefgmkoå");
        worms = 0;
        player1 = GameObject.FindGameObjectsWithTag(CurrentPlayerName());
        Debug.Log(CurrentPlayerName());


        foreach ( GameObject worm in player1)
        {
            worms += 1;
            worm.GetComponent<WhatIDo>().InstantiateAction();
            worm.GetComponent<WhatIDo>().WhatActionDoIDo(action1);
            //worm.GetComponent<NavMeshPlayer>().WhatLayerAmI();
           

        }
        

        currentPlayer += 1;
        currentPlayer %= currentAmountOfPlayers;
        

        endTurn = false;

    }
    //void SetAllWorms()
    //{

    //    conNavMesh.WhosTurn(CurrentPlayerName());
        



    //}

    public static string CurrentPlayerName()
    {
         
        TurnOrder tO = GetInstance();
        return "Player" + tO.currentPlayer;
        
    }

    public static bool PlayerAnimationDone()
    {
        
        TurnOrder tO = GetInstance();
        return tO.worms == tO.wormsPlayed && tO.endTurn == false;

    }

    public int WormCount(int wormsdone)
    {
        
       // TurnOrder tO = GetInstance();
       foreach (GameObject worm in player1)
        {

            wormsdone += worm.GetComponent<WhatIDo>().WormDone();
           // Debug.Log("thishappens");
        }

        return wormsdone;
    }

    public static string Action1()
    {

        TurnOrder tO = GetInstance();
        return tO.action1;

    }
    public static string Action2()
    {

        TurnOrder tO = GetInstance();
        return tO.action2;

    }
    public static string Action3()
    {

        TurnOrder tO = GetInstance();
        return tO.action3;

    }


    public static GameObject FocusedWurm(int wurmNmr)
    {
        TurnOrder tO = GetInstance();
        return tO.player1[wurmNmr];


    }
    public static int AmountWormsCurrentPlayers()
    {
        TurnOrder tO = GetInstance();
        return tO.player1.Length;


    }

    






}
