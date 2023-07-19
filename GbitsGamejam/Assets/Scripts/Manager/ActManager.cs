using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    public static ActManager instance;
    public float actMoveSpeed;
    public float actTime;
    public GameObject video;
    public GameObject videoCamera;
    public GameObject mainCamera;
    GameObject player;
    PlayerAnimController playerAnimController;
    int index;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    private void Start()
    {
        index = SceneManager.GetActiveScene().name[8] - '0';
        if (index == 3)
        {
            RendererFeatureManager.instance.SetLineActive(false);
        }
        RendererFeatureManager.instance.SetOldTVActive(false);
    }
    public void StartAct()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimController = player.GetComponent<PlayerAnimController>();
        playerAnimController.animator.SetBool("ActMove", true);
        playerAnimController.animator.SetBool("ActMode", true);
        GameMode.Instance.SetGameMode(GamePlayMode.Act);
        if (index == 1)
        {
            RendererFeatureManager.instance.SetLineActive(true);
        }
        StartCoroutine(ActCoroutine());
    }
    IEnumerator ActCoroutine()
    {
        float t = 0f;
        int x = 1;
        if (index == 3)
        {
            x = -1;
            playerAnimController.animator.SetBool("Heart", true);
        }
        while (t < actTime)
        {
            t += Time.deltaTime;
            if (player)
                player.transform.position += x * Vector3.right * Time.deltaTime * actMoveSpeed;
            yield return null;
        }
        if (index == 2)
        {
            GameMode.Instance.SetGameMode(GamePlayMode.Play);
            playerAnimController.animator.SetBool("ActMove", false);
            playerAnimController.animator.SetBool("ActMode", false);
        }
        if (index == 1)
        {
            StartCoroutine(VideoCoroutine());
        }
        //yield return new WaitForSeconds(actTime);
    }
    public IEnumerator ActMoveCoroutine(float time, int x = 1, string sceneName = "")
    {
        float t = 0f;
        playerAnimController.animator.SetBool("ActMove", true);
        playerAnimController.animator.SetBool("ActMode", true);
        while (t < time)
        {
            t += Time.deltaTime;
            if (player)
                player.transform.position += x * Vector3.right * Time.deltaTime * actMoveSpeed;
            yield return null;
        }
        if (sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public IEnumerator VideoCoroutine()
    {

        videoCamera.SetActive(true);
        video.SetActive(true);
        mainCamera.SetActive(false);
        yield return new WaitForSeconds(22);
        SceneManager.LoadScene("ActScene2");
    }
}
