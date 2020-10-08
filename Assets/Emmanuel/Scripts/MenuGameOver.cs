using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets.Characters.FirstPerson
{
    public class MenuGameOver : MonoBehaviour
    {
        public GameObject menuOver;
        public string sceneGame;
        public GameObject player;

        void Start()
        {
            //menuOver.SetActive(false);
        }

        void OnEnable()
        {
            player = GameObject.Find("Player");
            player.GetComponent<RigidbodyFirstPersonController>().mouseLook.lockCursor = false;
            ///Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;

        }
        void Update()
        {
        }
        public void Replay()
        {
            Debug.Log("test");
            SceneManager.LoadScene(sceneGame);
        }

        public void GameQuit()
        {
            Application.Quit();
            Debug.Log("tes");

        }
    }

}
