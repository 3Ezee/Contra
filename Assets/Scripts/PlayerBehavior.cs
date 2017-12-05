using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : EntityBehavior {

    public Transform attackSpawner;
    //private bool isAtaccking = false;
    //private bool isWin = false;

    protected IJumpBehavior _jumpStrategy;

    //Booleans
    private bool isCrouch = false;
    private bool isLookUp = false;
    private bool isJump = false;
    private bool isShoot = false;

    //List<IAttackBehavior> _poolAttack;
    private int _numberAttacks = 2;
    private int _actualAttack = 0;

    Vector2 direction;
    void Start()
    {
        //_poolAttack = new List<IAttackBehavior>(); 
        _moveStrategy = new MoveHorizontal();
        _jumpStrategy = new JumpNormal();
        _attackStrategy = new Semiautomatic();

        initPoolAttack(); 
    }

    void initPoolAttack()
    {
        //_poolAttack.Add(new Semiautomatic());
        _poolAttack.Add(new Semiautomatic());
        _poolAttack.Add(new Spread());
        _poolAttack.Add(new Sinusoidal());
        
        //Debug.Log("hi");
        //Debug.Log(_poolAttack[1]);
    }
    void Update()
    {
        AnimationsPlayer();
        CheckJump();

        if(actualLife <= 0)
        {
            ResetThis();
            EventsManager.TriggerEvent(EventType.GP_Loose);

        }
    }

    public void Crouch(bool isC)
    {
        isCrouch = isC;
        if (isJump)
            isCrouch = false;
    }

    public void LookUp(bool isLU)
    {
        isLookUp = isLU;
        if (isJump)
            isLookUp = false;
    }

    public void LookDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
            this.transform.right = direction;
    }

    public void Move(Vector2 direction)
    {
            _moveStrategy.Move(rb, this.transform, direction, speed);
    }

    public void Jump()
    {
        _jumpStrategy.Jump(rb, jumpForce, isCollisionFloor);
        isJump = true;
    }

    public void Attack()
    {
        _attackStrategy.Attack(this.gameObject.layer, attackSpawner);
        isShoot = true;
    }

    private void CheckJump()
    {
        if (rb.velocity.y <0 && IsCollisionFloor)
            isJump = false;
    }
    //ChangeStrategies
    public void ChangeWeapon()
    {
        _actualAttack++;
        if (_actualAttack > _numberAttacks)
            _actualAttack = 0;
        _attackStrategy = _poolAttack[_actualAttack];
    }

    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);
        EventsManager.TriggerEvent(EventType.GP_Life, new object[] { actualLife });
    }
    //Get-Set
    public bool IsShoot
    {
        get
        {
            return IsShoot;
        }
        set
        {
            isShoot = value;
        }
    }

    //Animations
    private void AnimationsPlayer()
    {
        anim.SetFloat("SpeedX",Mathf.Abs(rb.velocity.x));
        anim.SetBool("IsCrouch", isCrouch);
        anim.SetBool("IsLookUp", isLookUp);
        anim.SetBool("IsJump", isJump);
        anim.SetBool("IsShoot", isShoot);
    }

}
