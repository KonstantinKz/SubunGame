using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unirest_net.http;
using Newtonsoft.Json;
using System;
using UnityEngine.SceneManagement;

public class HighscoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public Text somethingWentWrong;
    public GameObject recordMyHS;
    public GameObject highScoreTable;
    List<HighscoreEntry> highscoreEntryList;
    float templateHeight = 100f;

    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
        highscoreEntryList = new List<HighscoreEntry>();
        GetHighscores();
    }

    class HighscoreEntry
    {
        public float score;
        public string name;
    }

    async void GetHighscores()
    {
        var response = await Unirest.get("http://localhost/highscores").asJsonAsync<string>();
        
        if (response.Code != 200)
        {
            somethingWentWrong.enabled = true;
            entryTemplate.gameObject.SetActive(false);
            entryContainer.gameObject.SetActive(false);
            return;
        }
        
        try
        {
            highscoreEntryList = JsonConvert.DeserializeObject<List<HighscoreEntry>>(response.Body);

            for (int i = 0; i < highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscoreEntryList.Count; j++)
                {
                    if (highscoreEntryList[j].score > highscoreEntryList[i].score)
                    {
                        HighscoreEntry tmp = highscoreEntryList[i];
                        highscoreEntryList[i] = highscoreEntryList[j];
                        highscoreEntryList[j] = tmp;
                    }
                }
            }

            int limit = 0;

            foreach (var highscoreEntry in highscoreEntryList)
            {
                if (limit < 12)
                {
                    Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                    RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                    entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * limit);
                    entryTransform.gameObject.SetActive(true);


                    entryTransform.Find("Score").GetComponent<Text>().text = highscoreEntry.score.ToString();
                    entryTransform.Find("Name").GetComponent<Text>().text = highscoreEntry.name;
                    limit++;
                }

            }

        }
        catch (Exception)
        {
            somethingWentWrong.enabled = true;
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Record()
    {
        highScoreTable.gameObject.SetActive(false);
        recordMyHS.gameObject.SetActive(true);
    }
}
