using UnityEngine;
using UnityEngine.InputSystem;

public class CamerController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float zoomSpeed = 1.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 10.0f;
    public float currentZoom = 6.0f;
    public float pitch = 2f;

    private void Update()
    {
        currentZoom -= Mouse.current.scroll.ReadValue().y * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void LateUpdate()
    {
        //offset.x = offset.x * -1;
        transform.position = target.position + offset;
        transform.LookAt(target.position + Vector3.up * pitch);
        Camera camera = gameObject.GetComponent<Camera>();
        camera.orthographicSize = currentZoom;
    }
}
