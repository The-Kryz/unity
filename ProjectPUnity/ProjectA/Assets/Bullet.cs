using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb; // Precisamos do componente f�sico
    public float damage = 5f; // Dano do tiro

    void Start()
    {
        // Assim que a bala nasce, aplicamos velocidade na dire��o "Cima" dela
        // Como a bala gira junto com a nave, o "Cima" dela � a frente do tiro
        rb.linearVelocity = transform.up * speed;
    }

    // Se sair da tela, destr�i (opcional, mas bom pra limpar mem�ria)
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}