using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaganController : BasePick
{
    // Start is called before the first frame update
    public FlatBase controlledFlat;
    Animator animator;
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
        bPicked = true;
        animator.SetBool("Picked", true);
        if (controlledFlat != null)
            controlledFlat.FlatEnable();
        //¼ì²âÊÇ·ñ¹ý¹Ø
        RecordManager.instance.lagan = this;
        GameMode.Instance.CheckIfPass();
        StartCoroutine(MoveEndCoroutine());
    }
    IEnumerator MoveEndCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("MoveEnd", true);
    }
}
