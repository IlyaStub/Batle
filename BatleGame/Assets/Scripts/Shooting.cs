using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPref;
    public float bulletForce = 10f; //���� ����
    public float recoil = 0.1f;
    public float recoilDuration = 0.1f;
    private float recoilTime = 0f;

    public float fireRate = 4f; // ���������� ������� ��������� � ������
    private float ReadyForShot; // ��������� ������� �������� �� ����� ������� �������� � ���� ����������
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > ReadyForShot) { // ������� ������� ����� �� ��� ��������
                ReadyForShot = Time.time + 1 / fireRate; // ����� ����� �������� ��������� �������
                Shoot();
                recoilTime = recoilDuration;
            }
            
        }
        if (recoilTime > 0f)
        {
            transform.position -= transform.right * recoil;
            recoilTime -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
