using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    // public
    public GameObject[] cloudPrefabs;
    
    public bool gameLost;
    
    // private
    private GameObject _clouds;
    private const int CloudLimit = 10;
    private int _cloudAmount;
    private int _cloudsZPos = 1;
    
    private GameObject _gura;
    private float _playerVerticalExtent;
    private float _playerHorizontalExtent;

    private bool _gameStarted;

    private Camera _camera;
    
    private float _windowHorizontalExtent;
    private float _windowVerticalExtent;
    
    void Start()
    {
        _gura = GameObject.FindGameObjectWithTag("Player");
        _playerHorizontalExtent = _gura.GetComponent<Collider2D>().bounds.size.x / 2;
        _playerVerticalExtent = _gura.GetComponent<Collider2D>().bounds.size.y / 2;

        _clouds = GameObject.FindGameObjectWithTag("Clouds");
        
        _camera = Camera.main;
        
        _windowVerticalExtent = _camera.orthographicSize;
        _windowHorizontalExtent = _windowVerticalExtent * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameStarted)
        {
            StartGame();
        }
        else
        {
            CloudLoop();

            if (gameLost)
            {
                GameLost();
            }
        }
    }

    private void CloudsFirstSpawn()
    {
        float r0X = Random.Range(-_windowHorizontalExtent, _windowHorizontalExtent);
        float r0Y = Random.Range(-_windowVerticalExtent, -_windowVerticalExtent - _playerVerticalExtent);
        GameObject firstCloud= Instantiate(GetRandomCloudPrefab(), new Vector3(r0X, r0Y, _cloudsZPos), Quaternion.identity);
        firstCloud.transform.parent = _clouds.transform;

        for (int i = 1; i < CloudLimit; ++i)
        {
            GameObject newCloud = Instantiate(GetRandomCloudPrefab(), new Vector3(r0X, r0Y, _cloudsZPos), Quaternion.identity);
            newCloud.transform.parent = _clouds.transform;
            CloudReset(i);
        }
    }

    private void CloudReset(int index)
    {
        int prevIndex = index - 1;
        if (prevIndex < 0)
        {
            prevIndex = CloudLimit - 1;
        }

        float prevX = _clouds.transform.GetChild(prevIndex).transform.position.x;
        float prevY = _clouds.transform.GetChild(prevIndex).transform.position.y;

        float rx = Random.Range(-_windowHorizontalExtent, _windowHorizontalExtent);
        float ry = Random.Range(prevY - 10, prevY - 3);
        
        if (rx > _windowHorizontalExtent)
        {
            rx -= _windowHorizontalExtent * 2;
        }

        _clouds.transform.GetChild(index).transform.position = new Vector3(rx, ry, _cloudsZPos);
    }
    
    void CloudLoop()
    {
        for (int i = 0; i < _clouds.transform.childCount; i++)
        {
            if (_clouds.transform.GetChild(i).transform.position.y > _camera.transform.position.y + _windowVerticalExtent + 3)
            {
                CloudReset(i);
            }
        }
    }
    
    GameObject GetRandomCloudPrefab()
    {
        int rng = Random.Range(0, cloudPrefabs.Length - 1);
        return cloudPrefabs[rng];
    }

    void StartGame()
    {
        if (_gura.transform.position.y < -1)
        {
            CloudsFirstSpawn();
            _gameStarted = true;
        }
    }

    void GameLost()
    {
        Debug.Log("Game Lost");
    }
}
