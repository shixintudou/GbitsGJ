using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBug : MonoBehaviour
{
    int SectionIndex;//对应的时间段
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
