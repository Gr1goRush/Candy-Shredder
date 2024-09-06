using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletController
{
    private List<BulletView> _bulletsView;
    private List<Bullet> _bullets;

    public BulletController()
    {
        _bulletsView = new List<BulletView>();
        _bullets = new List<Bullet>();
    }

    public void OnInputEvent(Vector2 value)
    {
        var findedBullet = _bullets.Find(bulletTemp => bulletTemp.IsStopped());

        if (findedBullet != null)
            findedBullet.UpdateVelocity(value);
    }

    public void CreateBullets(BulletView bulletViewPrefab, Transform parent, Vector2 positionPlatform)
    {
        for (int i = 0; i < ContainerSaveerPlayerPrefs.Instance.SaveerData.CountBalls; i++)
        {
            var bulletView = Object.Instantiate(bulletViewPrefab, parent);
            var startPosition = new Vector2(positionPlatform.x, bulletView.transform.position.y);
            bulletView.transform.position = startPosition;
            var bullet = new Bullet(startPosition);
            Initialize(bulletView, bullet);

            _bulletsView.Add(bulletView);
            _bullets.Add(bullet);
        }
    }

    public void OnChangePostionPlatform(Vector2 position)
    {
        foreach (var bulletView in _bulletsView)
            bulletView.UpdatePlatformPosition(position);

        foreach (var bullet in _bullets)
            bullet.UpdatePosition(position);
    }

    private void Initialize(BulletView bulletView, Bullet bullet)
    {
        bulletView.OnCollisionEventHandler.AddListener(bullet.UpdateVelocity);
        bullet.OnUpdateVelocityEventHandler.AddListener(bulletView.UpdateVelocity);
        bullet.OnUpdatePositionEventHandler.AddListener(bulletView.UpdatePosition);
    }
}
