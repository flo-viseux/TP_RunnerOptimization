using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    #region SerializedFields
    [SerializeField] private TMP_Text label;
    #endregion

    #region UnityMethods
    void Start()
    {
        PlayerController._onIncreaseScore += ScoreChanged;
        ScoreChanged(0);
    }
    #endregion

    #region Private
    private void ScoreChanged(float score)
    {
        label.text = "Score : " + ((int) score).ToString();
    }
    #endregion
}
