using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TankController : MonoBehaviour
{
    [SerializeField] float _speed = 1f;
    [SerializeField] float _rotateSpeed = 1f;
    [SerializeField] float _rotateTurretSpeed = 1f;
    [SerializeField] GameObject _turret;
    [SerializeField] GameObject _bullet;
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(new Vector3(0, -_speed, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(this.transform.forward, _rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(this.transform.forward, -_rotateSpeed * Time.deltaTime);
        }
        TurretController();
    }
    void TurretController()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _turret.transform.Rotate(this.transform.forward, _rotateTurretSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _turret.transform.Rotate(this.transform.forward, -_rotateTurretSpeed * Time.deltaTime);
        }
    }
}
