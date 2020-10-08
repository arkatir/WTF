using UnityEngine;

public class FlyingThing : MonoBehaviour
{
    private MaterialPropertyBlock _properties;
    private Color _baseColor;
    private MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _properties = new MaterialPropertyBlock();
        _renderer = GetComponentInChildren<MeshRenderer>();
        _renderer.GetPropertyBlock(_properties);
        _baseColor = _properties.GetColor("_BaseColor");
    }

    public void Tint(Color color, float factor)
    {
        _properties.SetColor("_BaseColor", Color.Lerp(_baseColor, color, factor));
        _renderer.SetPropertyBlock(_properties);
    }
}
