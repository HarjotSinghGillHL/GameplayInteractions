using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_MakeSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    static bool bInitialized = false;
    void Start()
    {
        if (bInitialized)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            bInitialized = true;
        }
    }

}
