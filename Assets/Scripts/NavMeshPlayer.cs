using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class NavMeshPlayer : MonoBehaviour
{

    // [SerializeField] private List<Vector3> movementList;
    // [SerializeField] private int clickerinos;

    // Start is called before the first frame update
    [SerializeField] private NavMeshAgent agent;

    private int intImAm;

    private LineRenderer lr;
    [SerializeField] private Vector3 positionToTravell;

    private bool moveTime;

    private int whosturn;

    private string whosTurn;

    GameObject MyChild;

    [SerializeField]private int layerDefault;
    [SerializeField] private int layerPlayer;

    [SerializeField] private float distance;
    Vector2 positiontoTravellVector;
    Vector2 playerVector;

    private bool readyToAttack;

    private float timer;
    [SerializeField] private float maxTimer = 10;
    private bool attack;


    private Vector3 whereToTravel;
    [SerializeField] private GameObject attackedThingy;


    private GameObject whereIDig;
    private bool ImGoingToDIg;


    void Start()
    {
        timer = maxTimer;
        agent = GetComponent<NavMeshAgent>();


        intImAm = 0;

        lr = gameObject.GetComponent<LineRenderer>();
        
        
        layerDefault = LayerMask.NameToLayer("Default");
        layerPlayer = LayerMask.NameToLayer("Player");


       // WhatLayerAmI();


        positionToTravell = transform.position;
        whereToTravel = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

       



        if (attack && attackedThingy != null)
        {
            if (Physics.Raycast(transform.position, attackedThingy.transform.position - transform.position, out hit, 1.5f))
            {
                if (hit.collider.gameObject == attackedThingy)
                {
                    
                  
                    

                    attackedThingy.GetComponent<Health>().IlooseHealth(1);
                    
                    //attackedThingy.GetComponent<Health>().IlooseHealth(1);
                    positionToTravell = transform.position;
                    
                    //agent.SetDestination(transform.position); // agent.SetDestination(transform.position);
                    //moveTime = false;
                    

                    readyToAttack = false;
                    attack = false;
                }




            }

        }
        
        
        if (moveTime)
        {

            agent.SetDestination(positionToTravell);
            // moveTime = false;
            //lr.enabled = false;
            lineController(positionToTravell);

            if (readyToAttack)
            {
                attack = true;

            }

            timer -= Time.deltaTime;
        }



        distance = Vector3.Distance(transform.position, new Vector3(positionToTravell.x, transform.position.y, transform.position.z));


         positiontoTravellVector = new Vector2(positionToTravell.x, positionToTravell.z);

         playerVector = new Vector2(transform.position.x, transform.position.z);

        if (ImAtLocation())
        {


           moveTime = false;
          // readyToAttack = false;


        }


    }




    public void lineController(Vector3 endpos)
    {
        if (!moveTime)
        {
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, endpos);

        }
        


    }
    public Vector3 MovePosition(Vector3 endpos)
    {

        

        whereToTravel = endpos;


        return positionToTravell;
    }

    public bool IsItTimeToMove(int whatintami)
    {
        if(gameObject.tag == "Player")
        {

            intImAm = 1;

        }

        return intImAm == whatintami;

    }

    public int TurnIsitNow(int turnFromTurnOrder)
    {
        whosturn = turnFromTurnOrder;
        

        return whosturn;
    }
    public string WhosTurn (string fromTurnOrder)
    {

        whosTurn = fromTurnOrder;
        return whosTurn;

    }
    public void DestoryChild()
    {
        Transform[] ts = GetComponentsInChildren<Transform>();
        foreach(Transform t in ts)
        {
            if(t.tag == "Destroyable")
            {

                MyChild = t.gameObject;
                Object.Destroy(MyChild);



            }

        }

    }

    public void MoveWorm()
    {
        timer = maxTimer;

        positionToTravell = whereToTravel;

        moveTime = true;

   

        DestoryChild();


        if (ImGoingToDIg)
        {
            whereIDig.GetComponent<HoleTeleport>().AddToList(gameObject);


        }


    }

    public void PositionChange(Vector3 ChangeMovementPlace)
    {

        positionToTravell = ChangeMovementPlace;


    }

    public void  WhoiAttack(GameObject whom)
    {

        Debug.Log("dad");

        attackedThingy = whom;
        readyToAttack = true;
        //positionToTravell = whom.transform.position;
        MovePosition(whom.transform.position);
       // return attackedThingy;
    }


    public int ImDone()
    {
       
        if (ImAtLocation())
        {


            return 1;


        }
        else return 0;


       
    }

    public bool ImAtLocation()
    {

      


      return  playerVector == positiontoTravellVector;



        

    }

    public void DoITravelThroughHole(GameObject digLocation)
    {
        whereIDig = digLocation;
        ImGoingToDIg = true;

    }





}
