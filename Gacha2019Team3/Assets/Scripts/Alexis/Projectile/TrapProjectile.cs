using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class TrapProjectile : Projectile
{
    /// <summary>
    /// Hit the snake part on the tile
    /// </summary>
    /// <param name="_tile">tile</param>
    protected override void ApplyEffect(CustomTile _tile)
    {
        _tile.m_Entities.Select(e => e.GetComponent<SnakePart>()).First().Hit();
    }

    /// <summary>
    /// Check if the next tile has a trap within its entities
    /// </summary>
    /// <param name="_nextTile"></param>
    /// <returns></returns>
    protected override bool CanHit(CustomTile _nextTile)
    {
        return _nextTile.m_Entities.Any(e => e.GetComponent<SnakePart>());
    }
}
