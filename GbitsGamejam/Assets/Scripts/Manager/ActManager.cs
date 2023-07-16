using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    public static ActManager instance;
    public float actMoveSpeed;
    public float actTime; 
    GameObject player;
    PlayerAnimController playerAnimController;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    public void StartAct()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimController = player.GetComponent<PlayerAnimController>();
        playerAnimController.animator.SetBool("ActMove", true);
        playerAnimController.animator.SetBool("ActMode", true);
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
        //yield return new WaitForSeconds(actTime);
    }
}
