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


    private float timer;
    private float maxTimer = 10;
    private bool attack;

    [SerializeField] private GameObject attackedThingy;

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



    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

       
        if(moveTime)
        {

            agent.SetDestination(positionToTravell);
            // moveTime = false;
            //lr.enabled = false;
            lineController(positionToTravell);

            timer -= Time.deltaTime;
        }


        if (attack)
        {
            if (Physics.Raycast(transform.position, attackedThingy.transform.position - transform.position, out hit, 1.5f))
            {
                if (hit.collider.gameObject == attackedThingy)
                {

                    Debug.Log("CockMother");

                    attackedThingy.GetComponent<Health>().IlooseHealth(1);
                    agent.SetDestination(transform.position); // agent.SetDestination(transform.position);
                    //moveTime = false;
                    attack = false;
                    

                }




            }
        }



        distance = Vector3.Distance(transform.position, new Vector3(positionToTravell.x, transform.position.y, transform.position.z));


         positiontoTravellVector = new Vector2(positionToTravell.x, positionToTravell.z);

         playerVector = new Vector2(transform.position.x, transform.position.z);

        if (ImAtLocation())
        {


           moveTime = false;
       


        }

        //if (attack && distance < 0.1)
        //{

        //    Debug.Log("Cock");
        //    moveTime = false;

        //    attackedThingy.GetComponent<Health>().IlooseHealth(1);
        //    attack = false;


        //}



        //if (Physics.Raycast(transform.position, attackedThingy.transform.position - transform.position, out hit, 2f))
        //{
        //    if (hit.collider.gameObject == attackedThingy)
        //    {

        //        Debug.Log("CockMother");


        //    }




        //}



        //TurnOrder.WormCount(ImDone());




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

        timer = maxTimer;

        positionToTravell = endpos;


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

        moveTime = true;
        DestoryChild();
    }

    public void  WhoiAttack(GameObject whom)
    {

        Debug.Log(whom.name);

        attackedThingy = whom;
        attack = true;
        //positionToTravell = whom.transform.position;
        MovePosition(whom.transform.position);
       // return attackedThingy;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
        
    //    if (collision.gameObject == attackedThingy)
    //    {

    //        Debug.Log("cockmuther");

    //    }


    //}




    //public void WhatLayerAmI()
    //{

    //    if (gameObject.tag == TurnOrder.CurrentPlayerName())
    //    {
    //        gameObject.layer = layerDefault;



    //    }
    //    if(gameObject.tag != TurnOrder.CurrentPlayerName())
    //    {

    //        gameObject.layer = layerPlayer;



    //    }



    //}
    public int ImDone()
    {

        if (ImAtLocation() && attack == false || timer < 0)
        {


            return 1;


        }
        else return 0;


       
    }

    public bool ImAtLocation()
    {

      


      return  playerVector == positiontoTravellVector;



        

    }


}
