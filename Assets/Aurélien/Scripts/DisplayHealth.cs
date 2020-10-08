using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    private PlayerStats playerstats;

    // Start is called before the first frame update
    void Start()
    {
        playerstats = GetComponentInParent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale =new Vector3((float)playerstats.GetHealth() / (float)playerstats.GetMaxHealth(), 1.0f, 1.0f);
    }
}
