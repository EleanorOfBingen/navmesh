using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] private GameObject target;

    private int bulletAmount = 2;
    private int bulletpain = 1;



    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject childBullet;
    // Start is called before the first frame update

    [SerializeField] private bool shotsFired;

    public void Shoot()
    {
        Debug.Log("PewPew");
        //target.GetComponent<Health>().IlooseHealth(bulletpain);
        
        childBullet = Instantiate(bullet, transform.position, transform.rotation, transform);

        childBullet.GetComponent<BulletShot>().Target(target.transform.position, gameObject);

        shotsFired = true;

        bulletAmount--;
        Debug.Log(bulletAmount);
        Debug.Log(AmIOutOfAmmo());
    }

    public GameObject WhoAmIShooting(GameObject whomstIShoot)
    {


        return target = whomstIShoot; 
    }
    public void Hit()
    {

      
        
        target.GetComponent<Health>().IlooseHealth(bulletpain);
        shotsFired = false;
        Destroy(childBullet);

    }


    public int ShotsDone()
    {
        if(shotsFired == false)
        {


            return 1;

        }
        else
        {

            return 0;
        }


    }
    public bool AmIOutOfAmmo()
    {

        return bulletAmount < 1;
        
    }

    public int HowMushAmmo()
    {

        return bulletAmount;

    }


}
