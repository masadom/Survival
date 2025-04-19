using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    public Transform target;
    public float normalSpeed = 5f;
    public float accelerationSpeed = 10f;
    public float maxDistance = 5f;
    public Vector2 offset = new Vector2(0f, 2f); // Offset w 2D (X i Y)

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z // Zachowaj g³êbokoœæ (Z) kamery
        );

        float distance = Vector3.Distance(transform.position, targetPosition);
        float currentSpeed = distance > maxDistance ? accelerationSpeed : normalSpeed;

        // P³ynne œledzenie (tylko X i Y)
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            1f / currentSpeed
        );
    }
}