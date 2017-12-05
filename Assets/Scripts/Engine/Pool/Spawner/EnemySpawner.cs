using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public EnemyBehavior enemyPrefab; //Referencia al prefab de la bala
    public int numberEnemies = 8;
    private Pool<EnemyBehavior> _enemyPool; //Pool que va a contener Bullets

    private static EnemySpawner _instance; //Singleton del bullet spawner
    public static EnemySpawner Instance { get { return _instance; } } //Singleton del bullet spawner

    void Awake()
    {
        _instance = this; //Asigna el singleton
        _enemyPool = new Pool<EnemyBehavior>(numberEnemies,
                                                EnemyFactory,
                                                EnemyBehavior.InitializeProjectile,
                                                EnemyBehavior.DisposeProjectile,
                                                true); //Crea el pool de objetos
        //EventsManager.SubscribeToEvent(EventType.GP_ShootProjectile, GetEnemyFromPool);
    }

    public void GetEnemyFromPool(params object[] parameters)
    {
        EnemyBehavior enem = _enemyPool.GetObjectFromPool();
        //Debug.Log("GetEnemyFromPool: " + (int)parameters[2] + "-" + (int)parameters[2]);
        enem.transform.parent = this.transform.parent;
        enem.
             SetX((float)parameters[0]).
             SetY((float)parameters[1]).
             SetStrategyAttack((int)parameters[2]).
             SetStrategyMove((int)parameters[3]).
             SetDirection((float)parameters[4]).
             SetAnimationController((int)parameters[5]).
             ResetEntity();
    }

    //Función que me devuelve un bullet (factory)
    private EnemyBehavior EnemyFactory()
    {
        return Instantiate<EnemyBehavior>(enemyPrefab); //Instancia un prefab de la bala
    }

    //Función que devuelve una bala al pool
    public void ReturnEnemyToPool(EnemyBehavior enemy)
    {
        _enemyPool.DisablePoolObject(enemy); //Llama al método disable del pool.
    }
}
