using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : IMoveBulletBehavior
{
    public void Move(Transform t, Vector3 direction, float speed)
    {
        
        t.Translate(t.right *speed * Time.deltaTime,Space.World);
    }
}
