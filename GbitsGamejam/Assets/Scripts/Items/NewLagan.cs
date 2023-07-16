using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLagan : LaganController
{
    // Start is called before the first frame update
    public GameObject controlObj;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void BePicked()
    {
        base.BePicked();
        if (controlObj != null)
        {
            controlObj.SetActive(true);
        }
    }
}
