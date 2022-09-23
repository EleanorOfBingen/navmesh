using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class WhatIDo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string action;

    private Shooter shooter;
    private NavMeshPlayer nmp;

    private void Start()
    {

        shooter = GetComponent<Shooter>();
        nmp = GetComponent<NavMeshPlayer>();



    }


    public string WhatActionDoIDo(string actionString)
    {

        return action = actionString;

    }

    public void InstantiateAction()
    {
        //Move
        if (action == TurnOrder.Action1())
        {

            nmp.MoveWorm();

        }

        //Shoot
        if(action == TurnOrder.Action2())
        {
            shooter.Shoot();


        }


    }

    public int WormDone()
    {
        if(action == TurnOrder.Action1())
        {

            return GetComponent<NavMeshPlayer>().ImDone();

        }
        if (action == TurnOrder.Action2())
        {

            return GetComponent<Shooter>().ShotsDone();

        }
        return 0;



    }






}
