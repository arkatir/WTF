using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This script manages a damage effect on screen, by manipulating transparency of a UI image on Canvas
/// </summary>
public class DamageCameraEffect : MonoBehaviour
{

    public float nonDamageAlpha = 0f;
    public float damagedAlpha = 50f;
    public Image imageToAffect;

    private UnityAction onPlayerDamageListener;

    // Start is called before the first frame update
    void Awake()
    {
        onPlayerDamageListener = new UnityAction(OnDamageTaken);
    }

    private void OnEnable()
    {
        EventManager.StartListening("PlayerTakeDamage", onPlayerDamageListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerTakeDamage", onPlayerDamageListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDamageTaken()
    {
        Color currentcol = imageToAffect.color;
        currentcol.a = damagedAlpha;
        imageToAffect.color = currentcol;
        StartCoroutine(SlowlyGoBackToNormal(0.1f));
    }

    private IEnumerator SlowlyGoBackToNormal(float increment)
    {
        float currentAlpha = damagedAlpha - 1;
        while(currentAlpha > 0)
        {
            Color currentcol = imageToAffect.color;
            currentcol.a = currentAlpha;
            imageToAffect.color = currentcol;
            currentAlpha -= 1;
            yield return new WaitForSeconds(increment);
        }
        yield return null;
    }
}
