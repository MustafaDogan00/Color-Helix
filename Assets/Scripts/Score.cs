using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text _bestScoreText;
    private Text _scoreText;


    private void Awake()
    {
        _bestScoreText=transform.GetChild(0).GetComponent<Text>();
        _scoreText = transform.GetChild(1).GetComponent<Text>();
    }
    void Start()
    {
       


    }


    void Update()
    {
        if (PlayerScript.CameraSpeed() == 0)
        {
            _bestScoreText.gameObject.SetActive(true);
            _scoreText.gameObject.SetActive(false);
            print("AMINA KOYARIM");
        }
        else if (PlayerScript.CameraSpeed() != 0)
        {
            _bestScoreText.gameObject.SetActive(false);
            _scoreText.gameObject.SetActive(true);
        }
        _scoreText.text = GameController.Instance.score.ToString();

        if (GameController.Instance.score >= PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", GameController.Instance.score);
        }
        _bestScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

    }
}
