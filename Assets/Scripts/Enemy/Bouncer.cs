using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    [SerializeField] int bounceForce = 500;

    protected override void PlayerImpact(Player player)
    {
        //base.PlayerImpact(player);
        //player.Bounce();
        player.GetComponent<Rigidbody>().AddExplosionForce(bounceForce, transform.position, 1);
        Debug.Log("Boing!");
    }
}
