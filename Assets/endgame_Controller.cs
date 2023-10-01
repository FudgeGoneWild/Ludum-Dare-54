using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class endgame_Controller : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text highscore;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EndGame(int score)
    {
        canvas.SetActive(true);
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        text.SetText("HIGH SCORE : " + PlayerPrefs.GetInt("HighScore").ToString());
        highscore.SetText("CURRENT SCORE : " + score.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) { SceneManager.LoadScene("Playscreen"); }
    }
}
