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

    public Animator m_EnemyAnimator;
    public MeleeEnemyController m_EnemyController;
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
            StartCoroutine(RemoveAfterDeath());
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

    private IEnumerator RemoveAfterDeath()
    {
        m_EnemyController.isDying = true;
        m_EnemyController.nav.isStopped = true;
        m_EnemyAnimator.SetTrigger("Death"); //launch death anim
        //Fetch the current Animation clip information for the base layer
        var m_CurrentClipInfo = m_EnemyAnimator.GetCurrentAnimatorClipInfo(0);
        //Access the current length of the clip
        var m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        Debug.Log("time of death is: " + m_CurrentClipLength.ToString());
        yield return new WaitForSeconds(m_CurrentClipLength); //Wait for end of clip before removing enemy GameObject
        ObjectPoolManager.managerInstance.RemoveObject(this.gameObject);
        yield return null;
    }
}
