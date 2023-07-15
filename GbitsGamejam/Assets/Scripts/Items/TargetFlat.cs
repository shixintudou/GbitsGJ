using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFlat : FlatBase
{
    public Vector2 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void FlatEnable()
    {
        StartCoroutine(MoveCoroutine());
    }
    IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(1f);
        while(Vector2.Distance(new Vector2(transform.position.x,transform.position.y),targetPosition)>0.5f)
        {
            transform.position = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
