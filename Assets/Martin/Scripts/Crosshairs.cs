using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Crosshairs : MonoBehaviour
{
    private static Crosshairs _instance;
    private Image _image;
    private RectTransform _rectTransform;

    public static Crosshairs Get()
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public Crosshairs SetColor(Color color)
    {
        _image.color = color;
        return this;
    }

    public Crosshairs ResetColor()
    {
        _image.color = Color.white;
        return this;
    }

    public Crosshairs SetAim(Vector3 position)
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(position);
        _rectTransform.localPosition = new Vector3(screenPoint.x - Screen.width / 2, screenPoint.y - Screen.height / 2, 0);
        return this;
    }

    public Crosshairs ResetAim()
    {
        _rectTransform.localPosition = Vector3.zero;
        return this;
    }
}
