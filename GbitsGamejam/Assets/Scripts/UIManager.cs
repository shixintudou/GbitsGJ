using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(transform.parent.gameObject);
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
       
    }
   
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
