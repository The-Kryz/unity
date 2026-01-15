using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance; // Permite chamar de qualquer lugar
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Awake()
    {
        // O "Pulo do Gato": faz esse objeto sobreviver entre as cenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Começa o jogo fazendo Fade Out (revelando a cena)
        StartCoroutine(Fade(0));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        // 1. Escurece a tela
        yield return StartCoroutine(Fade(1));
        // 2. Carrega a cena
        SceneManager.LoadScene(sceneName);
        // 3. Revela a nova cena
        yield return StartCoroutine(Fade(0));
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        // Se vamos escurecer (ir para 1), bloqueamos o clique na hora
        if (targetAlpha > 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        // SE O FADE TERMINOU E A TELA ESTÁ CLARA (Alpha 0)
        // Liberamos o clique para o jogador conseguir apertar os botões
        if (targetAlpha <= 0)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}