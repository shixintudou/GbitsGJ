using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��Ҵ���������ͨ��
        if (collision.gameObject.tag == "Player")
        {
            GameMode.Instance.IfTouchTransport = true;
            //������һ�γ���
            GameMode.Instance.CheckIfPass();
        }
    }
}
