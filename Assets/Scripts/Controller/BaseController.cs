using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public float _speed;
    private float _moveInput;
    private bool _isRight = true;

    public float _jumpForce;
    public float _checkRadius;
    private bool _isGrounded;
    public Transform _groundCheck;
    public LayerMask _groundLayer;

    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);

        _moveInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_moveInput * _speed, _rb.velocity.y);

        if (_moveInput > 0 && !_isRight)
        {
            Flip();
        }
        else if (_moveInput < 0 && _isRight) {
            Flip();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _isGrounded) {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Flip() { 
        _isRight = !_isRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public virtual void UseSkill() { }
}
