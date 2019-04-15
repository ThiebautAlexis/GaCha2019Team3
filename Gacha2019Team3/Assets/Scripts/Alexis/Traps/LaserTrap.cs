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
    /// Cast a ray in front of the trap during a certain duration
    /// If the player is touched, call the damage method 
    /// Then break and destroy the object
    /// </summary>

    
    protected override IEnumerator TriggerTrap()
    {
        float _timer = 0;
        RaycastHit _hitInfo;
        int _rayNumber = AIManager.Instance.m_CurrentStateIndex < 3 ? 1 : AIManager.Instance.m_CurrentStateIndex > 3 ? 4 : (int)UnityEngine.Random.Range(2,4); 
        if(m_laserRenderer)
        {
            m_laserRenderer.positionCount = 2 * _rayNumber;
            for (int i = 0; i < m_laserRenderer.positionCount; i++)
            {
                m_laserRenderer.SetPosition(i, transform.position); 
            }
        }
        Vector3 _direction = Vector3.zero; 
        while (_timer < m_LaserDurationInSeconds)
        {
            for (int i = 0; i < m_laserRenderer.positionCount; i+=2)
            {
                _direction = transform.forward * Mathf.Cos(90*(i/2)*Mathf.Deg2Rad) + transform.right * Mathf.Sin(90 * (i/2) * Mathf.Deg2Rad);
                if (Physics.Raycast(new Ray(transform.position, _direction), out _hitInfo))
                {
                    if (m_laserRenderer != null)
                    {
                        m_laserRenderer.SetPosition(i, transform.position); 
                        m_laserRenderer.SetPosition(i+1, _hitInfo.point);
                    }
                    // Check if the player is touched by the ray
                    if (_hitInfo.collider.GetComponent<SnakePart>())
                    {
                        //Call Method to damage the player
                        _hitInfo.collider.GetComponent<SnakePart>().Hit();
                        break;
                    }
                }
            }
            _timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        CleanTile(); 
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
