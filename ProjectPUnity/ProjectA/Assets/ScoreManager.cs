using UnityEngine;
using TMPro; // Biblioteca de Texto da Unity

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton (para acessar de qualquer lugar)

    public TextMeshProUGUI scoreText; // O texto na tela
    int score = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}