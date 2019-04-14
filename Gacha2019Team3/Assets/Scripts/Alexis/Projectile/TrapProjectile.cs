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
        _tile.m_Entities.Select(e => e.GetComponent<SnakePart>()).FirstOrDefault().Hit();
    }

    /// <summary>
    /// Check if the next tile has a trap within its entities
    /// </summary>
    /// <param name="_nextTile"></param>
    /// <returns></returns>
    protected override bool CanHit(CustomTile _nextTile)
    {
        if (_nextTile.m_Entities.Count == 0) return false;
        for (int i = 0; i < _nextTile.m_Entities.Count; i++)
        {
            if (_nextTile.m_Entities[i] && _nextTile.m_Entities[i].GetComponent<SnakePart>())
                return true; 
        }
        return false; 
    }
}
