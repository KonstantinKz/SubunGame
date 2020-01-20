using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnLamps : MonoBehaviour
{
    public GameObject soulsPrefab;
    public GameObject heartsPrefab;
    public GameObject warningPrefab;

    public GameObject screenTop;
    public GameObject screenBottom;

    public GameObject player;

    public float soulsRespawnTime = 1.5f;
    public float heartsRespawnTime = 5f;
    private float heartsRandom;

    public Canvas gameOverText;
    public Text scoreText;
    public Text highScoreText;


    // Start is called before the first frame update
    void Start()
    {
        soulsPrefab.GetComponent<LampFall>().speed = 3f;
        StartCoroutine(spawnSouls());
        StartCoroutine(spawnHearts());
    }

    private void spawnSoul()
    {
        GameObject soul = Instantiate(soulsPrefab) as GameObject;
        soul.transform.position = new Vector2(Random.Range(-2.5f, 2.5f), screenTop.transform.position.y * 1.25f);
    }

    IEnumerator spawnSouls()
    {
        while (true)
        {
            yield return new WaitForSeconds(soulsRespawnTime);
            spawnSoul();
            if (soulsPrefab.GetComponent<LampFall>().speed < 6.0f)
            {
                soulsPrefab.GetComponent<LampFall>().speed += 0.01f;
            }
            if (soulsRespawnTime > 0.75f)
            {
                soulsRespawnTime -= 0.01f;
            }
        }
    }

    private void spawnHeart()
    {
        GameObject heart = Instantiate(heartsPrefab) as GameObject;
        heart.transform.position = new Vector2(heartsRandom = Random.Range(-2.5f, 2.5f), screenTop.transform.position.y * 2.25f);
        GameObject warning = Instantiate(warningPrefab) as GameObject;
        warning.transform.position = new Vector2(heartsRandom, 6.2f);
    }

    IEnumerator spawnHearts()
    {
        while (true)
        {
            yield return new WaitForSeconds(heartsRespawnTime);
            spawnHeart();
            if (heartsRespawnTime > 0.5)
            {
                heartsRespawnTime -= 0.1f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < screenBottom.transform.position.y)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
            scoreText.enabled = false;
            highScoreText.text = "YOUR HIGHSCORE: \n" + PlayerPrefs.GetFloat("HighScore");
            gameOverText.enabled = true;

            if (player.GetComponent<PlayerCombat>().score > PlayerPrefs.GetFloat("HighScore", 0))
            {
                PlayerPrefs.SetFloat("HighScore", player.GetComponent<PlayerCombat>().score);
                highScoreText.text = "YOUR HIGHSCORE: \n" + player.GetComponent<PlayerCombat>().score.ToString();
            }
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
            {
                Time.timeScale = 1.0f;
                gameOverText.enabled = false;
                SceneManager.LoadScene("Game");
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Time.timeScale = 1.0f;
                SceneManager.UnloadSceneAsync("Game");
                SceneManager.LoadScene("HighScores");
            }
        }   
    }
}
