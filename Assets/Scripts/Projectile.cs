using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public float timeAlive;
    private float _time = 0;
    Vector2 _direction;

    private SpriteRenderer _sr;
    public Sprite p1;
    public Sprite p2;
    public Sprite p3;
    private int _actualSprite;

    //Strategy
    private IMoveBulletBehavior _moveStrategy;

    void Start () {
        _sr = this.GetComponent<SpriteRenderer>();
        _moveStrategy = new MoveForward();
        _sr.sprite = p1;
    }
	void Update () {
        _time += Time.deltaTime;
        _moveStrategy.Move(this.transform,_direction,speed);
        if(_time> timeAlive)
            ProjectileSpawner.Instance.ReturnBulletToPool(this);
    }
    private void Initialize()
    {
        _time = 0;
    }

    public static void InitializeProjectile(Projectile projectileObj)
    {
        projectileObj.gameObject.SetActive(true); 
        projectileObj.Initialize(); 
    }

    public static void DisposeProjectile(Projectile projectileObj)
    {
        projectileObj.gameObject.SetActive(false); 
    }

    public Projectile SetX(float x)
    {
        transform.position = new Vector2(x,transform.position.y);
        return this;
    }
    public Projectile SetY(float y)
    {
        transform.position = new Vector2(transform.position.x,y);
        return this;
    }

    public Projectile SetLayer(int layer)
    {
        this.gameObject.layer = layer;
        return this;
    }
    public Projectile SetDirection(Vector3 vec)
    {
        _direction = vec;
        this.transform.right = vec;
        //this.transform.Rotate(vec);
       
        return this;
    }
    public Projectile SetDirection(Transform t)
    {
        this.transform.rotation = t.rotation;
        _direction = t.right;
        return this;
    }

    public Projectile SetSprite(int numberP)
    {
        /*if (numberP == 1)
        {
            _sr.sprite = p1;
        }
        else if(numberP == 2)
        {
            _sr.sprite = p2;
        }
        else if (numberP == 3)
        {
            _sr.sprite = p3;
        }*/
        
        return this;
    }
    public Projectile SetStrategy(int n)
    {
        if(n == 0)
        {
            _moveStrategy = new MoveForward();
        }
        else
        {
            _moveStrategy = new MoveSinusoidal();
        }

        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != this.gameObject.layer)
        {
            if (collision.gameObject.layer == 9 || collision.gameObject.layer == 8)
            {
                collision.gameObject.GetComponent<EntityBehavior>().ReceiveDamage(1);
            }
        }
        
    }
}