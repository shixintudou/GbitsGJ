using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transport : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name.Contains("ActScene"))
        {
            animator.SetTrigger("Open");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��Ҵ���������ͨ��
        if (collision.gameObject.tag == "Player")
        {
            if(SceneManager.GetActiveScene().name.Contains("ActScene"))
            {
                SceneManager.LoadScene("ActScene3");
            }
            animator.SetTrigger("Open");
            GameMode.Instance.IfTouchTransport = true;
            //������һ�γ���
            GameMode.Instance.CheckIfPass();
        }
    }
}
