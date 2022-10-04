using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{

    private Vector3 whereItravel;
    [SerializeField]private bool killMe;
    GameObject myWorm;
    // Start is called before the first frame update
    void Start()
    {

        gameObject.tag = transform.parent.tag;
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, whereItravel, 10f *Time.deltaTime);

        //if (killMe)
        //{

        //    DestroyImmediate(gameObject);

        //}

    }


    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("FuckIHit2");
        if (other.tag != gameObject.tag)
        {

            Debug.Log("FuckIHit");
            Shooter Father = myWorm.GetComponentInParent<Shooter>();
            Father.Hit();
            Destroy(transform.gameObject);



        }
      




    }

    public void  Target(Vector3 TargetWorm, GameObject MyWorm)
    {


        whereItravel = TargetWorm;
        myWorm = MyWorm;
    }


}
