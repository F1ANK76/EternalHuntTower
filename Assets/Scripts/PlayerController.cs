using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 3f;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _input;
    private Vector2 _lastMove;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        // 이동 중이면 마지막 방향 갱신
        if (_input.sqrMagnitude > 0.01f)
        {
            _lastMove = _input;
        }

        // Blend Tree에 파라미터 전달
        _animator.SetFloat("MoveX", _input.x);
        _animator.SetFloat("MoveY", _input.y);

        _animator.SetFloat("LastMoveX", _lastMove.x);
        _animator.SetFloat("LastMoveY", _lastMove.y);
    }

    private void FixedUpdate()
    {
        Vector2 move = _input.normalized * MoveSpeed;
        _rigidbody2D.linearVelocity = move;
    }
}
