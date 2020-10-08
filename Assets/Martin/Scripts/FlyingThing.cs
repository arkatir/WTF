using UnityEngine;

public class FlyingThing : MonoBehaviour
{
    private Color _emissiveColor;
    private MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        _emissiveColor = _renderer.material.GetColor("_EmissiveColor");
    }

    public void Tint(Color color, float factor)
    {
        _renderer.material.SetColor("_EmissiveColor", Color.Lerp(_emissiveColor, color * _emissiveColor.a, factor));
    }
}
