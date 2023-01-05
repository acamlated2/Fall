using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    // private
    private GameObject _gura;

    private GameObject _gameController;
    
    void Start()
    {
        _gura = GameObject.FindGameObjectWithTag("Player");
        _gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject gura = _gura;
        Collider2D guraCollider = gura.GetComponent<Collider2D>();

        if (col == guraCollider)
        {
            _gameController.GetComponent<GameControllerScript>().gameLost = true;
        }
    }
}
