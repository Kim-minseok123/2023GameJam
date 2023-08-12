using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amundsen : BaseController
{
    private bool canDoubleJump = false;  // 더블 점프가 가능한지 확인하는 변수
    private bool isDoubleJump = false;  // 더블 점프를 실행할지 여부를 결정하는 변수
    private int jumpCount = 0; // 현재 점프한 횟수

    public override void Update()
    {
        if (GameManager.Instance.isPaused)
            return;

        // 점프키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.W))
        {
            // 처음 점프하는 경우
            if (_isGrounded)
            {
                jumpRequested = true;
                canDoubleJump = true; // 첫번째 점프 후에는 더블 점프 가능
            }
            // 더블 점프 가능하고, 현재 공중에 있다면
            else if (canDoubleJump)
            {
                // 더블 점프 로직
                _rb.velocity = new Vector2(_rb.velocity.x, 0); // 현재 상승 중이면 속도 초기화 (선택 사항)
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

                if (_footAudio.isPlaying) _footAudio.Stop();
                _jumpAudio.clip = AudioClipJump;
                _jumpAudio.Play();
                animator.SetTrigger("Jump");

                canDoubleJump = false; // 더블 점프 사용 후에는 더블 점프 불가능
            }
        }
    }

    public override void ChangeCharacter()
    {
        base.ChangeCharacter();
        _coldGaugeReduced = 2;
    }
}

