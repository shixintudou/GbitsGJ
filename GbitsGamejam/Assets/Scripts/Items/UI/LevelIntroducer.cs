using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelIntroducer : MonoBehaviour
{
    // public Sprite[] LevelIntroduces;
    Animator animator;
    private static LevelIntroducer instance;
    public static LevelIntroducer Instance
    {
        get
        {
            return instance;
        }
    }
    Image image;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetIntroduceImageAndEnable(int index)
    {
        if (index < 0 && index >= SceneManager.sceneCountInBuildSettings) return;
        GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
        animator.SetInteger("LevelIndex",index);
        gameObject.SetActive(true);
        Invoke("DisableThis", 3f);
    }
    public void DisableThis()
    {
        gameObject.SetActive(false);
        GameMode.Instance.StartLevel();
    }
}
