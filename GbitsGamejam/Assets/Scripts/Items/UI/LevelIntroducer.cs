using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelIntroducer : MonoBehaviour
{
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
        animator = GetComponentInChildren<Animator>(true);
        image = GetComponentInChildren<Image>(true);
        if (animator)
            imageObj = animator.gameObject;
        if(imageObj)
            imageObj.SetActive(false);
    }
    private void Update()
    {
       
    }

    /// <summary>
    ///第一次进入关卡，显示关卡名动画，对应调用后续游戏流程
    /// </summary>
    /// <param name="nowSceneIndex"></param>
    /// <param name="ifLoadNextLevel"></param>
    //index从0开始
    public void SetIntroduceImageAndEnable(int nowSceneIndex, GameMode.Func_NoParam LaterStream_callBack)
    {
        if (nowSceneIndex < 0 && nowSceneIndex > SceneManager.sceneCountInBuildSettings - 1) return;
        imageObj.SetActive(true);
        GameMode.Instance.SetGameMode(GamePlayMode.Act);//设为演出模式
       // animator.SetInteger("LevelIndex", nowSceneIndex+1);
        animator.Play((nowSceneIndex + 1).ToString(), 0, 0f);
        StartCoroutine(DC(LaterStream_callBack));
    }
    IEnumerator DC(GameMode.Func_NoParam LaterStream_callBack)
    {
        yield return new WaitForSeconds(2.5f);
        imageObj.SetActive(false);
        //开始关卡
        LaterStream_callBack();
    }
}
