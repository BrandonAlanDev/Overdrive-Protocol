using UnityEngine;
using UnityEngine.InputSystem;

public class IsoCameraFollow : MonoBehaviour
{
    public Transform player;
    public float height = 15f;
    public float distance = 15f;
    public float followSpeed = 5f;
    public float aimOffsetMultiplier = 3f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, height));

        Vector3 aimOffset = (mouseWorldPos - player.position).normalized * aimOffsetMultiplier;
        aimOffset.y = 0;

        Vector3 targetPos = player.position + aimOffset + new Vector3(0, height, -distance);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
        transform.LookAt(player);
    }
}
