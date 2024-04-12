using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HL_LevelManager : MonoBehaviour
{
    public GameObject LocalPlayer;
    private HL_PlayerController LocalPlayerController;

    public GameObject InteractionManagerGameObject;
    private HL_InteractionManager InteractionManager;

    public GameObject GameManagerObject;
    private HL_GameManager GameManager;
    void Start()
    {
        LocalPlayerController = LocalPlayer.GetComponent<HL_PlayerController>();
        InteractionManager = InteractionManagerGameObject.GetComponent<HL_InteractionManager>();
        GameManager = GameManagerObject.GetComponent<HL_GameManager>();
    }

    public void OnGameplayLevelLoad(Scene scene, LoadSceneMode SceneMode)
    {
        GameObject SpawnPoint = GameObject.Find("SpawnPoint");

        if (SpawnPoint != null)
            LocalPlayer.transform.position = SpawnPoint.transform.position;
         
    }
    public void LoadSceneEx(string SceneName)
    {

        InteractionManager.OnPreSceneLoad();

        if (SceneName.Contains("GameEnd"))
        {
            // LocalPlayer.SetActive(false);
            InteractionManager.bQueueForDeletion = true;
            GameManager.OnGameOver();
            Debug.Log("Gameplay_GameEnd");
        }
        else if (SceneName.Contains("Gameplay_"))
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