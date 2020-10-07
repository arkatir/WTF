using UnityEngine;

public class DivideGun : SlotItem
{
    public GameObject projectilePrefab;
    public float groundRotationSpeed = 60f;
    public float minIntervalBetweenShots = 0.2f;
    public Material defaultMaterial;
    public Material groundMaterial;

    private bool _held;
    private float _lastShot;
    private string projectileName;

    public void Start()
    {
        projectileName = projectilePrefab.name;
        GetComponentInChildren<MeshRenderer>().sharedMaterial = groundMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (_held)
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time >= _lastShot + minIntervalBetweenShots)
                {
                    ObjectPoolManager.managerInstance.CreateObject(projectileName, transform.parent.position, transform.parent.rotation);
                    _lastShot = Time.time;
                }
            }
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + Time.deltaTime * groundRotationSpeed, 0);
        }
    }

    public override void OnInsert()
    {
        _held = true;
        GetComponentInChildren<MeshRenderer>().sharedMaterial = defaultMaterial;
        transform.SetParent(Player.Get().GetComponentInChildren<Camera>().transform);
        transform.localPosition = -0.5f * Vector3.up;
        transform.localRotation = Quaternion.AngleAxis(-10f, Vector3.right);
    }

    public override void OnRemove()
    {
        _held = false;
        GetComponentInChildren<MeshRenderer>().sharedMaterial = groundMaterial;
        Vector3 newPos = transform.parent.position + 3 * transform.parent.forward;
        transform.SetParent(null);        
        transform.position = new Vector3(newPos.x, 1, newPos.z);
        transform.localRotation = Quaternion.identity;
    }
}
