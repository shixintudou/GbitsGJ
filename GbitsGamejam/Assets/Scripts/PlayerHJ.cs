using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//横板跳跃玩家类
public class PlayerHJ : MonoBehaviour
{
    [Header("移动数据")]
    public float moveSpeed = 7f;
    public float jumpforce = 4f;
    public float jumpCompensate = 2f;
    public int maxJumpTimes = 2;

    int nowJumpTimes = 0;
    bool isOnGround = true;

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
            rb.velocity += Vector2.up * jumpforce * 2;
            nowJumpTimes++;
        }
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.1 && CheckIsOnGround())
            isOnGround = true;
        else
            isOnGround = false;
        if (isOnGround)
            nowJumpTimes = 0;
        if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * jumpCompensate * 0.02f;
        }
        //水平速度
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
    }
  

    bool CheckIsOnGround()
    {
        var bisOnGround =  Physics2D.OverlapCircle(footTrans.position, 0.8f,LayerMask.GetMask("Ground"));
        return bisOnGround;
    }
    private void OnDrawGizmos()
    {
    }

}
