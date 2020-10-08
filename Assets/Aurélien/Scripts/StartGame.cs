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
    public Text loadingText;
    public Image loading;

    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Button>().onClick.AddListener(Controls);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Controls()
    {
        titleText.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        controlsText.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(true);
        loading.gameObject.SetActive(true);
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        AsyncOperation result = SceneManager.LoadSceneAsync("Map");
        while (!result.isDone)
        {
            float progress = Mathf.Clamp01(result.progress);
            loading.transform.localScale = new Vector3(progress, 1, 1);
            loadingText.text = "Loading : " + (progress * 100) + "%";
            yield return null;
        }
        
        
    }
}
