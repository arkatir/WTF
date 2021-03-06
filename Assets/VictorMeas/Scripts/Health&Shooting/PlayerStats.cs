﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject gameOverUI;
    // Start is called before the first frame update
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int health;
    void Start()
    {
        
    }

    public void AddHealth(int val)
    {
        health += val;
    }

    public void RemoveHealth(int val)
    {
        EventManager.TriggerEvent("hurt");
        int removedVal = health - val;
        if (removedVal < 0)
        {
            health = 0;
            gameOverUI.SetActive(true);
        }
        else
        {
            EventManager.TriggerEvent("PlayerTakeDamage");
            health = removedVal;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int SetHealth(int val)
    {
        health = val;
        return health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
