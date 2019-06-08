using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;

    }
    #endregion

    public GameObject player;
    public Camera cam;
    private void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(800, 450, true);
        cam.aspect = 16f / 9f;
    }
    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
