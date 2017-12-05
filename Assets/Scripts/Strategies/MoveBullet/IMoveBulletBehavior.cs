using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveBulletBehavior{

    void Move(Transform t, Vector3 direction, float speed);
}
