using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Arraste o Player aqui
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // Deixe Z = -10

    // LIMITES DO MAPA
    public bool ativarLimites = true;
    public Vector2 minLimit; // O canto inferior esquerdo
    public Vector2 maxLimit; // O canto superior direito

    void LateUpdate()
    {
        if (target != null)
        {
            // 1. Calcula onde a câmera QUER ir
            Vector3 desiredPosition = target.position + offset;

            // 2. Aplica suavidade (Lerp)
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 3. TRAVA A CÂMERA NOS LIMITES (Clamp)
            if (ativarLimites)
            {
                // Impede que o X seja menor que o min ou maior que o max
                smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minLimit.x, maxLimit.x);
                // Mesma coisa para o Y
                smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minLimit.y, maxLimit.y);
            }

            // 4. Aplica a posição final
            transform.position = smoothedPosition;
        }
    }
}