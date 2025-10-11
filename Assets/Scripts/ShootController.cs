using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] GameObject _shootPoint;
    [SerializeField] float _timebtw = 7.5f;
    [SerializeField] float _timecount = 0;
    private void Update()
    {
        _timecount += Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) && _timecount >= _timebtw)
        {
            Vector2 LocalDirection = Vector2.up;
            Vector2 worldDirection = transform.TransformDirection(LocalDirection);
            SpawnerManager.instance.SpawnBullet(_shootPoint.transform.position, worldDirection);
            _timecount = 0;
        }
    }
}
