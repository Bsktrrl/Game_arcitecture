using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperManager : MonoBehaviour
{
    [SerializeField] GameObject bumper_left;
    Quaternion bumper_left_Rot;

    [SerializeField] GameObject bumper_right;
    Quaternion bumper_right_Rot;

    public float speed = 750;


    //--------------------


    private void Start()
    {
        bumper_left_Rot = bumper_left.transform.rotation;
        bumper_right_Rot = bumper_right.transform.rotation;
    }
    private void Update()
    {
        BumperActivity();
    }


    //--------------------


    void BumperActivity()
    {
        //Left Bumper
        if (Input.GetKey(KeyCode.A))
        {
            if (bumper_left.transform.rotation.z > 40)
            {
                bumper_left.transform.rotation = new Quaternion(0, 0, 40, 0);
            }
            else
            {
                bumper_left.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            }
        }
        else if (!Input.GetKeyUp(KeyCode.A))
        {
            bumper_left.transform.rotation = bumper_left_Rot;
        }

        //Right Bumper
        if (Input.GetKey(KeyCode.D))
        {
            if (bumper_right.transform.rotation.z > 40)
            {
                bumper_right.transform.rotation = new Quaternion(0, 0, 40, 0);
            }
            else
            {
                bumper_right.transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
            }
        }
        else if (!Input.GetKeyUp(KeyCode.D))
        {
            bumper_right.transform.rotation = bumper_right_Rot;
        }
    }
}
