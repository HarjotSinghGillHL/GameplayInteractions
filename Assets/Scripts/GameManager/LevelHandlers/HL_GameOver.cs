using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HL_GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject UIGameManager = GameObject.Find("GameManager");

        HL_GameManager GameManager = UIGameManager.GetComponent<HL_GameManager>();
        GameManager.OnGameOver();

        Debug.Log(GameManager.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
