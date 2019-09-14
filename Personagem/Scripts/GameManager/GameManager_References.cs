using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_References : MonoBehaviour
{
    public string playerTag;
    public static string _playerTag;

    public string enemyTag;
    public static string _enemyTag;

    public string assetPlayerTag;
    public static string _assetPlayerTag;

    public static GameObject _player;
    public static GameObject _assetPlayer;

    void OnEnable()
    {
        _playerTag = playerTag;
        _enemyTag = enemyTag;
        _assetPlayerTag = assetPlayerTag;

        _player = GameObject.FindGameObjectWithTag(_playerTag);
        _assetPlayer = GameObject.FindGameObjectWithTag(_assetPlayerTag);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
