using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float MoveSpeed = 2f;
    private float AttackCooldown = 0.5f;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Camera _mainCamera;

    private Vector2 _input;
    private Vector2 _lastMove;
    private float _lastAttackTime;

    public WeaponHitbox _weaponHitbox;
    public void DisableWeaponHitbox()
    {
        _weaponHitbox.DisableAll();
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        // 이동 입력
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        if (_input.sqrMagnitude > 0.01f)
        {
            _lastMove = _input;
        }

        // 이동 애니메이터 파라미터
        _animator.SetFloat("MoveX", _input.x);
        _animator.SetFloat("MoveY", _input.y);
        _animator.SetFloat("LastMoveX", _lastMove.x);
        _animator.SetFloat("LastMoveY", _lastMove.y);

        // 좌클릭 공격
        if (Input.GetMouseButtonDown(0) && Time.time >= _lastAttackTime + AttackCooldown)
        {
            TriggerAttack(true); // true = 좌클릭
            _lastAttackTime = Time.time;
        }

        // 우클릭 공격
        if (Input.GetMouseButtonDown(1) && Time.time >= _lastAttackTime + AttackCooldown)
        {
            TriggerAttack(false); // false = 우클릭
            _lastAttackTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        Vector2 move = _input.normalized * MoveSpeed;
        _rigidbody2D.linearVelocity = move;
    }

    private void TriggerAttack(bool isLeftClick)
    {
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 attackDirection = (mouseWorldPos - transform.position).normalized;

        _animator.SetFloat("AttackX", attackDirection.x);
        _animator.SetFloat("AttackY", attackDirection.y);

        _animator.SetBool("IsLeftClick", isLeftClick);
        _animator.SetTrigger("Attack");

        // 여기서 공격 방향에 맞는 히트박스 활성화
        if (_weaponHitbox != null)
        {
            if (Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y))
            {
                if (attackDirection.x > 0) _weaponHitbox.EnableHitbox("Right");
                else _weaponHitbox.EnableHitbox("Left");
            }
            else
            {
                if (attackDirection.y > 0) _weaponHitbox.EnableHitbox("Up");
                else _weaponHitbox.EnableHitbox("Down");
            }
        }
    }
}