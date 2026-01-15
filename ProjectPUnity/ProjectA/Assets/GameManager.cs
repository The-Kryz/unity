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

    public void VoltarParaMenu()
    {
        // 1. Descongela o tempo (fundamental se o jogo estiver pausado)
        Time.timeScale = 1f;

        // 2. Chama a transição para a cena do Menu Principal
        // Substitua "MenuPrincipal" pelo nome exato da sua cena de menu
        SceneTransition.Instance.LoadScene("Menu");
    }
}