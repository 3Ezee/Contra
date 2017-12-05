using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpNormal : IJumpBehavior
{
    private bool _canJump;

    public void Jump(Rigidbody2D rb, float jumpForce, bool IsCollisionFloor)
    {
        if (IsCollisionFloor)
        {
            _canJump = true;
        }
        if (_canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * rb.gravityScale);
            _canJump = false;
        }
    }
}