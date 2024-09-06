using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletView : MonoBehaviour
{
    private Transform _transform;
    private Vector2 _platformPosition;
    private LayerMask _layerMaskRigidboby;

    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private CircleCollider2D _circleCollider2;
    [SerializeField] private InputMouseBullet _input;
    [SerializeField] private ParticleSystem _motionTrail;

    public Vector2 StartPosition => _startPosition;
    public InputMouseBullet Input => _input;
    [HideInInspector]
    public UnityEvent<Vector2> OnCollisionEventHandler = new UnityEvent<Vector2>();

    public void UpdateVelocity(Vector2 target)
    {
        if (target != Vector2.zero)
            _motionTrail.gameObject.SetActive(true);

        _rigidbody2D.velocity = target.normalized * _speed;
    }

    public void UpdatePosition(Vector2 newPosition) => _transform.position = newPosition;

    public void UpdatePlatformPosition(Vector2 newPosition) => _platformPosition = newPosition;

    private void Start()
    {
        _transform = transform;
        _motionTrail.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var wallView = collision.gameObject.GetComponent<WallView>();
        if (wallView != null)
        {
            OnCollisionEventHandler?.Invoke(collision.relativeVelocity);

            _layerMaskRigidboby = LayerMask.GetMask("Bonus", "Bullet");
            _rigidbody2D.excludeLayers = _layerMaskRigidboby;
        }

        var candyView = collision.gameObject.GetComponent<CandyView>();
        if (candyView != null)
        {
            candyView.BrokeCandyEventHandler?.Invoke(candyView.transform);
            OnCollisionEventHandler?.Invoke(_platformPosition - (Vector2)_transform.position);

            _layerMaskRigidboby = LayerMask.GetMask("Bonus", "Bullet");
            _rigidbody2D.excludeLayers = _layerMaskRigidboby;
            return;
        }

        var platfromView = collision.gameObject.GetComponent<PlatformView>();
        if (platfromView != null)
        {
            _motionTrail.gameObject.SetActive(false);
            OnCollisionEventHandler?.Invoke(Vector2.zero);

            _layerMaskRigidboby = LayerMask.GetMask("Platform", "Bonus", "Bullet");
            _rigidbody2D.excludeLayers = _layerMaskRigidboby;
        }
    }

    private void OnValidate()
    {
        if (_rigidbody2D == null)
            _rigidbody2D = transform.GetComponent<Rigidbody2D>();

        if (_circleCollider2 == null)
            _circleCollider2 = transform.GetComponent<CircleCollider2D>();
    }

    private void OnDestroy()
    {
        OnCollisionEventHandler.RemoveAllListeners();
    }
}
