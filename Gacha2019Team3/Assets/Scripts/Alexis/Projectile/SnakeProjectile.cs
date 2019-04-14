using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeProjectile : Projectile
{
    protected override void ApplyEffect(CustomTile _tile)
    {
        Debug.Log("TOUCH TRAP"); 
    }

    protected override bool CanHit(CustomTile _nextTile)
    {
        // Check if the projectile can apply its effect on a script
        return _nextTile.m_Entities.Any(e => e.GetComponent<Trap>()); 
    }
}
