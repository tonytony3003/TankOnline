using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Tank Settings")]
    [SerializeField] float _speed = 2f;
    [SerializeField] float _rotateSpeed = 90f;
    [SerializeField] float _rotateTurretSpeed = 180f;
    [SerializeField] GameObject _turret;

    [Header("NavMesh")]
    [SerializeField] Transform _target;
    private NavMeshAgent _agent;
    private NavMeshPath _path;
    private int _currentCorner = 0;

    private float _repathTimer = 0f;
    [SerializeField] float _repathInterval = 0.3f; // cập nhật 3 lần/giây

    private Vector3 _lastTargetPos;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = false;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _path = new NavMeshPath();
        _lastTargetPos = _target.position;
        UpdatePath();
    }

    void Update()
    {
        if (_target == null) return;

        // 🔁 Chỉ cập nhật path khi target di chuyển đủ xa
        _repathTimer += Time.deltaTime;
        if (_repathTimer >= _repathInterval &&
            Vector3.Distance(_target.position, _lastTargetPos) > 0.3f)
        {
            _lastTargetPos = _target.position;
            UpdatePath();
            _repathTimer = 0f;
        }

        // 🚗 Di chuyển theo NavMesh path
        if (_path.corners.Length > 1 && _currentCorner < _path.corners.Length)
        {
            MoveAlongPath();
        }

        // 🎯 Xoay turret về target
        if (_turret != null)
        {
            Vector3 dirToTarget = _target.position - _turret.transform.position;
            float targetAngle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRot = Quaternion.Euler(0, 0, targetAngle);
            _turret.transform.rotation = Quaternion.RotateTowards(
                _turret.transform.rotation,
                targetRot,
                _rotateTurretSpeed * Time.deltaTime
            );
        }
    }

    void UpdatePath()
    {
        if (_agent.CalculatePath(_target.position, _path))
        {
            _currentCorner = 1;
        }
    }

    void MoveAlongPath()
    {
        if (_currentCorner >= _path.corners.Length) return;

        Vector3 nextCorner = _path.corners[_currentCorner];
        Vector3 dir = (nextCorner - transform.position).normalized;

        // 🚙 Xoay mượt theo hướng di chuyển
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRot = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            _rotateSpeed * Time.deltaTime
        );
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.5f, LayerMask.GetMask("Wall"));
        Debug.DrawRay(transform.position, transform.up * 0.5f, hit.collider ? Color.red : Color.green);
        // Khi xoay đúng hướng thì tiến tới
        float angleDiff = Quaternion.Angle(transform.rotation, targetRot);
        if (angleDiff < 10f && hit.collider == null)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Translate(-Vector3.up * _speed*1.2f * Time.deltaTime, Space.Self);
        }

        // 📍 Nếu đến gần corner thì đi corner tiếp theo
        if (Vector3.Distance(transform.position, nextCorner) < 0.2f)
        {
            _currentCorner++;
        }
    }
}
