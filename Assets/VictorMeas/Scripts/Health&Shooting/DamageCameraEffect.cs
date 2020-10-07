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
    public float fadeTime;
    public Image imageToAffect;

    private UnityAction onPlayerDamageListener;
    private YieldInstruction fadeInstruction = new YieldInstruction();

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
        Debug.Log("damaged alpha: " + currentcol.a.ToString());
        imageToAffect.color = currentcol;
        StartCoroutine(FadeOut(imageToAffect));
        //StartCoroutine(SlowlyGoBackToNormal(0.1f));
    }

    
    IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            image.color = c;
        }
    }

    /*private IEnumerator SlowlyGoBackToNormal(float increment)
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
    }*/
}
