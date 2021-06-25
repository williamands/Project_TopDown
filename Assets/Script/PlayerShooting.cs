using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float shootTimer = 0f;

    [SerializeField] private int maxAmmo = 8;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float reloadTime = 1.5f;
    private bool isReloading = false;

    public TMP_Text ammoUI;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    private void Update()
    {
        Aim();
        Shoot();
        Ammo();
        PressReload();
    }

    private void Aim()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && shootTimer <= 0 && currentAmmo > 0)
        {
            currentAmmo--;
            GameObject bullet = Instantiate(prefabBullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rigidBody2D = bullet.GetComponent<Rigidbody2D>();
            rigidBody2D.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            shootTimer = 0.35f;
        }

        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        ammoUI.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }

    private void Ammo()
    {
        if (isReloading)
        {
            return;
        }
        
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    private void PressReload()
    {
        if (Input.GetKeyDown("r"))
        {
            StartCoroutine(Reload());
        }
    }

    private static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    private static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
