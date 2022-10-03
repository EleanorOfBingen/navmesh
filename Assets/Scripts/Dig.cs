using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dig : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]private Vector3 digEntrence;
    [SerializeField] private Vector3 digExit;

    private bool moveToDig;
    private bool digginDone;

    private NavMeshAgent nma;
    private NavMeshPlayer nmPlayer;

    private bool amther;
    Vector2 playerVector;
    Vector2 positiontoTravellVector;

    private float timmer;
    private float maxtimmer;

    [SerializeField] private GameObject hole;

    private float[] directionMovement;
 


    void Start()
    {

        directionMovement = new float[]{-1.5f,-1,1,1.5f};
       

        nma = GetComponent<NavMeshAgent>();
        nmPlayer = GetComponent<NavMeshPlayer>();


    }

    // Update is called once per frame
    void Update()
    {

        // positiontoTravellVector = new Vector2(digEntrence.x, digEntrence.z);

        // playerVector = new Vector2(transform.position.x, transform.position.z);


        //if (moveToDig)
        //{

        //    Debug.Log("Dig2");




        //    if (amther)
        //    {

        //        nma.Warp(digExit);
        //        Debug.Log("Dig3");
        //        nmPlayer.MovePosition(digExit);

        //        moveToDig = false;

        //    }

        //}
        //if (playerVector == positiontoTravellVector)
        //{
        //    amther = true;


        //}


        if (moveToDig)
        {

            nma.Warp(digExit);
            nmPlayer.MovePosition(digExit);
            Debug.Log("Dig3");
            digginDone = true;
            moveToDig = false;
        }

        if (digginDone)
        {

            nmPlayer.MovePosition(digExit + new Vector3(directionMovement[Random.Range(0,directionMovement.Length)],0, directionMovement[Random.Range(0, directionMovement.Length)]));
            nmPlayer.MoveWorm();
            
            CreateDigginHoles();
            digginDone = false;
            
        }
        






    }

    public void SetEntrence(Vector3 entrence)
    {


        digEntrence = entrence;

    }

    public void SetExit(Vector3 exit)
    {

        digExit = exit;

    }

    public void MoveTowardsDiggin()
    {
        nmPlayer.MovePosition(digEntrence);
        nmPlayer.MoveWorm();
        moveToDig = true;
        Debug.Log("Dig1");

    }

    public int DiggingDone()
    {
        if(moveToDig == false && digginDone == false)
        {

            return 1;

        }
        return 0;

        
    }
    
    public void CreateDigginHoles()
    {
        GameObject firstObject = Instantiate(hole, digEntrence, transform.rotation);
        firstObject.GetComponent<HoleTeleport>().DigExitPoint(digExit);

        GameObject secondObject = Instantiate(hole, digExit, transform.rotation);
        secondObject.GetComponent<HoleTeleport>().DigExitPoint(digEntrence);
    }

}
