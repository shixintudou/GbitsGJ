using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D col;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
#nullable enable
        PlayerHJ? player = collision.GetComponent<PlayerHJ>();
#nullable disable
        if(player != null)
        {
            player.Dead();
        }
    }
}
