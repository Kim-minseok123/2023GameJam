using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public float _speed = 5f;
    private float _moveInput;
    protected bool _isRight = true;

    public float _jumpForce = 6f;
    public float _checkRadius = 0.2f;
    private bool _isGrounded;
    public Transform _groundCheck;
    public LayerMask _groundLayer;
    private bool _isJumping = false;
    public Animator animator;
    private Rigidbody2D _rb;

    public float _coldGaugeReduced = 1;
    private bool jumpRequested = false;
    private bool prevGround;

    //모바일용
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private AudioSource _footAudio;
    public AudioClip AudioClipFoot;
    public AudioClip AudioClipJump;
    private AudioSource _jumpAudio;

    public Animator Effect;
    public AudioClip EffectSound;
    public AudioSource AudioClipEffect;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
        prevGround = _isGrounded;
        _footAudio = GetComponentInChildren<AudioSource>();
        _jumpAudio = gameObject.AddComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
#if UNITY_IOS || UNITY_ANDROID
            // 모바일에서 버튼 입력을 기반으로 moveInput 설정
            if(isMovingLeft)
            {
                _moveInput = -1;
            }
            else if(isMovingRight)
            {
                _moveInput = 1;
            }
            else
            {
                _moveInput = 0;
            }
#endif
            _moveInput = Input.GetAxis("Horizontal");
            _rb.velocity = new Vector2(_moveInput * _speed, _rb.velocity.y);

            if (jumpRequested)
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                if (_footAudio.isPlaying) _footAudio.Stop();
                _jumpAudio.clip = AudioClipJump;  // 변경된 오디오 소스
                _jumpAudio.Play();
                animator.SetTrigger("Jump");
                _isJumping = true;
                jumpRequested = false;
            }

            if (!_isGrounded && !_isJumping)
            {
                animator.SetTrigger("Air");
                _footAudio.Stop();
            }
            else if (_isGrounded && _isGrounded != prevGround)
            {
                animator.SetTrigger("Ground");
                _isJumping = false;
            }
            prevGround = _isGrounded;


            if (_moveInput > 0 && !_isRight)
            {
                Flip();
            }
            else if (_moveInput < 0 && _isRight)
            {
                Flip();
            }
            if (_moveInput != 0 && _isGrounded && !_isJumping)
            {
                if (!_footAudio.isPlaying)
                {
                    _footAudio.clip = AudioClipFoot;
                    _footAudio.Play();
                }
                animator.SetBool("Walk", true);
            }
            else if (_moveInput == 0)
            {
                if (_footAudio.isPlaying)
                {
                    _footAudio.Stop();
                }
                animator.SetBool("Walk", false);
            }
        }
    }
#if UNITY_STANDALONE || UNITY_WEBPLAYER
    public virtual void Update()
    {
        if (GameManager.Instance.isPaused)
            return;
        _moveInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            jumpRequested = true;
        }
    }
#endif

    private void Flip()
    {
        _isRight = !_isRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public virtual void UseSkill() {
        Effect.SetTrigger("EffectOn");
        AudioClipEffect.PlayOneShot(EffectSound);
    }
    public virtual void ChangeCharacter()
    {
        StartCoroutine(Camera.main.GetComponent<CameraController>().ZoonOut(5f));
        GameManager.Instance.SetPlayer(this.gameObject, _coldGaugeReduced);
        Effect.SetTrigger("EffectOn");
        AudioClipEffect.PlayOneShot(EffectSound);
    }
    // 모바일용 버튼 입력 처리 메서드들
    public void OnLeftButtonDown()
    {
        isMovingLeft = true;
    }

    public void OnLeftButtonUp()
    {
        isMovingLeft = false;
    }

    public void OnRightButtonDown()
    {
        isMovingRight = true;
    }

    public void OnRightButtonUp()
    {
        isMovingRight = false;
    }

    public void OnJumpButtonPressed()
    {
        if (_isGrounded)
            jumpRequested = true;
    }
}
