using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : BasePick
{
    // Start is called before the first frame update
    float len = 0.5f;
    Vector2 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void BePicked()
    {
        base.BePicked();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BePicked();
        }
    }
}
