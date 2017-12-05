using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : EntityBehavior {
    
    public Transform attackSpawner;
    protected List<IMoveBehavior> _poolMove;
    public Vector2 direction = new Vector2(-1, 0);
    public float _time;

    void Start()
    {
        //ChangeMoveStrategy(1);
        _poolMove = new List<IMoveBehavior>();
        _moveStrategy = new MoveHorizontalLookDirection();
        _attackStrategy = new EnemySemiautomatic();

        initPoolAttack();
        initPoolMove();
    }

    void Update()
    {

        Move(direction);
        Attack();
    }

    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);
        EventsManager.TriggerEvent(EventType.GP_Score);
        EventsManager.TriggerEvent(EventType.GP_Destroy, new object[] {
                                        this.transform.position.x,
                                        this.transform.position.y
                               });
        EnemySpawner.Instance.ReturnEnemyToPool(this);
    }
    void initPoolAttack()
    {
        _poolAttack.Add(new NoAttack());
        _poolAttack.Add(new EnemySemiautomatic());
    }

    void initPoolMove()
    {
        _poolMove.Add(new NoMove());
        _poolMove.Add(new MoveHorizontalLookDirection());
    }

   
    public void Move(Vector2 direction)
    {
        _moveStrategy.Move(rb, this.transform, direction, speed);
        anim.SetFloat("Speed",Mathf.Abs(rb.velocity.x));
    }

    public void Attack()
    {
        _attackStrategy.Attack(this.gameObject.layer, attackSpawner);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerBehavior>().ReceiveDamage(1);
        }
        if (collision.gameObject.tag == "Destroy")
        {
            EnemySpawner.Instance.ReturnEnemyToPool(this);
        }
    }

    public EnemyBehavior ResetEntity()
    {
        actualLife = totalLife;
        isDie = false;
        return this;
    }

    public static void InitializeProjectile(EnemyBehavior enemyObj)
    {
        enemyObj.gameObject.SetActive(true);
        enemyObj.ResetEntity();
    }

    public static void DisposeProjectile(EnemyBehavior enemyObj)
    {
        enemyObj.gameObject.SetActive(false);
    }

    public EnemyBehavior SetX(float x)
    {
        transform.position = new Vector2(x, transform.position.y);
        return this;
    }
    public EnemyBehavior SetY(float y)
    {
        transform.position = new Vector2(transform.position.x, y);
        return this;
    }

    public EnemyBehavior SetDirection(float vec)
    {
        direction = new Vector2(vec,0);
        this.transform.right = direction;
        //this.transform.right = new Vector2(vec.x,vec.y);
        return this;
    }

    public EnemyBehavior SetStrategyAttack(int i)
    {
        if(i == 0)
        {
            _attackStrategy = new NoAttack();
        }
        else
        {
            _attackStrategy = new EnemySemiautomatic();
        }

        return this;
    }

    public EnemyBehavior SetStrategyMove(int i)
    {
        if (i == 0)
        {
            _moveStrategy = new NoMove();
        }
        else
        {
            _moveStrategy = new MoveHorizontalLookDirection();
        }

        return this;
    }

    public EnemyBehavior SetAnimationController(int i)
    {
        if (i == 3)
        {
            anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/Enemy1AnimationController", typeof(RuntimeAnimatorController));
            
        }
        else if(i == 2)
        {
            anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/Enemy2AnimationController", typeof(RuntimeAnimatorController));
        }
        else if(i==1)
        {
            anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/Enemy3AnimationController", typeof(RuntimeAnimatorController));
        }
        return this;
    }
}
