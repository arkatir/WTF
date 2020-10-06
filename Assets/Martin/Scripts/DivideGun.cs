using UnityEngine;

public class DivideGun : MonoBehaviour
{
    public GameObject projectilePrefab;

    private GameObject _currentProjectile;

    // Start is called before the first frame update
    void Start()
    {
        //_currentProjectile = Instantiate(projectilePrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("fired divide gun");
            _currentProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }
}
