using UnityEngine;

public class Interactible : MonoBehaviour
{
    public virtual void Interact()
    {
        // for ovverride
    }
    public float radius = 1f;
    Transform player;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Focused(Transform player)
    {
        float distance =  Vector3.Distance(player.position, transform.position);
        if (distance <= radius)
        {
            Interact();
        }
    }
}
