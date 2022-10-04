using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ControllerNavMesh : MonoBehaviour
{

    [SerializeField] private GameObject aimer;
    [SerializeField] private GameObject pointOFMovement;
    //[SerializeField] private bool created;
    [SerializeField] private GameObject moveAimer;
    [SerializeField] private LayerMask mask;


    [SerializeField] private bool hitPlayer;
    [SerializeField] private GameObject currentWorm;
    //[SerializeField] private GameObject targetWorm;

    private NavMeshPlayer nmp;

    [SerializeField]private bool iShoot;

    [SerializeField] GameObject[] worms;

    [SerializeField] private WhatIDo wid;
    private LineRendCon lrc;


    [SerializeField]private float distance;
    private float maxDistance = 10;
    private float shootDistance = 50;


   
    [SerializeField] float distance2 = 50f;
    RaycastHit hit2;
    Vector3 playerPoint;


    [SerializeField]private GameObject coursor;

    [SerializeField] private bool pressedDown;
    RaycastHit hit;

    [SerializeField] private bool ihitwall;

    [SerializeField] private bool dig;
    private bool digLocationPlaced;
    private Vector3 digLocation;
    [SerializeField] private bool imGoingToEnterDigHole;
    [SerializeField] private GameObject digHole;
    private HoleTeleport ht;

    private Dig digger;

    //RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        lrc = GetComponent<LineRendCon>();
        //moveAimer = Instantiate(aimer);
        //moveAimer.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 hitvector = coursor.transform.position;
        //RaycastHit hit2;
        //Vector3 playerPoint = Camera.main.ScreenPointToRay(currentWorm.transform.position);



        //if (Physics.Raycast(ray, out hit, 100.0f))
        //{

        //    playerPoint = hit.point;

        //}




        if (currentWorm == null)
        {


        }
        else if(pressedDown)
        {

            Debug.Log("Cokc");

            lrc.LineRenderPositions(currentWorm.transform.position, hitvector, AmIOutOfRange("", ihitwall));


        }else if (dig)
        {

            lrc.LineRenderPositions(currentWorm.transform.position, hitvector, AmIOutOfRange("", ihitwall));

        }
        else
        {


        }




        if (Physics.Raycast(ray, out hit ,100.0f) && TurnOrder.PlayerAnimationDone())
        {
            //if (dig)
            //{

            //   // lrc.LineRenderPositions(currentWorm.transform.position, hitvector, AmIOutOfRange(wid.WhatAmI(), ihitwall));
            //}


            if(hit.collider.tag == TurnOrder.CurrentPlayerName() && Input.GetMouseButtonDown(0))
            {

                PressedWurm(hit.collider.gameObject);

            }
            //if (digLocationPlaced)
            //{

            //    if (Input.GetMouseButtonDown(0))
            //    {

            //        // PressedWurm(hit.collider.gameObject);



            //        //digLocation = hit.point;
            //        digLocation = hit.point;
            //        InstantiateDigLocation(digLocation);
            //        digLocationPlaced = false;
            //        ////dig = false;
            //        TurnOfCollider(false);
            //        digger.SetExit(digLocation);
            //        wid.WhatActionDoIDo(TurnOrder.Action3());

            //    }

            //}
            if (dig && Input.GetMouseButtonDown(0))
            {

                // PressedWurm(hit.collider.gameObject);

                if (!AmIOutOfRange("", ihitwall))
                {

                    digLocation = hit.point;
                    InstantiateDigLocation(digLocation);
                    
                    digger.SetExit(digLocation);
                    digger.SetEntrence(currentWorm.transform.position);
                    wid.WhatActionDoIDo(TurnOrder.Action3());
                    TurnOfCollider(false);
                    dig = false;
                }
            }





            if (hitPlayer)
            {

                playerPoint = hit.point;
                //playerPoint = Camera.main.WorldToScreenPoint(currentWorm.transform.position);
                //Vector3 distanceMousePlayer = Input.mousePosition - playerPoint;
                //float angle = Mathf.Atan2(distanceMousePlayer.x, distanceMousePlayer.y) * Mathf.Rad2Deg;
                //if (angle < 0.0f) { angle += 360.0f; }

                //currentWorm.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));



                distance = Vector3.Distance(currentWorm.transform.position, hit.point);

                //Debug.Log(hit.collider.name);

                // SetActiveAimer(playerPoint);

                if (hit.collider.tag == "Hole")
                {
                    //digHole = hit.collider.gameObject;
                    imGoingToEnterDigHole = true;
                    //lrc.LineRenderPositions(currentWorm.transform.position, hit.collider.gameObject.transform.position, AmIOutOfRange("", ihitwall));

                }
                else
                {

                    imGoingToEnterDigHole = false;


                }

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    
                    //Debug.Log(hit.collider.tag);
                    iShoot = true;
                 
                    if (Physics.Raycast(currentWorm.transform.position, hit.collider.gameObject.transform.position - currentWorm.transform.position, out hit2, distance))
                    {
                        if (hit2.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                        {
                           
                            ihitwall = false;


                        }
                        else
                        {
                           
                            ihitwall = true; 

                        }


                       
                       // lrc.LineRenderPositions(currentWorm.transform.position, hit.collider.gameObject.transform.position, AmIOutOfRange(0));
                        

                    }
                    lrc.LineRenderPositions(currentWorm.transform.position, hit.collider.gameObject.transform.position, AmIOutOfRange(wid.WhatAmI(), ihitwall));
                }
                else
                {
                   // lrc.LineRenderPositions(currentWorm.transform.position, hitvector, AmIOutOfRange());
                    iShoot = false;

                }


                //if (AmIOutOfRange())
                //{

                //    Debug.Log("YEAH");

                //}
                //else
                //{

                //    Debug.Log("Nah");

                //}

                //if (Physics.Raycast(currentWorm.transform.position, Input.mousePosition - currentWorm.transform.position, out hit2, distance, mask))
                //{
                //    Debug.Log(hit2.collider.tag) ;


                //}

            }



            else
            {
                if (!dig)
                {
                    lrc.LineRenderPositions(currentWorm.transform.position, currentWorm.transform.position, AmIOutOfRange("", false));
                }

            }




            if (Input.GetMouseButtonUp(0))
            {
                if (pressedDown)
                {
                    Debug.Log("YEE");
                    /// CHEK IF I HIT SOMETHING ELENOR


                    pressedDown = false;

                    TurnOfCollider(false);


                    if (iShoot)
                    {
                        if (!AmIOutOfRange(wid.WhatAmI(), ihitwall))
                        {



                            PointOfShot(hit.collider.gameObject, wid.WhatAmI());

                        }

                        //if (!iShoot)
                        //{




                        //    InstantatePointOfMovement(hit.point);
                        //}

                    }
                    else if (!AmIOutOfRange("", ihitwall))
                    {
                        if (imGoingToEnterDigHole)
                        {

                            digHole = hit.collider.gameObject;
                            Debug.Log("HighTide");
                            digHole.GetComponent<HoleTeleport>().AddToList(currentWorm);





                        }
                        else if(digHole != null)
                        {
                            digHole = hit.collider.gameObject;
                            digHole.GetComponent<HoleTeleport>().RemoveToList(currentWorm);


                        }
                        
                        InstantatePointOfMovement(hit.point);
                        //if (imGoingToEnterDigHole)
                        //{
                        //    digHole = hit.collider.gameObject;
                        //    digHole.GetComponent<HoleTeleport>().AddToList(currentWorm);

                        //    InstantatePointOfMovement(hit.point);
                        //}
                        //else
                        //{

                        //    InstantatePointOfMovement(hit.point);

                        //}

                        // InstantatePointOfMovement(hit.point);



                    }

                    else
                    {

                        InstantatePointOfMovement(currentWorm.transform.position);



                    }
                   

                   

                    // moveAimer.SetActive(false);

                    //    worms = GameObject.FindGameObjectsWithTag(TurnOrder.CurrentPlayerName());

                    //    foreach (GameObject worm in worms)
                    //    {

                    //        worm.GetComponent<BoxCollider>().enabled = true;

                    //    }
                }
               

                TurnOfCollider(false);
                hitPlayer = false;
                pressedDown = false;
            }


            }


      



    }

    //public void SetActiveAimer(Vector3 mousePos)
    //{

    //    moveAimer.SetActive(true);
    //    moveAimer.transform.position = mousePos;

    //}


    public bool raycastHiting(RaycastHit ray)
    {


        return ray.collider != null;
    }

    void InstantatePointOfMovement(Vector3 positionInstantiate)
    {
        //curre
      
        Instantiate(pointOFMovement, new Vector3(positionInstantiate.x, positionInstantiate.y , positionInstantiate.z), transform.rotation, currentWorm.transform);
        nmp.MovePosition(positionInstantiate);
        wid.WhatActionDoIDo(TurnOrder.Action1());


        //hitPlayer = false;
        // pointOFMovement.transform.parent = currentWorm.transform;


    }




    void InstantiateDigLocation(Vector3 positionInstantiate)
    {
        Instantiate(pointOFMovement, new Vector3(positionInstantiate.x, positionInstantiate.y, positionInstantiate.z), transform.rotation, currentWorm.transform);


    }


    void PointOfShot(GameObject targetWorm, string playerType)
    {
        if (playerType == "Canon")
        {
            currentWorm.GetComponent<Shooter>().WhoAmIShooting(targetWorm);
           
        }
        else
        {
            //nmp.MovePosition(targetWorm.transform.position);
            nmp.WhoiAttack(targetWorm);

        }

        wid.WhatActionDoIDo(TurnOrder.Action2());

        // wid.WhatActionDoIDo(TurnOrder.Action2());
        //hitPlayer = false;
    }

    bool AmIOutOfRange(string playerType, bool hittWall)
    {

        //if (Mdistance == maxDistance && hit.collider.tag != "underground")
        //{


        //    return distance > Mdistance;
        //}

        //if (iShoot && notHittWall)
        //{

        //    return distance > MDistance;

        //}
        if (playerType == "Canon" && hittWall)
        {

            return true;

        }else if (playerType == "Canon" && !hittWall)
        {


            return distance > shootDistance;
        }
        else
        {

            return distance > maxDistance;

        }
        



        

    }

    private void PressedWurm(GameObject wurmorino)
    {


        Debug.Log("YE");

        hitPlayer = true;
        currentWorm = wurmorino;
        nmp = currentWorm.GetComponent<NavMeshPlayer>();
        nmp.DestoryChild();
        wid = currentWorm.GetComponent<WhatIDo>();
        //worms = GameObject.FindGameObjectsWithTag(TurnOrder.CurrentPlayerName());
        pressedDown = true;

        TurnOfCollider(true);
        //foreach (GameObject worm in worms)
        //{

        //    worm.GetComponent<BoxCollider>().enabled = false;

        //}

    }

    bool AmIHittingWall()
    {


        return true;

    }


    public void ClickToDig(Button digbutton)
    {
        //GameObject father = digbutton.gameObject.transform.parent.parent.gameObject;
        //PressedWurm(father);
        
        currentWorm = digbutton.gameObject.transform.parent.parent.gameObject;
        dig = true;
        nmp = currentWorm.GetComponent<NavMeshPlayer>();
        nmp.DestoryChild();
        wid = currentWorm.GetComponent<WhatIDo>();
        digger = currentWorm.GetComponent<Dig>();

        TurnOfCollider(true);
        // worms = GameObject.FindGameObjectsWithTag(TurnOrder.CurrentPlayerName());
        //// pressedDown = true;


        // foreach (GameObject worm in worms)
        // {

        //     worm.GetComponent<BoxCollider>().enabled = false;

        // }
    }

    public void TurnOfCollider(bool turnOff)
    {

        worms = GameObject.FindGameObjectsWithTag(TurnOrder.CurrentPlayerName());
        // pressedDown = true;

        if (turnOff)
        {
            foreach (GameObject worm in worms)
            {

                worm.GetComponent<BoxCollider>().enabled = false;

            }
        }
        if (!turnOff)
        {
            foreach (GameObject worm in worms)
            {

                worm.GetComponent<BoxCollider>().enabled = true;

            }
        }



    }


    //bool canIHitWorm()
    //{



    //}













}
