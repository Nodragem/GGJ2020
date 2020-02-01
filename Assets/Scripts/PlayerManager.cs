using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public static PlayerManager Instance;

    private int m_playerCount;
   
    public Action OnGameStart;
    public Material[] Materials;
    public PlayerInputManager InputManager;
    public Transform[] SpawnLocations;
    public bool isStarting;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        m_playerCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.maxPlayerCount == m_playerCount)
        {
            isStarting = true;
        }

        if (!isStarting) return;

        InputManager.DisableJoining();

        GameStart();
    }

    void OnPlayerJoined()
    {
        GameObject player = InputManager.playerPrefab;

        player.transform.position = SpawnLocations[m_playerCount].position;

        player.GetComponent<MeshRenderer>().material = Materials[m_playerCount];

        m_playerCount++;

        print("Player joined");
    }

    void GameStart()
    {
        OnGameStart?.Invoke();
    }
}
