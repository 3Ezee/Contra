using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : IAttackBehavior
{
    
    public void Attack(int layer, Transform attackSpawner)
    {
        TriggerAttack(layer, attackSpawner, attackSpawner.right,2);
        TriggerAttack(layer, attackSpawner, attackSpawner.right + attackSpawner.up, attackSpawner.gameObject.layer);
        TriggerAttack(layer, attackSpawner, attackSpawner.right - attackSpawner.up, attackSpawner.gameObject.layer);
    }

    private void TriggerAttack(int layer, Transform attackSpawner, Vector3 vec,int sp)
    {
       
        EventsManager.TriggerEvent(EventType.GP_ShootProjectile,
                                   new object[] {
                                        layer,
                                        attackSpawner.position.x,
                                        attackSpawner.position.y,
                                        vec,
                                        sp,
                                        0
                                   });
    }
}
