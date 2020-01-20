using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unirest_net.http;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class RecordHighscore : MonoBehaviour
{
    public Text playerHighScore;
    public Text recorded;
    public InputField inputField;
    public Text somethingWentWrong;
    public GameObject recordMyHS;
    public GameObject highScoreTable;
    public GameObject back;
    void Awake()
    {
        playerHighScore.text = "your highscore: \n" + PlayerPrefs.GetFloat("HighScore");
    }

    public void recordPlayerHS()
    {
        //to do foolprof
        var highscore = new HighscoreEntry()
        {
            name = inputField.text,   
            score = PlayerPrefs.GetFloat("HighScore"),
        };
        var json = JsonConvert.SerializeObject(highscore);

        var response = Unirest.post("http://localhost/highscores")
            .body(json)
            .asJson<string>();

        if (response.Code != 200)
        {
            Debug.Log("Something went wrong (responce error)");
            somethingWentWrong.enabled = true;
            recordMyHS.gameObject.SetActive(false);
            return;
        }

        recorded.enabled = true;
        back.gameObject.SetActive(true);
        recordMyHS.gameObject.SetActive(false);

        //else
        //{   
        //    somethingWentWrong.enabled = true;
        //    recordMyHS.gameObject.SetActive(false);
        //}
    }

    public void Cancel()
    {
        SceneManager.LoadScene("Highscores");
        //recordMyHS.gameObject.SetActive(false);
        //highScoreTable.gameObject.SetActive(true);
        //back.gameObject.SetActive(false);
        //recorded.enabled = false;
    }

    class HighscoreEntry
    {
        public float score;
        public string name;
    }


}
