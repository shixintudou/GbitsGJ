using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��Ҵ���������ͨ��
        if (collision.gameObject.tag == "Player" && GameMode.Instance.IfPass)
        {
            //������һ�γ���
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
