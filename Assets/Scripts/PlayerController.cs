using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private PlayerBehavior _pawn;
    private float sensitivityAxis = 0.25f;
    Vector2 _direction;
    private float dirX = 0;
    private float dirY = 0;

    private bool blockMove = false;

    // Use this for initialization
    void Start()
    {
        _pawn = this.GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        DirectionController();
        CrouchController();
        LookUpController();
        MoveController();
        JumpController();
        AttackController();
        ChangeWeapon();
        StandUpController();

    }

    private void DirectionController()
    {
        //Debug.Log("H: " + Input.GetAxis("Horizontal") + " V: "+Input.GetAxis("Vertical"));

        dirX = Input.GetAxis("Horizontal");
 
        //if (dirX > sensitivityAxis || dirX < sensitivityAxis)
        {
            _direction = Vector2.right * dirX;
        }
        //else
        {
        //    _direction = new Vector2(0, _direction.y);
        }

        dirY = Input.GetAxis("Vertical");

        if (dirY > sensitivityAxis || dirY < sensitivityAxis)
        {
            //_direction = Vector2.up * dirY;
        }
        else
        {
            //_direction = new Vector2(_direction.x,dirY);
        }
        _pawn.LookDirection(_direction);
    }

    void CrouchController()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            _pawn.Crouch(true);
        }
        else
        {
            _pawn.Crouch(false);
        }
    }
    void LookUpController()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            _pawn.LookUp(true);
        }
        else
        {
            _pawn.LookUp(false);
        }
    }
    void MoveController()
    {
        if (!blockMove &&(_direction.x > sensitivityAxis || _direction.x < -sensitivityAxis))
        {
            _pawn.Move(_direction);
        }
    }
    void JumpController()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _pawn.Jump();
        }
    }
    void AttackController()
    {
        if (Input.GetButton("Fire1")){
            //Debug.Log("PlayerController: Attack");
            _pawn.Attack();
        }
        else
        {
            _pawn.IsShoot = false;
        }
    }
    void StandUpController()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            blockMove = true;
        }
        else
        {
            blockMove = false;
        }
    }
    void ChangeWeapon()
    {
        if (Input.GetButtonDown("ChangeWeapon")){
            _pawn.ChangeWeapon();
        }
    }
}
