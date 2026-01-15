using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para mudar de cena

public class SplashManager : MonoBehaviour
{
    public float tempoDeEspera = 3f;

    void Start()
    {
        // "Agenda" a troca de cena para daqui a 3 segundos
        Invoke("IrParaMenu", tempoDeEspera);
    }

    void IrParaMenu()
    {
        // Carrega a cena de índice 1 (Menu) que configuramos no Build Settings
        SceneTransition.Instance.LoadScene("Menu");
    }
}