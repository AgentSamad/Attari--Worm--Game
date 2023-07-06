using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Camera cam;
    private Vector3 clampedPosition, mousePosition;

    [SerializeField] private Vector2 screenXBounds;
    [SerializeField] private Vector2 screenYBounds;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);


        clampedPosition = new Vector3(
            Mathf.Clamp(mousePosition.x, cam.ScreenToWorldPoint(Vector3.zero).x + screenXBounds.x,
                cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, 0)).x + screenXBounds.y),
            Mathf.Clamp(mousePosition.y, cam.ScreenToWorldPoint(Vector3.zero).y + screenYBounds.x,
                cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight / 2, 0)).y + screenYBounds.y),
            0
        );
        transform.position = clampedPosition;
    }
}