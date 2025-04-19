using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    
    public CinemachineCamera camManager;
    public float zoomSpeed = 1.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 10.0f;
    public float currentZoom = 6.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        currentZoom -= Mouse.current.scroll.ReadValue().y * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        camManager.Lens.OrthographicSize = currentZoom;
        
    }
}
