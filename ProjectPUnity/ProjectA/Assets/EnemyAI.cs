using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public float health = 10f; // Agora tem vida!
    public float damage = 1f;  // Dano que ele dá no player

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameObject.Find("Player") != null)
            player = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    // --- NOVA FUNÇÃO: CONFIGURAR DIFICULDADE ---
    // O Spawner vai chamar isso assim que o inimigo nascer
    public void SetDifficulty(float multiplier)
    {
        health *= multiplier; // Aumenta vida
        speed *= multiplier;  // Aumenta velocidade (cuidado pra não ficar impossível)
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Dá XP para o jogador antes de morrer
        if (player != null)
        {
            player.GetComponent<PlayerController>().GainExperience(10);
        }

        // Dá pontos
        if (ScoreManager.instance != null) ScoreManager.instance.AddScore(10);

        Destroy(gameObject);
    }

    // Colisão com a Bala
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Pega o dano da bala (vamos configurar a bala jajá)
            Bullet bulletScript = other.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                TakeDamage(bulletScript.damage);
            }

            Destroy(other.gameObject); // Destroi a bala
        }
    }

    // Colisão com Player (igual antes)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            if (playerScript != null) playerScript.TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }
}