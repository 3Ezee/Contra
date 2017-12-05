using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour {

    public float actualLife;
    public float totalLife;
    public float speed;
    public float jumpForce;
    public float damage;
    public float attackVelocity;
    protected Rigidbody2D rb;
    protected Animator anim;

    //Strategies
    protected IAttackBehavior _attackStrategy;
    protected IMoveBehavior _moveStrategy;

    //prueba
    protected List<IAttackBehavior> _poolAttack;

    protected bool isCollisionWall = false;
    protected bool isCollisionFloor = false;
    protected bool isDie = false;

    protected void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        _poolAttack = new List<IAttackBehavior>();
    }

    protected void ResetThis()
    {
        actualLife = totalLife;
        EventsManager.TriggerEvent(EventType.GP_Life, new object[] { actualLife });
        isDie = false;
    }
    public virtual void ReceiveDamage(float damage)
    {
        actualLife -= damage;
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor") isCollisionFloor = true;
        if (collision.gameObject.tag == "Wall") isCollisionWall = true;
    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor") isCollisionFloor = false;
        if (collision.gameObject.tag == "Wall") isCollisionWall = false;
    }
    public bool IsCollisionFloor
    {
        get
        {
            return isCollisionFloor;
        }
        set
        {
            isCollisionFloor = value;
        }
    }
    public bool IsCollisionWall
    {
        get
        {
            return IsCollisionWall;
        }
        set
        {
            isCollisionWall = value;
        }
    }
}
