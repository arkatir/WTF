using UnityEngine;

public class WallGun : MonoBehaviour
{
    public GameObject wallsPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(wallsPrefab, transform.position + transform.forward * 3 + Vector3.up * 2, Quaternion.identity);
        }
    }
}
