using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Essa função vai aparecer no botão JOGAR
    public void Jogar()
    {
        // Salva que queremos ir para a cena 3 (O Jogo)
        PlayerPrefs.SetInt("CenaParaCarregar", 3);
        // Carrega a cena 2 (O Loading)
        SceneTransition.Instance.LoadScene("Loading");
    }

    // Essa função vai aparecer no botão SAIR
    public void Sair()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}