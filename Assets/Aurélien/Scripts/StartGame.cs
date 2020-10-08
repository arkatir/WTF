using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button button;
    public Text titleText;
    public Text controlsText;
    private bool controls = false;

    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Button>().onClick.AddListener(Controls);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && controls)
        {
            controlsText.gameObject.SetActive(false);
            SceneManager.LoadScene("Map");
        }
    }

    public void Controls()
    {
        titleText.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        controlsText.gameObject.SetActive(true);
        controls = true;
    }
}
