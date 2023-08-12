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
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
        prevGround = _isGrounded;
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);

        _moveInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_moveInput * _speed, _rb.velocity.y);

        if (jumpRequested)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            _isJumping = true;
            jumpRequested = false;
        }

        if (!_isGrounded && !_isJumping)
        {
            animator.SetTrigger("Air");
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
        if (_moveInput != 0) { 
            animator.SetBool("Walk", true);
        }
        else if (_moveInput == 0)
        {
            animator.SetBool("Walk", false);
        }
    }
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void Flip()
    {
        _isRight = !_isRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public virtual void UseSkill() { }
    public virtual void ChangeCharacter()
    {
        StartCoroutine(Camera.main.GetComponent<CameraController>().ZoonIn(5f));
        GameManager.Instance.SetPlayer(this.gameObject, _coldGaugeReduced);
    }
}
