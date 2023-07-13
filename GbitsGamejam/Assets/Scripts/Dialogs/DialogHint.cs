using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHint : MonoBehaviour
{
    [SerializeField]
    Vector3 targetScale = Vector3.one;
    Vector3 originScale = Vector3.zero;

    IEnumerator enumerator = null;
    [SerializeField]
    float speed = 6f;

    public void Show()
    {
        if (enumerator != null)
        {
            StopCoroutine(enumerator);      
        }
        enumerator = IChangeScale(targetScale, speed);
        StartCoroutine(enumerator);
    }
    
    public void Hide()
    {
        if (enumerator != null)
        {
            StopCoroutine(enumerator);
        }
        enumerator = IChangeScale(originScale, 6f);
        StartCoroutine(enumerator);
    }
    IEnumerator IChangeScale(Vector3 target, float speed)
    {

        while (Vector3.Magnitude(transform.localScale - target) > 0.1f)
        {
            transform.localScale = transform.localScale + (target - transform.localScale).normalized * speed * Time.deltaTime;
            yield return null;
        }
        transform.localScale = target;
    }
}
