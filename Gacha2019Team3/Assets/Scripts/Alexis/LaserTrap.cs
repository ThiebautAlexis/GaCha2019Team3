using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : Trap
{
    #region Fields and properties
    [Header("Laser Settings")]
    [SerializeField, Range(.1f, 5)] private int m_laserDuration = 1;

    [SerializeField] private LineRenderer m_laserRenderer;  
    #endregion

    #region Methods
    /// <summary>
    /// Method called when the trap has to be activated
    /// </summary>
    protected override void TriggerTrap()
    {
        StartCoroutine(CastLaser());
    }

    /// <summary>
    //Cast a ray in front of the trap during a certain duration
    // If the player is touched, call the damage method 
    // Then break
    /// </summary>
    /// <returns></returns>
    private IEnumerator CastLaser()
    {
        float _timer = 0;
        RaycastHit _hitInfo; 
        if(m_laserRenderer != null)
        {
            Debug.Log("in 1 ");
            m_laserRenderer.SetVertexCount(2);
            m_laserRenderer.SetPosition(0, transform.position);
            m_laserRenderer.SetPosition(1, transform.position); 
        }
        while(_timer < m_laserDuration)
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out _hitInfo))
            {
                if(m_laserRenderer != null)
                {
                    m_laserRenderer.SetPosition(1, _hitInfo.point);
                }
                //if(_hitInfo.collider)
                // Check if the player is touched by the ray
                if (false)
                {
                    //Call Method to damage the player
                    break; 
                }
            }
            _timer += .25f;
            yield return new WaitForSeconds(.25f);
        }
        Destroy(gameObject); 
    }

    #region UnityMethods
    protected void Start()
    {
        if(m_laserRenderer != null)
        {
            m_laserRenderer.SetVertexCount(0); 
        }
    }
    #endregion
    #endregion
}
