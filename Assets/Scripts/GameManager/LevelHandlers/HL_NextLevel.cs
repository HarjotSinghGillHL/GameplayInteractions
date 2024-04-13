using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HL_NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    HL_TaskManager TaskManager;
    HL_GameManager GameManager;
    void Start()
    {
        TaskManager = GameObject.Find("TaskManager").GetComponent<HL_TaskManager>();
        GameManager = GameObject.Find("GameManager").GetComponent<HL_GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TaskManager.bHasNextLevelKey)
        {
            if ((SceneManager.GetActiveScene().buildIndex + 1) == 3)
                GameManager.OnGameOver();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
