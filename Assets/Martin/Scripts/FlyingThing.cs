using UnityEngine;

public class FlyingThing : MonoBehaviour
{
    private MaterialPropertyBlock _properties;
    private Color _emissionColor;
    private MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _properties = new MaterialPropertyBlock();
        _renderer = GetComponentInChildren<MeshRenderer>();
        _renderer.GetPropertyBlock(_properties);
        _emissionColor = _properties.GetColor("_EmissionColor");
    }

    public void Tint(Color color, float factor)
    {
        _properties.SetColor("_EmissionColor", Color.Lerp(_emissionColor, color, factor));
        _renderer.SetPropertyBlock(_properties);
    }
}
