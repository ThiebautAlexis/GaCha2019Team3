using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : Trap
{
    #region Fields and properties
    [Header("Laser Settings")]
    [SerializeField, Range(.1f, 5)] private int m_laserDuration = 1;
    public float m_LaserDurationInSeconds
    {
        get { return m_laserDuration * GameUpdater.Instance.m_TickEvent;  }
    }

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
    /// Cast a ray in front of the trap during a certain duration
    /// If the player is touched, call the damage method 
    /// Then break
    /// </summary>
    /// <returns></returns>
    private IEnumerator CastLaser()
    {
        float _timer = 0;
        RaycastHit _hitInfo; 
        if(m_laserRenderer != null)
        {
            m_laserRenderer.positionCount = 2; 
            m_laserRenderer.SetPosition(0, transform.position);
            m_laserRenderer.SetPosition(1, transform.position); 
        }
        while(_timer < m_LaserDurationInSeconds)
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out _hitInfo))
            {
                if(m_laserRenderer != null)
                {
                    m_laserRenderer.SetPosition(1, _hitInfo.point);
                }
                //if(_hitInfo.collider)
                // Check if the player is touched by the ray
                if (_hitInfo.collider.GetComponent<SnakePart>())
                {
                    //Call Method to damage the player
                    _hitInfo.collider.GetComponent<SnakePart>().Hit(); 
                    break; 
                }
            }
            _timer += Time.deltaTime; 
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
        yield break; 
    }

    #region UnityMethods
    protected override void Start()
    {
        base.Start();
        if (!m_laserRenderer)
        {
            m_laserRenderer = GetComponent<LineRenderer>();
        }
        m_laserRenderer.positionCount = 0;
    }
    #endregion
    #endregion
}
