using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private Animator _animator;
    private Rigidbody2D _rb2d;

    private bool _isMoving = false;
    private Vector2 _targetPosition = Vector2.zero;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _isMoving = true;
            _animator.SetBool("isWalking", true);

            Vector2 direction = _targetPosition - _rb2d.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        // Move towards the target position
        if (_isMoving)
        {
            _rb2d.position = Vector2.MoveTowards(_rb2d.position, _targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving when the target position is reached
            if (_rb2d.position == _targetPosition)
            {
                _isMoving = false;
                _animator.SetBool("isWalking", false);
            }
        }
    }
}
