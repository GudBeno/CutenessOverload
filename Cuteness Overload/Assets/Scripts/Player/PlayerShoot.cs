using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject FPSCam;
    [SerializeField]
    private Transform projSpawn;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    [SerializeField]
    private float allowedAmmo = 10f;
    [SerializeField]
    private float maxAmmoStored = 100f;

    public float _ammoInClip = 10f;
    public float _storedAmmo = 100f;

    private KeyCode reload = KeyCode.R;

    public float bulletspeed = 10f;


    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        FireBullet();
        Aim();
        Reload();
        Debug.DrawRay(FPSCam.transform.position, FPSCam.transform.forward, Color.magenta);
    }

    public void FireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_ammoInClip > 0)
            {
                GameObject instance = Instantiate(bullet, FPSCam.transform.position, FPSCam.transform.rotation);
                instance.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                instance.transform.forward = FPSCam.transform.forward;
                _ammoInClip--;
            }
        }
    }

    public void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            vcam.m_Lens.FieldOfView = 40;
        }
        if (Input.GetMouseButtonUp(1))
        {
            vcam.m_Lens.FieldOfView = 60;
        }
    }

    public void Reload()
    {
        if (Input.GetKeyDown(reload))
        {
            if (_ammoInClip < allowedAmmo || _storedAmmo > 0)
            {
                if (_storedAmmo < allowedAmmo)
                {
                    float movedAmmo = allowedAmmo - _ammoInClip;
                    if (movedAmmo >= _storedAmmo)
                    {
                        _ammoInClip = _ammoInClip + _storedAmmo;
                        _storedAmmo = 0;
                    }
                    else if (movedAmmo < _storedAmmo)
                    {
                        _ammoInClip = _ammoInClip + movedAmmo;
                        _storedAmmo = _storedAmmo - movedAmmo;
                    }
                }
                else if (_storedAmmo >= allowedAmmo){
                    float ammoReloaded = allowedAmmo - _ammoInClip;
                    _ammoInClip = _ammoInClip + ammoReloaded;
                    _storedAmmo = _storedAmmo - ammoReloaded;
                }
            }
        }
    }

    public void CollectAmmo()
    {
        if(_storedAmmo < maxAmmoStored)
        {
            float ammoCollectable = ((float)Math.Round(UnityEngine.Random.Range(10f, 75f), 0));
            float ammoAllowable = maxAmmoStored - _storedAmmo;
            if(ammoAllowable < ammoCollectable)
            {
                _storedAmmo = _storedAmmo + ammoAllowable;
            }
            else if (ammoAllowable > ammoCollectable)
            {
                _storedAmmo = _storedAmmo + ammoCollectable;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("AmmoCollectable"))
        {
            CollectAmmo();
            other.gameObject.SetActive(false);
        }
    }
}
