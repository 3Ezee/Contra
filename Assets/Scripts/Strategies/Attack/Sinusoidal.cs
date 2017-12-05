using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinusoidal : IAttackBehavior
{
    public void Attack(int layer, Transform attackSpawner)
    {
        EventsManager.TriggerEvent(EventType.GP_ShootProjectile,
                                   new object[] {
                                        layer,
                                        attackSpawner.position.x,
                                        attackSpawner.position.y,
                                        attackSpawner.right,
                                        attackSpawner.gameObject.layer,
                                        1
                                   });
    }
}
