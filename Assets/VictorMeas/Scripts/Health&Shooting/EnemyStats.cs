using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Health manager for enemies.
/// </summary>
public class EnemyStats : MonoBehaviour
{
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
        int removedVal = health - val;
        if (removedVal < 0)
        {
            health = 0;
            ObjectPoolManager.managerInstance.RemoveObject(this.gameObject); //Sets enemy inactive on health to 0
        }
        else
        {
            health = removedVal;
        }
    }

    public int GetHealth()
    {
        return health;
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
