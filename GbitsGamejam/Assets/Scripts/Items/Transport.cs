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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��Ҵ���������ͨ��
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("Open");
            GameMode.Instance.IfTouchTransport = true;
            //������һ�γ���
            GameMode.Instance.CheckIfPass();
        }
    }
}
