using System; 
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using Random = UnityEngine.Random; 

public class AIManager : MonoBehaviour
{
    #region Fields and properties 
    [SerializeField] private float[] m_statesCoefficient = new float[4] {1, .7f, .5f, .2f };
    private int m_currentStateIndex = 0;

    [SerializeField] private string[] m_spawningTraps;
    #endregion

    #region Methods 

    #region UnityMethods

    #endregion

    #endregion

}
