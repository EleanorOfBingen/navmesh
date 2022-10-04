using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class WhatIDo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string action;

    private Shooter shooter;
    private NavMeshPlayer nmp;
    private Attack attack;
    private Dig diggin;

    [SerializeField] private string WhatTypeAmAI;

    [SerializeField] private String[] whatICanDo;
    private int whatID;

    [SerializeField] private TMP_Text textstring;

    private void Start()
    {

        whatICanDo = new String[]{"Attacker", "Canon"};


        shooter = GetComponent<Shooter>();
        nmp = GetComponent<NavMeshPlayer>();
        attack = GetComponent<Attack>();
        diggin = GetComponent<Dig>();

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

            if (WhatTypeAmAI == "Canon")
            {
                shooter.Shoot();
            }
            else
            {
                nmp.MoveWorm();


            }


        }
        if(action == TurnOrder.Action3())
        {
            diggin.MoveTowardsDiggin();


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
            if (WhatTypeAmAI == "Canon")
            {
                return GetComponent<Shooter>().ShotsDone();
            }
            else
            {
                return GetComponent<NavMeshPlayer>().ImDone();


            }
           // return GetComponent<Shooter>().ShotsDone();

        }

        if (action == TurnOrder.Action3())
        {

            return GetComponent<Dig>().DiggingDone();

        }
        return 0;



    }
    public string WhatAmI()
    {


        return WhatTypeAmAI;
    }

    public void ChangeType(int changnmbr)
    {

        whatID += changnmbr;
        whatID = Mathf.Clamp(whatID, 0, (whatICanDo.Length - 1));

        WhatTypeAmAI = whatICanDo[whatID];
        textstring.text = WhatTypeAmAI;

    }

    






}
