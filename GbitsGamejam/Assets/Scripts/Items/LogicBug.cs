using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBug : MonoBehaviour
{
    int SectionIndex;//��Ӧ��ʱ���
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSectionIndex(int Index)
    {
        SectionIndex= Index;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameMode.Instance.OnPlayerTouchSectionBug(SectionIndex,this);
        }
    }
}
