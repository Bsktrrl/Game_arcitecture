using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float launchPadForce = 100;
    public float pushStrength = 5;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigidbody = collision.rigidbody;

        if (otherRigidbody != null && otherRigidbody.gameObject.tag == "Ball")
        {
            print("Hit the Ball");

            Vector3 impactPoint = collision.contacts[0].point;
            Vector3 direction = otherRigidbody.position - impactPoint; // from point of impact to Center of target
            direction.Normalize();

            otherRigidbody.AddForce(direction * pushStrength, ForceMode.Impulse);
            otherRigidbody.transform.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.Normalize(this.transform.GetComponent<Rigidbody>().velocity) * launchPadForce);
        }
    }
}
