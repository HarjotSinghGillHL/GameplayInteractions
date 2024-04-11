using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HL_LevelManager : MonoBehaviour
{
    public GameObject LocalPlayer;
    private HL_PlayerController LocalPlayerController;
    void Start()
    {
        LocalPlayerController = LocalPlayer.GetComponent<HL_PlayerController>();
    }

    public void OnGameplayLevelLoad(Scene scene, LoadSceneMode SceneMode)
    {
        GameObject SpawnPoint = GameObject.Find("SpawnPoint");

        if (SpawnPoint != null)
            LocalPlayer.transform.position = SpawnPoint.transform.position;
         
    }
    public void LoadSceneEx(string SceneName)
    {

        if (SceneName.Contains("Gameplay_"))
        {
            LocalPlayer.SetActive(true);
            SceneManager.sceneLoaded += OnGameplayLevelLoad;
        }

        SceneManager.LoadScene(SceneName);

    }
    void Update()
    {

    }
}