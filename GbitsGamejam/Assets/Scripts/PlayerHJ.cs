using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//横板跳跃玩家类
public class PlayerHJ : MonoBehaviour
{
    //public enum State
    //{
    //    Idle,Move,OnAir
    //}

    [Header("移动数据")]
    public float moveSpeed = 7f;
    public float jumpforce = 5f;
    public float jumpCompensate = 2f;
    public int maxJumpTimes = 2;

    int nowJumpTimes = 0;
    bool isOnGround = true;
    //State state = State.Move;

    Transform footTrans;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footTrans = transform.Find("foot");
        if (footTrans == null)
            print("未获取playerfoot");

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && nowJumpTimes < maxJumpTimes)
        {
           // rb.AddForce(Vector2.up * jumpforce);
            rb.velocity =new Vector2(rb.velocity.x,jumpforce);
            nowJumpTimes++;
        }
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.1 && CheckIsOnGround())
        {
            if (!isOnGround)
                nowJumpTimes = 0;
            isOnGround = true;
        }
        else
            isOnGround = false;
        if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * jumpCompensate * Time.fixedDeltaTime;
        }
        //水平速度
        float y = 0;
        if(!isOnGround)
        {
            y=rb.velocity.y;
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, y);
    }


    bool CheckIsOnGround()
    {
        var bisOnGround = Physics2D.OverlapCircle(footTrans.position, 0.2f, LayerMask.GetMask("Ground"));
        return bisOnGround;
    }
    private void OnDrawGizmos()
    {
    }

}
