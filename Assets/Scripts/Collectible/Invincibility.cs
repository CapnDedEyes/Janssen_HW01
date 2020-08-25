using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{

    protected override void _PowerUp(Player player)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        player.PowerUp();

    }

    protected override void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler
            (0, MovementSpeed, MovementSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
}
