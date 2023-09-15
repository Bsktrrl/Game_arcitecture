using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] TriangleSurface meshObject;

    [Header("Ball Physics")]
    Vector3 gravity = Physics.gravity;
    float m = 1;
    Vector3 velocity = Vector3.zero;
    [SerializeField] [Range(0, 1)] float bouncyness = 0.6f;
    [SerializeField] float radius = 2;


    //--------------------


    private void FixedUpdate()
    {
        Move();
    }


    //--------------------


    void Move()
    {
        Vector3 forceSum = m * gravity; //G = m * g

        var pos = transform.position;
        var pos2D = new Vector2(pos.x, pos.z);

        var hit = meshObject.GetCollision(pos2D);

        if (hit.isHit /*&& Vector3.Distance(hit.position, pos) < radius*/)
        {
            print("Hit: " + hit.normal + " | Pos: " + pos);
            
            //Calculate normal forces
            forceSum += Vector3.Dot(forceSum, hit.normal) * hit.normal; //forceSum = G

            //Change velocity to go in the direction of the plane
            velocity = Vector3.ProjectOnPlane(velocity, hit.normal);
        }

        Vector3 A = forceSum / m; //(N + G) / m
        velocity += A * Time.fixedDeltaTime;

        transform.position += velocity * Time.fixedDeltaTime;

    }
}
