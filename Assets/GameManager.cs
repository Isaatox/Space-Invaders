using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text scoreText;

    private int score = 0;
    public GameObject endSceneRoot;

    void Start()
    {
        if (endSceneRoot != null)
        {
            endSceneRoot.SetActive(false);
        }
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void PlayerLost()
    {
        if (endSceneRoot != null)
        {
            endSceneRoot.SetActive(true);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseScore(int invaderRank)
    {
        int points = 0;

        switch (invaderRank)
        {
            case 0:
                points = 50;
                break;
            case 1:
            case 2:
                points = 30;
                break;
            default:
                points = 10;
                break;
        }

        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
