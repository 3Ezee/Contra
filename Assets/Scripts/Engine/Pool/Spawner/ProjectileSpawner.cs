using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour
{
    public Projectile projectilePrefab; //Referencia al prefab de la bala
    public int numberProjectiles=8;
    private Pool<Projectile> _projectilePool; //Pool que va a contener Bullets

    private static ProjectileSpawner _instance; //Singleton del bullet spawner
    public static ProjectileSpawner Instance { get { return _instance; } } //Singleton del bullet spawner

    void Awake()
    {
        _instance = this; //Asigna el singleton
        _projectilePool = new Pool<Projectile>(numberProjectiles, ProjectileFactory, Projectile.InitializeProjectile, Projectile.DisposeProjectile, true); //Crea el pool de objetos
        projectilePrefab = Instantiate(Resources.Load("Prefabs/Projectile", typeof(Projectile))) as Projectile;

        EventsManager.SubscribeToEvent(EventType.GP_ShootProjectile, GetProjectileFromPool);
    }

    private void GetProjectileFromPool(params object[] parameters)
    {
        Projectile proj = _projectilePool.GetObjectFromPool();
        proj.transform.parent = this.transform.parent;
        proj.
            SetSprite((int)parameters[4]).
            SetDirection((Vector3)parameters[3]).
             SetLayer((int)parameters[0]).
             SetX((float)parameters[1]).
             SetY((float)parameters[2]).
             SetStrategy((int)parameters[5]);
    }

    //Función que me devuelve un bullet (factory)
    private Projectile ProjectileFactory()
    {
        return Instantiate<Projectile>(projectilePrefab); //Instancia un prefab de la bala
    }

    //Función que devuelve una bala al pool
    public void ReturnBulletToPool(Projectile projectile)
    {
        _projectilePool.DisablePoolObject(projectile); //Llama al método disable del pool.
    }

    public void UnsubscribeEvent()
    {
        EventsManager.UnsubscribeToEvent(EventType.GP_ShootProjectile, GetProjectileFromPool);
    }
}