using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUi : MonoBehaviour {

    public GameObject[] AllUI;

	void Start ()
    {
        AllUI[0].GetComponent<Text>().text = "Total Score: " + PlayerPrefs.GetInt("TotalScore");
        AllUI[1].GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        AllUI[2].GetComponent<Text>().text = "Last Game You Get..." + PlayerPrefs.GetInt("LastScore");
	}
	
	// Update is called once per frame
	public void OnPlay(string name)
    {
        for(int i = 0; i < AllUI.Length; i++)
        {
            AllUI[i].GetComponent<Animator>().SetBool(name, true);
        }
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
}
