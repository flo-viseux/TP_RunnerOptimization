using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Managers;
public class GameOverUI : MonoBehaviour
{
    #region SerializedFields
    [SerializeField] private Canvas canvas = null;

    [SerializeField] private Button button = null;
    #endregion

    #region UnityMethods
    void Start()
    {
        canvas.enabled = false;
        button.onClick.AddListener(() => ReloadScene());
        PlayerController._onGameOver += Show;
    }
    #endregion

    private void Show()
    {
        canvas.enabled = true;
    }

    private void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
