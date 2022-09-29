using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoleTeleport : MonoBehaviour
{

    private List<GameObject> digginDwarfs;

    [SerializeField]private Vector3 digExit;


    private NavMeshAgent nma;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (digginDwarfs.Contains(collision.gameObject))
        {

            nma = collision.GetComponent<NavMeshAgent>();
            nma.Warp(digExit);
            digginDwarfs.Remove(collision.gameObject);


        }


    }


    public Vector3 DigExitPoint(Vector3 dExit)
    {
        digExit = dExit;


        return digExit;
    }

    public void AddToList(GameObject dwarfToDig)
    {


        digginDwarfs.Add(dwarfToDig);

    }
    

}
