using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Controller : MonoBehaviour
{
    [SerializeField] Canvas pauseCanvas;
    private bool isOpen = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.enabled = !isOpen;
            isOpen = !isOpen;
            if (isOpen)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time .timeScale = 1.0f;
            }
        }
    }

    public void BackTostart()
    {
        SceneManager.LoadScene("StartScreen");
        Time.timeScale = 1.0f;
    }
}
