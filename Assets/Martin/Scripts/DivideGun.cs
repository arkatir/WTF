using UnityEngine;

public class DivideGun : SlotItem
{
    public GameObject projectilePrefab;
    public float groundRotationSpeed = 60f;
    //public float firingRotationSpeed = 270f;
    public float minIntervalBetweenShots = 0.2f;

    private bool _held = true;
    private float _lastShot;

    // Update is called once per frame
    void Update()
    {
        if (_held)
        {
            if (Input.GetMouseButton(0))
            {
                //transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + Time.deltaTime * firingRotationSpeed);
                if (Time.time >= _lastShot + minIntervalBetweenShots)
                {
                    Instantiate(projectilePrefab, transform.position, transform.rotation);
                    _lastShot = Time.time;
                }
            }
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + Time.deltaTime * groundRotationSpeed, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Destroy(gameObject);
        }
    }
}
