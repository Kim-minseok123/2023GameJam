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
    private bool isSkilled = false;
    private float moveSpeed = 8.0f; // ī�޶� �̵� �ӵ�
    public Vector3 centerPoint; // �߽� ����
    private float maxDistanceFromCenter = 10.0f; // �߽� �������� �ִ� �Ÿ�

    private void Start()
    {

    }
    private void FixedUpdate()
    {
        if (player == null) return;
        if (isSkilled)
        {
            Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            Vector3 moveVelocity = moveInput.normalized * moveSpeed;

            // ������ ī�޶� �̵�
            transform.Translate(moveVelocity * Time.deltaTime, Space.World);

            // �߽��������� �Ÿ� Ȯ�� �� �ʹ� �־����� �ٽ� ��ܿ�
            float distanceFromCenter = Vector3.Distance(transform.position, centerPoint);
            if (distanceFromCenter > maxDistanceFromCenter)
            {
                Vector3 fromCenterToCamera = transform.position - centerPoint;
                Vector3 newCameraPosition = centerPoint + (fromCenterToCamera.normalized * maxDistanceFromCenter);
                transform.position = newCameraPosition;
            }

        }
        else {
            Vector3 targetPos = new Vector3(player.position.x, player.position.y + 0.5f, this.transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
    public void HansenSkillOn() {
        centerPoint = new Vector3(player.position.x, player.position.y +0.5f, this.transform.position.z); ;
        isSkilled = true;
    }
    public void HansenSkillOff() {
        isSkilled = false;
    }
    public void SetCameratoPlayer(Transform transform) {
        player = transform;
    }
}
