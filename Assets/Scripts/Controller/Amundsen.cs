using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amundsen : BaseController
{
    private bool canDoubleJump = false;  // ���� ������ �������� Ȯ���ϴ� ����
    private bool isDoubleJump = false;  // ���� ������ �������� ���θ� �����ϴ� ����
    private int jumpCount = 0; // ���� ������ Ƚ��

    public override void Update()
    {
        if (GameManager.Instance.isPaused)
            return;

        // ����Ű�� ������ ��
        if (Input.GetKeyDown(KeyCode.W))
        {
            // ó�� �����ϴ� ���
            if (_isGrounded)
            {
                jumpRequested = true;
                canDoubleJump = true; // ù��° ���� �Ŀ��� ���� ���� ����
            }
            // ���� ���� �����ϰ�, ���� ���߿� �ִٸ�
            else if (canDoubleJump)
            {
                // ���� ���� ����
                _rb.velocity = new Vector2(_rb.velocity.x, 0); // ���� ��� ���̸� �ӵ� �ʱ�ȭ (���� ����)
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

                if (_footAudio.isPlaying) _footAudio.Stop();
                _jumpAudio.clip = AudioClipJump;
                _jumpAudio.Play();
                animator.SetTrigger("Jump");

                canDoubleJump = false; // ���� ���� ��� �Ŀ��� ���� ���� �Ұ���
            }
        }
    }

    public override void ChangeCharacter()
    {
        base.ChangeCharacter();
        _coldGaugeReduced = 2;
    }
}

