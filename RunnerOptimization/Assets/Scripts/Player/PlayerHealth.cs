using Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Attributes
    [SerializeField]private int _LifeCount = 3; //Default
    [SerializeField] private TMP_Text _LifeText;

    public int LifeCount { get => _LifeCount; }
    #endregion

    private void OnEnable()
    {
        PlayerController._onHit += DecreseLifeCount;
    }

    private void OnDisable()
    {
        PlayerController._onHit -= DecreseLifeCount;
    }

    #region API
    public void DecreseLifeCount()
    {
        --LifeCount;
        _LifeText.text = LifeCount.ToString();
    }
    #endregion
}
