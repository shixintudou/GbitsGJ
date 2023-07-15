using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //玩家触碰并且已通关
        if (collision.gameObject.tag == "Player")
        {
            GameMode.Instance.IfTouchTransport = true;
            //加载下一次场景
            GameMode.Instance.CheckIfPass();
        }
    }
}
