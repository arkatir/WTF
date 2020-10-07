using UnityEngine;

public class DivideGunProjectile : MonoBehaviour
{
    public float speed = 2f;
    public float spread = 3f;
    public float timeToLive = 10f;

    private float _startTime;

    private void OnEnable()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= _startTime + timeToLive)
        {
            ObjectPoolManager.managerInstance.RemoveObject(gameObject);
        }
        else
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyStats enemyStats))
        {
            int newHealth = enemyStats.GetHealth() / 2;
            enemyStats.SetHealth(newHealth > 0 ? newHealth : 1);
            enemyStats.transform.localScale *= 0.75f;
            //GameObject clone = Instantiate(other.gameObject, (transform.up * 2 + Random.Range(-1f, 1f) * transform.right) * spread, Quaternion.identity);
        }
        if (!other.TryGetComponent(out DivideGun divideGun) && !other.TryGetComponent(out PlayerStats playerStats))
        {
            ObjectPoolManager.managerInstance.RemoveObject(gameObject);
        }
    }
}
