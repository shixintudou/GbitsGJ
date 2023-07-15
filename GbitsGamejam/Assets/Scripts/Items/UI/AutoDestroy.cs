using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float LifeSpan=1.0f;
    void Start()
    {
        Invoke("DestroySelf", LifeSpan);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
