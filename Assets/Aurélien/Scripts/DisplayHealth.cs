using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    private PlayerStats playerstats;
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        playerstats = GetComponentInParent<PlayerStats>();
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "Health : " + playerstats.GetHealth();
    }
}
