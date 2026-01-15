using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para mexer nas Cenas

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Descongela o tempo (Importante! Senão o jogo volta pausado)
        Time.timeScale = 1f;

        // Recarrega a cena que está aberta agora
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}