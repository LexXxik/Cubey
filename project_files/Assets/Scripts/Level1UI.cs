using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1UI : MonoBehaviour {

    public int score;

    public GameObject[] PauseUI;
    public GameObject[] PlayUI;
    public GameObject[] ScoreUI;
    public GameObject[] LooseUI1;
    public GameObject[] LooseUI2;

    public Text Score;
    public Text HighScore;
    public Text MiddleText;

    public bool reacting = true;
    private void Start()
    {
        Score.text = "0";
        HighScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void AnimatingUI(GameObject[] arr, string name)
    {


        for(int i = 0; i < arr.Length; i++)
        {
            arr[i].GetComponent<Animator>().SetBool(name, !arr[i].GetComponent<Animator>().GetBool(name));
        }

    }

    public void PauseGame()
    {
        if (reacting == true)
        {
            reacting = false;
            AnimatingUI(PlayUI, "NextScene");

            for (int i = 0; i < PauseUI.Length; i++)
            {
                PauseUI[i].SetActive(true);
            }
            AnimatingUI(PauseUI, "NextScene");

            Time.timeScale = 0;

        }
    }

    public void PlayGame()
    {
        if (reacting == true)
        {
            reacting = false;
            AnimatingUI(PauseUI, "NextScene");

            StartCoroutine(WaitForDisappear());

        }
    }

    IEnumerator WaitForDisappear()
    {
        yield return new WaitForSecondsRealtime(1);
        MiddleText.gameObject.SetActive(true);
    }

    public void RealPlay()
    {
        AnimatingUI(PlayUI, "NextScene");
        Time.timeScale = 1;
    }


    public void ReGame(string level)
    {
        if (reacting == true)
        {
            reacting = false;
            if (MiddleText.GetComponent<Animator>().GetBool("Dead") == false)
            {
                AnimatingUI(PauseUI, "NextScene");
            }
            AnimatingUI(ScoreUI, "NextScene");
            StartCoroutine(ReWait(1, level));
        }
    }
    IEnumerator ReWait(float time, string level)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void ScoreUp(int score)
    {
        Score.text = score.ToString();
    }

    public void Activation(GameObject activate, bool value)
    {
        activate.SetActive(value);
    }

    public void Reacting()
    {
        reacting = true;
    }

    public void GameOver(int iscore)
    {
        score = iscore;
        for(int i = 0; i < LooseUI1.Length; i++)
        {
            LooseUI1[i].SetActive(true);
        }
        AnimatingUI(LooseUI1, "Dead");
        MiddleText.text = "You loose!";
    }


    
}
