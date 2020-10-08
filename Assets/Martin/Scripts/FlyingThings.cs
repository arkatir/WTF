using System.Collections.Generic;
using UnityEngine;

public class FlyingThings : MonoBehaviour
{
    public Vector3 dimensions = Vector3.one;
    public GameObject thingPrefab;
    public int thingCount = 5;
    public float thingSpeed = 1f;

    private GameObject[] _things;

    // Start is called before the first frame update
    void Start()
    {
        _things = new GameObject[thingCount];
        for (int i = 0; i < thingCount; i++)
        {
            _things[i] = Instantiate(thingPrefab);
            _things[i].transform.position = new Vector3(transform.position.x + Random.Range(-dimensions.x, dimensions.x) * 0.5f,
                transform.position.y + Random.Range(-dimensions.y, dimensions.y) * 0.5f,
                transform.position.z + Random.Range(-dimensions.z, dimensions.z) * 0.5f);
            _things[i].transform.localRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            _things[i].transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = new Bounds(transform.position, dimensions);
        for (int i = 0; i < _things.Length; i++)
        {
            Vector3 newPos = _things[i].transform.position + _things[i].transform.forward * thingSpeed * Time.deltaTime;
            if (bounds.Contains(newPos))
            {
                _things[i].transform.position = newPos;
            }
            else
            {
                _things[i].transform.LookAt(_things[i].transform.position - _things[i].transform.forward);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, dimensions);
    }
}
