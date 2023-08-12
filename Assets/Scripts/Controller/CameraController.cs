using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform player;
    [SerializeField] float smoothing = 0.2f;
    public float depth = 3f;
    [SerializeField] Vector2 minCameraBoundary;
    [SerializeField] Vector2 maxCameraBoundary;

    private void Start()
    {

    }
    private void FixedUpdate()
    {
        if (player == null) return;
        Vector3 targetPos = new Vector3(player.position.x, player.position.y + 3f, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }

    public IEnumerator ZoonIn(float target) {
        Camera camera = Camera.main;
        while (camera.orthographicSize < target)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, target, Time.deltaTime / 0.5f);
            yield return null;
        }
    }
    public IEnumerator ZoonOut(float target)
    {
        Camera camera = Camera.main;
        while (camera.orthographicSize > target)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, target, Time.deltaTime / 0.5f);
            yield return null;
        }
    }
    public void SetCameratoPlayer(Transform transform) {
        player = transform;
    }
}
