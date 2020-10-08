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
    private float health;

    public Animator m_EnemyAnimator;
    public MeleeEnemyController m_EnemyController;
    private AutoDestroy autoDestroy;
    void Start()
    {
        autoDestroy = GetComponent<AutoDestroy>();
    }

    public void OnEnable()
    {
        health = maxHealth;
        this.gameObject.transform.localScale = Vector3.one; 
    }

    public void AddHealth(int val)
    {
        health += val;
    }

    public void RemoveHealth(float val)
    {
        float removedVal = health - val;
        if (removedVal <= 0)
        {
            health = 0;
            if (m_EnemyController)
            {
                if (!m_EnemyController.isDying)
                {
                    StartCoroutine(RemoveAfterDeath());
                }

            }

            if(autoDestroy)
            {
                autoDestroy.beginExplosion();
            }
            
        }
        else
        {
            if (m_EnemyAnimator)
            {
                m_EnemyAnimator.SetTrigger("Hit");
            }
            health = removedVal;
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public float SetHealth(float val)
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
        if (m_EnemyController)
        {
            m_EnemyController.isDying = true;
            //this.GetComponent<Rigidbody>().isKinematic = true;
            m_EnemyController.nav.isStopped = true;
        }
        if (m_EnemyAnimator)
        {
            m_EnemyAnimator.SetTrigger("Death"); //launch death anim
                                                 //Fetch the current Animation clip information for the base layer
            var m_CurrentClipInfo = m_EnemyAnimator.GetCurrentAnimatorClipInfo(0);
            //Access the current length of the clip
            var m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
            yield return new WaitForSeconds(m_CurrentClipLength + 3f); //Wait for end of clip before removing enemy GameObject
        }
        ObjectPoolManager.managerInstance.RemoveObject(this.gameObject);
        yield return null;
    }
}
