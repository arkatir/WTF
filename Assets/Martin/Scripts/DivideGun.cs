using UnityEngine;

public class DivideGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float rotationSpeed = 60f;

    private bool held = true;

    // Update is called once per frame
    void Update()
    {
        if (held)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
            }
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + Time.deltaTime * rotationSpeed, 0);
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
