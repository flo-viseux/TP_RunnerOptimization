using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Attributes
    private int lifeCount = 3; //Default
    #endregion

    #region API
    public void DecreseLifeCount()
    {
        --lifeCount;
    }
    #endregion
}
