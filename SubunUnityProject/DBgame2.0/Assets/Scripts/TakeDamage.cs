using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    public Canvas gameOverText;
    public Text score;
    public SpriteRenderer spriteRenderer;
    bool gameOver = false;
    public GameObject player;
    public Text highScoreText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Heart")
        {
            Debug.Log("Game Over");
            spriteRenderer.color = Color.red;
            Time.timeScale = 0f;
            score.enabled = false;
            highScoreText.text = "YOUR HIGHSCORE: \n" + PlayerPrefs.GetFloat("HighScore");
            gameOverText.enabled = true;
            gameOver = true;
            if (player.GetComponent<PlayerCombat>().score > PlayerPrefs.GetFloat("HighScore", 0))
            {
                PlayerPrefs.SetFloat("HighScore", player.GetComponent<PlayerCombat>().score);
                highScoreText.text = "YOUR HIGHSCORE: \n" + player.GetComponent<PlayerCombat>().score.ToString();
            }
        }

    }

    void Update()
    {
        if (gameOver == true)
        {
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
