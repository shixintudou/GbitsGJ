using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelIntroducer : MonoBehaviour
{
    public Sprite[] LevelIntroduces;
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
        image = GetComponentInChildren<Image>();
        if (animator)
            imageObj = animator.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 废案，未采用
    /// </summary>
    /// <param name="nowSceneIndex"></param>
    /// <param name="ifLoadNextLevel"></param>
    //index从0开始
    public void SetIntroduceImageAndEnable(int nowSceneIndex, bool ifLoadNextLevel = true)
    {
        if (nowSceneIndex < 0 && nowSceneIndex >= SceneManager.sceneCountInBuildSettings - 1) return;
        imageObj.SetActive(true);
        GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
        animator.Play((nowSceneIndex + 1).ToString(), 0, 1);
        animator.SetInteger("LevelIndex", nowSceneIndex);

        print("播放动画" + (nowSceneIndex + 1));

        if (nowSceneIndex >= 0 && nowSceneIndex+1 < LevelIntroduces.Length)
            image.sprite = LevelIntroduces[nowSceneIndex+1];
        StartCoroutine(DC(ifLoadNextLevel));
    }
    IEnumerator DC(bool ifLoadNextLevel)
    {
        yield return new WaitForSeconds(2.5f);
        imageObj.SetActive(false);
        if (ifLoadNextLevel)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameMode.Instance.StartLevel();
    }
    public void DisableThis()
    {
        imageObj.SetActive(false);
        GameMode.Instance.StartLevel();
    }
}
