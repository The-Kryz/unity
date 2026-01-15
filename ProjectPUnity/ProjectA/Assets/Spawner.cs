using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1f;

    // Limites da Arena
    public float minX = -22f;
    public float maxX = 22f;
    public float minY = -22f;
    public float maxY = 22f;

    float nextSpawnTime = 0f;

    // --- NOVIDADE: DIFICULDADE ---
    public float difficultyMultiplier = 1f;
    public float difficultyIncreaseRate = 0.1f; // Aumenta 10% a cada ciclo

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;

            // Aumenta a dificuldade bem devagarinho a cada inimigo ou use um Timer separado
            difficultyMultiplier += 0.01f;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPos = new Vector2(randomX, randomY);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // Aplica a dificuldade no inimigo que acabou de nascer
        EnemyAI enemyScript = newEnemy.GetComponent<EnemyAI>();
        enemyScript.SetDifficulty(difficultyMultiplier);
    }
}