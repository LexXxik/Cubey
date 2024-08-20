using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiddleText : MonoBehaviour {

    public string[] pauseText;
    public GameObject UiController;

    private int order = 0;

    private void Start()
    {
        gameObject.GetComponent<Animator>().SetBool("Resume", true);
        if (gameObject.GetComponent<Animator>().GetBool("Dead") == false)
            this.gameObject.GetComponent<Text>().text = pauseText[0];
    }

    public void Texting()
    {
        if (this.gameObject.name == "MiddleText")
        {
            order++;
            if (order == pauseText.Length)
            {
                UiController.GetComponent<Level1UI>().RealPlay();
                order = 0;
                this.gameObject.GetComponent<Text>().text = pauseText[order];
                gameObject.GetComponent<Animator>().SetBool("Resume", false);
                this.gameObject.SetActive(false);
            }
            else
            this.gameObject.GetComponent<Text>().text = pauseText[order];
        }
    }
    
    public void GameOverUi2()
    {
        PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore") + UiController.GetComponent<Level1UI>().score);
        PlayerPrefs.SetInt("LastScore", UiController.GetComponent<Level1UI>().score);

        if (UiController.GetComponent<Level1UI>().score > PlayerPrefs.GetInt("HighScore"))
        {
            UiController.GetComponent<Level1UI>().HighScore.text = "NEW High Score: " + UiController.GetComponent<Level1UI>().score.ToString();
            PlayerPrefs.SetInt("HighScore", UiController.GetComponent<Level1UI>().score);

        }
        UiController.GetComponent<Level1UI>().Score.text = UiController.GetComponent<Level1UI>().score.ToString();

        UiController.GetComponent<Level1UI>().Score.GetComponent<Text>().color = new Color32(255,255,255,255);
        UiController.GetComponent<Level1UI>().HighScore.GetComponent<Text>().color = new Color32(255, 255, 255, 255);

        Time.timeScale = 0;
        for (int i = 0; i < UiController.GetComponent<Level1UI>().LooseUI2.Length; i++)
            UiController.GetComponent<Level1UI>().LooseUI2[i].SetActive(true);

        UiController.GetComponent<Level1UI>().AnimatingUI(UiController.GetComponent<Level1UI>().LooseUI2, "NextScene");
    }

}
