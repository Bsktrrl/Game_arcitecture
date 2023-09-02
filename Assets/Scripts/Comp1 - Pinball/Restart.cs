using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigidbody = collision.rigidbody;

        if (otherRigidbody != null && otherRigidbody.gameObject.tag == "Ball")
        {
            print("Restart");

            LauncherManager.instance.Restart();
        }
    }
}
