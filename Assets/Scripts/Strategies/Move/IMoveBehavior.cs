using UnityEngine;

public interface IMoveBehavior {

    void Move(Rigidbody2D rb, Transform t, Vector2 direction, float speed);
}

