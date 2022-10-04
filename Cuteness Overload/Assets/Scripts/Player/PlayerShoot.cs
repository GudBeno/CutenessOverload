using System.Collections;
using System.Collections.Generic;
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
    private float _ammo = 10f;
    [SerializeField]
    private float _storedAmmo = 100f;
    [SerializeField]
    private float _ammoAmount = 10f;
    [SerializeField]
    private float maxAmmoStored = 100f;

    private KeyCode reload = KeyCode.R;

    public float bulletspeed = 10f;

    private void Start()
    {
        
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
            if (_ammo > 0)
            {
                GameObject instance = Instantiate(bullet, FPSCam.transform.position, FPSCam.transform.rotation);
                instance.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                instance.transform.forward = FPSCam.transform.forward;
                _ammo--;
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
            if (_ammo < _ammoAmount)
            {
                float ammoTransferred = _ammoAmount - _ammo;
                _ammo = _ammo + ammoTransferred;
                _storedAmmo = _storedAmmo - ammoTransferred;
                //ammoTransferred = 0;
            }
        }
    }

    public void CollectAmmo()
    {
        if(_storedAmmo < maxAmmoStored)
        {
            float ammoTransferred = maxAmmoStored - _storedAmmo;
            _storedAmmo = _storedAmmo + ammoTransferred;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectAmmo();
    }
}
