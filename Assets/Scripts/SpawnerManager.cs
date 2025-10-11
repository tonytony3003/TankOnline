using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    [SerializeField] GameObject _bullet;
    [SerializeField] int _bulletNum = 100;
    private void Start()
    {
        ObjectPooling.instance.CreatePool(_bullet, _bulletNum);
    }
    public void SpawnBullet(Vector2 pos, Vector2 dir)
    {
        GameObject obj = ObjectPooling.instance.GetObject(_bullet);
        obj.GetComponent<BulletController>().dir = dir.normalized;
        Vector2 direction = dir.normalized;
        var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * (angle-90f));
        obj.transform.position = pos;
        obj.SetActive(true);
    }
}
