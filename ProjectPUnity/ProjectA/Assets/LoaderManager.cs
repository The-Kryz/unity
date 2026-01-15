using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoaderManager : MonoBehaviour
{
    [SerializeField]
    private Slider barraDeProgresso;

    private const int DefaultSceneIndex = 3;

    void Start()
    {
        // Pega o ID da cena que salvamos no Menu
        int cenaAlvo = PlayerPrefs.GetInt("CenaParaCarregar", DefaultSceneIndex);

        StartCoroutine(CarregarCenaAssincrona(cenaAlvo));
    }

    IEnumerator CarregarCenaAssincrona(int cenaIndex)
    {
        AsyncOperation operacao = SceneManager.LoadSceneAsync(cenaIndex);

        // Enquanto não terminar de carregar...
        while (!operacao.isDone)
        {
            float progresso = Mathf.Clamp01(operacao.progress / 0.9f);

            if (barraDeProgresso != null)
            {
                barraDeProgresso.value = progresso;
            }

            yield return null;
        }
    }
}