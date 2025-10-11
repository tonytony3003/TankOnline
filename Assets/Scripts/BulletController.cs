using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 50f;
    public Vector2 dir;

    private void OnEnable()
    {
        dir = Vector2.up;
    }
    private void Update()
    {
        this.transform.Translate(dir*_bulletSpeed*Time.deltaTime);
    }

}
