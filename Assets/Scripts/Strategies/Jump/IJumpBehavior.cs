using UnityEngine;

public interface IJumpBehavior {
    
    void Jump(Rigidbody2D rb, float jumpForce, bool IsCollisionFloor);
}
