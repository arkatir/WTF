using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text scoreText;

    private static Player _instance;
    private int _score;

    public static Player Get()
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    public void IncreaseScore(int count)
    {
        _score += count;
        scoreText.text = "Score : " + _score;
    }
}
