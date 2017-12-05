using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySemiautomatic : IAttackBehavior { 

    private float _time = 0;

    public void Attack(int layer, Transform attackSpawner)
    {
        _time += Time.deltaTime;

        if (_time > 2)
        {
            EventsManager.TriggerEvent(EventType.GP_ShootProjectile,
                               new object[] {
                                        layer,
                                        attackSpawner.position.x,
                                        attackSpawner.position.y,
                                        attackSpawner.right,
                                        attackSpawner.gameObject.layer,
                                        0
                               });
            _time = 0;
        }
    }
}

