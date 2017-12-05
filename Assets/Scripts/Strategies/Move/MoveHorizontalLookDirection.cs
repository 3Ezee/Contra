using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontalLookDirection : IMoveBehavior {

    public void Move(Rigidbody2D rb, Transform t, Vector2 direction, float speed)
    {
        if(direction != Vector2.zero)
            t.right = direction;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }
}
