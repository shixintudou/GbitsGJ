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
    GameObject imageObj;
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
        if (animator)
            imageObj = animator.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //index´Ó0¿ªÊ¼
    public void SetIntroduceImageAndEnable(int index)
    {
        if (index < 0 && index >= SceneManager.sceneCountInBuildSettings-1) return;
        imageObj.SetActive(true);
        GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
        // animator.SetInteger("LevelIndex", index);
        animator.Play((index + 1).ToString(), 0, 1);
        StartCoroutine(DC());
    }
    IEnumerator DC()
    {
        yield return 3.0f;
        imageObj.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameMode.Instance.StartLevel();
    }
    public void DisableThis()
    {
        imageObj.SetActive(false);
        GameMode.Instance.StartLevel();
    }
}
