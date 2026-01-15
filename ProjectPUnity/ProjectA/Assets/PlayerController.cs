using UnityEngine;
using TMPro; // NECESSÁRIO para o texto de nível

public class PlayerController : MonoBehaviour
{
    [Header("Movimento e Física")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    [Header("Combate")]
    public Transform firePoint;     // A ponta da nave de onde sai o tiro
    public GameObject bulletPrefab; // O molde da bala
    public float damageMultiplier = 1f; // O quanto seu tiro fica mais forte (começa em 1x)
    public SpriteRenderer damageEffect;

    [Header("Vida e Game Over")]
    public int maxHealth = 3;       // O jogador morre com 3 toques
    int currentHealth;
    public GameObject gameOverPanel; // Arraste o painel vermelho aqui

    [Header("Sistema de Nível")]
    public int level = 1;           // Nível atual
    public int currentXP = 0;       // XP atual
    public int xpToNextLevel = 100; // Quanto precisa pra upar
    public TextMeshProUGUI levelText; // Arraste o Texto da UI pra cá

    void Start()
    {
        currentHealth = maxHealth;

        // Atualiza o texto do nível logo no começo
        UpdateLevelText();

        // Garante que a tela de Game Over comece escondida e o jogo rodando
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        Time.timeScale = 1f; // Garante que o jogo não comece pausado
    }

    void Update()
    {
        // 1. INPUT (Recebe os comandos)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Pega a posição do mouse na tela e converte para coordenadas do mundo
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Se clicar com botão esquerdo -> ATIRA
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // 2. FÍSICA (Executa o movimento)
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Girar (Matemática de vetores para olhar pro mouse)
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void Shoot()
    {
        // Cria a bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // --- MÁGICA DO NÍVEL ---
        // Pega o script da bala e multiplica o dano dela pelo seu nível de força
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = bulletScript.damage * damageMultiplier;
        }
    }

    // --- SISTEMA DE XP ---
    public void GainExperience(int amount)
    {
        currentXP += amount;

        // Verifica se encheu a barrinha de XP
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;                // Aumenta nível
        currentXP = 0;          // Zera XP (ou guarde a sobra se quiser: currentXP -= xpToNextLevel)

        // Aumenta a dificuldade do próximo nível em 20%
        xpToNextLevel = (int)(xpToNextLevel * 1.2f);

        // --- RECOMPENSAS ---
        damageMultiplier += 0.2f; // Tiros ficam 20% mais fortes
        moveSpeed += 0.5f;        // Fica um pouquinho mais rápido

        // Opcional: Recuperar vida ao passar de nível
        currentHealth = maxHealth;

        UpdateLevelText();
        Debug.Log("LEVEL UP! Agora você é nível " + level);
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + level.ToString();
        }
    }

    // --- FUNÇÃO DE TOMAR DANO ---
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        damageEffect.color = new Color(1f, 0f, 0f, 0.5f); // Flash vermelho
        Invoke("ResetDamageEffect", 0.2f); // Reseta o efeito após 0.2 segundos

        // Se a vida zerar, Game Over
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }
    void ResetDamageEffect()
    {
        // Volta para a cor original (Branco = sem tintura)
        damageEffect.color = Color.white;
    }

    void GameOver()
    {
        // Mostra a tela de Game Over
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        // Pausa o jogo (Congela o tempo)
        Time.timeScale = 0f;
    }
}