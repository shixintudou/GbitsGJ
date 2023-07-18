using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float LifeSpan = 1.0f;

    float LifeSpanTimer;
    void Start()
    {
        LifeSpanTimer = LifeSpan;
    }
    private void Update()
    {
        if (LifeSpanTimer > 0)
            LifeSpanTimer -= Time.deltaTime;
        else
            DestroySelf();
    }
    public void ResetLifeSpan()
    {
        LifeSpanTimer = LifeSpan;
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
