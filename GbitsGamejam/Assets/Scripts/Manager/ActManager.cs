using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    public static ActManager instance;
    public float actMoveSpeed;
    public float actTime; 
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
        print(SceneManager.GetActiveScene().name[8]);
    }
    public void StartAct()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimController = player.GetComponent<PlayerAnimController>();
        playerAnimController.animator.SetBool("ActMove", true);
        playerAnimController.animator.SetBool("ActMode", true);
        GameMode.Instance.SetGameMode(GamePlayMode.Act);
        StartCoroutine(ActCoroutine());
    }
    IEnumerator ActCoroutine()
    {
        float t = 0f;
        while (t<actTime)
        {
            t+= Time.deltaTime;
            player.transform.position += Vector3.right * Time.deltaTime * actMoveSpeed;
            yield return null;
        }
        if(index==2)
        {
            GameMode.Instance.SetGameMode(GamePlayMode.Play);
            playerAnimController.animator.SetBool("ActMove", false);
            playerAnimController.animator.SetBool("ActMode", false);
        }
        //yield return new WaitForSeconds(actTime);
    }
}
