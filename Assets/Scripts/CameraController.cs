using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform ground;

    float zoomFloat;

    private Camera cam;

    private float targetZoom;
    private float zoomSpeed = 3;
    [SerializeField] private float zoomLerpSpeed = 10;

    private CinemachineVirtualCamera cvc;

    [SerializeField] private GameObject[] cubes;
    [SerializeField] private int wurmInt;

    private Transform initalTransform;

    private bool startvalue;
    private void Start()
    {

        cam = Camera.main;
        zoomFloat = cam.orthographicSize;
        cvc = cam.GetComponent<CinemachineVirtualCamera>();
        ChangeTagForFocus();
        initalTransform = transform;

       


    }

    void Update()
    {

      


        if (TurnOrder.PlayerAnimationDone())
           {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Vector3 mouseMove = Camera.main.WorldToScreenPoint(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                //{



                // transform.position = hit.point;


                //}

                wurmInt = Mathf.Clamp(wurmInt, 0, (cubes.Length - 1));

            if (Input.GetMouseButton(1))
            {

                //cvc.Follow = transform;
                //transform.position = Vector3.MoveTowards(transform.position, mouseMove, 1f);



                if (Physics.Raycast(ray, out hit))
                {


                    cvc.Follow = transform;
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, 1f);






                }


            }
            else
            {

                cvc.Follow = cubes[wurmInt].transform;

                if (startvalue)
                {
                    targetZoom = 10f;
                    startvalue = false;
                }

            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                wurmInt -= 1;


            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                wurmInt += 1;


            }
        



            float scrolldata;
            scrolldata = Input.GetAxis("Mouse ScrollWheel");
            //Debug.Log(scrolldata);

            targetZoom -= scrolldata * zoomSpeed;
           

        }
        else
        {

            cvc.Follow = initalTransform;
            targetZoom = 20f;
            startvalue=true;


        }
        targetZoom = Mathf.Clamp(targetZoom, 5f, 20f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);








    }

    public void ChangeTagForFocus()
    {
        
        
        cubes = GameObject.FindGameObjectsWithTag(TurnOrder.CurrentPlayerName());
        wurmInt = 0;
        



            //cvc.Follow = cubes[0].transform;

    }



}
