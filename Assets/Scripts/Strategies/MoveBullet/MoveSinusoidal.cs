using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSinusoidal : IMoveBulletBehavior {

    private float _range = 0.1f;
    private bool isPositive = true;
    private float _time = 0;
    //private float speedActual=0;

    public void Move(Transform t, Vector3 direction, float speed)
    {
        _time += Time.deltaTime;
        t.Translate(t.right * (speed/2) *Time.deltaTime,Space.World);

        /*if (isPositive)
        {
            speedActual += Time.deltaTime;
        }
        else
        {
            speedActual -= 0.5f;
        }

        if(Mathf.Abs(speedActual)> speed)
        {
            if (isPositive)
            {
                speedActual = Mathf.Abs(speedActual);
            }
            else
            {
                speedActual = -Mathf.Abs(speedActual);
            }
            isPositive = !isPositive;
        }

        t.Translate(t.up * (speedActual*3) * Time.deltaTime, Space.World);*/
        if(_time>= _range)
        {
            isPositive = !isPositive;
            _time = 0;
        }

        if (isPositive)
        {
            t.Translate(t.up * speed /3 * Time.deltaTime,Space.World);
        }
        else
        {
            t.Translate(t.up * -speed /3 * Time.deltaTime,Space.World);
        }
    }
}
