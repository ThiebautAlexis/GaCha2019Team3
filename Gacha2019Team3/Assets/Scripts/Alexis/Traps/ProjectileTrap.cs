using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : Trap
{

    #region Fields and Properties
    //[SerializeField] 
    #endregion 

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    protected override IEnumerator TriggerTrap()
    {

        Destroy(gameObject); 
        yield break; 
    }


    #region UnityMethods


    #endregion

    #endregion

}
