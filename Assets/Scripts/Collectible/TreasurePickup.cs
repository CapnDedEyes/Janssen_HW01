using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasurePickup : CollectibleBase
{
    [SerializeField] int _treasureAdded = 1;

    protected override void Collect(Player player)
    {
        player.IncreaseTreasure(_treasureAdded);
    }

    protected override void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler
            (MovementSpeed, MovementSpeed, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
}
