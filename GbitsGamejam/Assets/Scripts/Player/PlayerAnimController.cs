using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    PlayerHJ player;
    Rigidbody2D rb;
    void Start()
    {
        animator= GetComponent<Animator>();
        player = GetComponent<PlayerHJ>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal")>0.5f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(Input.GetAxisRaw("Horizontal")<-0.5f)
        {
            transform.localScale = Vector3.one;
        }
        animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isAir", !player.CheckIsOnGround());
    }
}
