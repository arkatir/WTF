using UnityEngine;

public class FlyingThings : MonoBehaviour
{
    public enum Pattern
    {
        Random,
        UpAndDown
    }

    public Vector3 dimensions = Vector3.one;
    public GameObject thingPrefab;
    public int thingCount = 5;
    public float thingSpeed = 1f;
    public Color tintColor = Color.red;
    public Pattern pattern;

    private FlyingThing[] _things;
    //private float[] _speeds;
    private Bounds _bounds;

    // Start is called before the first frame update
    void Start()
    {
        _bounds = new Bounds(transform.position, dimensions);
        _things = new FlyingThing[thingCount];
        //_speeds = new float[thingCount];
        for (int i = 0; i < thingCount; i++)
        {
            _things[i] = Instantiate(thingPrefab).GetComponent<FlyingThing>();
            _things[i].transform.position = new Vector3(transform.position.x + Random.Range(-_bounds.extents.x, _bounds.extents.x),
                transform.position.y + Random.Range(-_bounds.extents.y, _bounds.extents.y),
                transform.position.z + Random.Range(-_bounds.extents.z, _bounds.extents.z));
            if (pattern == Pattern.Random)
            {
                _things[i].transform.localRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            }
            else
            {
                _things[i].transform.LookAt(_things[i].transform.position + Vector3.up);
            }
            _things[i].transform.SetParent(transform);
            //_speeds[i] = Random.Range(0.5f, 1.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _things.Length; i++)
        {
            Vector3 newPos = _things[i].transform.position + _things[i].transform.forward * thingSpeed * /* _speeds[i] * */ Time.deltaTime;
            if (_bounds.Contains(newPos))
            {
                _things[i].transform.position = newPos;
            }
            else
            {
                if (pattern == Pattern.Random)
                {
                    //_things[i].transform.LookAt(_things[i].transform.position - _things[i].transform.forward);
                    _things[i].transform.localRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                }
                else
                {
                    _things[i].transform.LookAt(_things[i].transform.position + (_things[i].transform.forward.y >= 0f ? -Vector3.up : Vector3.up));
                }                
            }
            _things[i].transform.localScale = (1.5f + 0.5f * Mathf.Sin(Time.time)) * Vector3.one;
            _things[i].Tint(tintColor, 0.5f + 0.5f * Mathf.Sin(Time.time));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, dimensions);
    }
}
