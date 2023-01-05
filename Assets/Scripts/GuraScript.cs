using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraScript : MonoBehaviour
{
    // public
    public enum States
    {
        Idle, 
        Walking, 
        Falling
    }

    public States currentState;
    
    // private
    private Vector2Int _dir;
    private const float Speed = 15;
    private float _jumpMultiplier;
    
    private float _dt;

    private Animator _anim;
    private HashIds _hash;

    private GameObject _mainPlatform;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIds>();
        currentState = States.Idle;

        _mainPlatform = GameObject.FindGameObjectWithTag("Main Platform");
    }
    
    void Update()
    {
        _dt = Time.deltaTime;
        ManageInput();
        PlatformCollider();
        
        // change to falling
        if (Speed * _dt * _dir.y * 1 - _jumpMultiplier < 0)
        {
            currentState = States.Falling;
        }
    }
    
    void FixedUpdate()
    {
        MovementManager();
    }

    void LateUpdate()
    {
        switch (currentState)
        {
            case States.Idle:
                _anim.SetInteger(_hash.playerState, 0);
                break;
            case States.Walking:
                _anim.SetInteger(_hash.playerState, 1);
                break;
            case States.Falling:
                _anim.SetInteger(_hash.playerState, 2);
                break;
        }
    }
    
    void ManageInput()
    {
        // x axis
        if ((Input.GetKeyUp("a")) ||
            (Input.GetKeyUp("d")))
        {
            _dir.x = 0;

            if (currentState != States.Falling)
            {
                currentState = States.Idle;
            }
        }

        if (Input.GetKey("d"))
        {
            _dir.x = 1;
            TurnRight();
            currentState = States.Walking;
        }
        if (Input.GetKey("a"))
        {
            _dir.x = -1;
            TurnLeft();
            currentState = States.Walking;
        }
    }
    
    void MovementManager()
    {
        // x axis
        transform.Translate(Speed * _dir.x * _dt, 0, 0);
        
        // y axis
        if (currentState == States.Falling)
        {
            transform.Translate(0, Speed * 0.1f * _dt * _dir.y * 1 - _jumpMultiplier, 0);
            if (_jumpMultiplier <= 0.5f)
            {
                _jumpMultiplier += 1.5f * _dt;
            }
        }

        if (_dir.y == 0)
        {
            _jumpMultiplier = 0;
        }
    }

    private void TurnLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }

    private void TurnRight()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void PlatformCollider()
    {
        if (_mainPlatform.GetComponent<PlatformScript>().stepped)
        {
            if (currentState != States.Falling)
            {
                if (_mainPlatform.GetComponent<PlatformScript>().PlayerFell(gameObject))
                {
                    _dir.y = 1;
                    _jumpMultiplier = 0.40f;
                    currentState = States.Falling;
                    _mainPlatform.GetComponent<PlatformScript>().stepped = false;
                }
            }
        }
    }
}
