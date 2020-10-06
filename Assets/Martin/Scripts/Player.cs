using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;

    public static Player Get()
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
