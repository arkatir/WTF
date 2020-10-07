using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
<<<<<<< Updated upstream
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int health;
=======
>>>>>>> Stashed changes
    void Start()
    {
        
    }

<<<<<<< Updated upstream
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

    public int SetHealth(int val)
    {
        health = val;
        return health;
    }

=======
>>>>>>> Stashed changes
    // Update is called once per frame
    void Update()
    {
        
    }
}
