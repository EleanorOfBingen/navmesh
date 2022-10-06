using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoleTeleport : MonoBehaviour
{
    private NavMeshPlayer nmPlayer;
    public List<GameObject> digginDwarfs;

    [SerializeField]private Vector3 digExit;


    private NavMeshAgent nma;
    private float[] directionMovement;

    // Start is called before the first frame update
    void Start()
    {
        directionMovement = new float[] { -1.5f, -1, 1, 1.5f };

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (digginDwarfs.Contains(collision.gameObject) && !TurnOrder.PlayerAnimationDone())
        {
            Debug.Log("diggydiggyhole" + collision.name);
            nma = collision.GetComponent<NavMeshAgent>();
            nma.Warp(digExit);
            nmPlayer = collision.GetComponent<NavMeshPlayer>();
            //nmPlayer.MovePosition(digExit + new Vector3(directionMovement[Random.Range(0, directionMovement.Length)], 0, directionMovement[Random.Range(0, directionMovement.Length)]));

            //nmPlayer.MoveWorm();

            nmPlayer.PositionChange(digExit + new Vector3(directionMovement[Random.Range(0, directionMovement.Length)], 0, directionMovement[Random.Range(0, directionMovement.Length)]));
            
            digginDwarfs.Remove(collision.gameObject);


        }


    }


    public Vector3 DigExitPoint(Vector3 dExit)
    {
        digExit = dExit;


        return digExit;
    }

    public void AddToList(GameObject CurrentDwarf)
    {

        Debug.Log(CurrentDwarf);
        digginDwarfs.Add(CurrentDwarf);

    }

    public void RemoveToList(GameObject DwarfCurrent)
    {
        if (digginDwarfs.Contains(DwarfCurrent))
        {

            digginDwarfs.Remove(DwarfCurrent);

        }


    }
    

    
    

}
