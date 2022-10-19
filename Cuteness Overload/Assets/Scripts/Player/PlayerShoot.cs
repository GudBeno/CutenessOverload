using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject FPSCam;
    [SerializeField]
    private Transform projSpawn;
    [SerializeField]
    private GameObject semiautobullet;
    [SerializeField]
    private GameObject sniperbullet;
    [SerializeField]
    private GameObject shotgunbullet;
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    [SerializeField]
    private float allowedAmmo = 10f; //This is the size of the magazine clip, the amount of ammo the gun can store at one time
    [SerializeField]
    private float maxAmmoStored = 100f; //This is the size of amount of ammo you can store outside of the clip at one time

    public float _ammoInClip = 10f; //This is the ammo in the clip. Public so it can be used in UI
    public float _storedAmmo = 100f; //This is the ammo stored on the body outside of the clip. Public so it can be used in UI

    private KeyCode reload = KeyCode.R;

    private bool isShotgun = false;
    private bool isSniper = false;
    private bool isAssaultRifle = true;
    private bool isChainsaw = false;

    private Vector3 offset; //Offset random in x and y, stays the same z

    public float bulletspeed = 10f;

    [SerializeField]
    private Text clipAmmoText;
    [SerializeField]
    private Text storedAmmoText;
    [SerializeField]
    private Text gunTypeText;

    private void Start() //Locks the cursor to the centre and hides it
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() //Runs the functions. Also currently draws a ray where the camera is aiming
    {
        FireBullet();
        Aim();
        Reload();
        ChangeWeapon();
        GunTypeText();
        Debug.DrawRay(FPSCam.transform.position, FPSCam.transform.forward, Color.magenta);
        clipAmmoText.text = _ammoInClip.ToString();
        storedAmmoText.text = _storedAmmo.ToString();
    }

    public void FireBullet() //Fires bullet function. 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_ammoInClip > 0)
            {
                GameObject instance = Instantiate(semiautobullet, FPSCam.transform.position, FPSCam.transform.rotation);
                instance.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                instance.transform.forward = FPSCam.transform.forward;
                _ammoInClip--;
            }
        }
    }

    public void Aim() //Aiming in function, done by changing the FOV
    {
        if (Input.GetMouseButton(1))
        {
            vcam.m_Lens.FieldOfView = 45;
        }
        if (Input.GetMouseButtonUp(1))
        {
            vcam.m_Lens.FieldOfView = 90;
        }
    }

    public void Reload() //Reloading. Has checks in place so you dont go into negative ammo
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

    public void CollectAmmo() //Collect Ammo function. Randomises the amount you collect each time. Has checks so you dont get negative ammo
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

    public void Shotgun()
    {
        if (isShotgun)
        {

        }
    }

    public void Sniper()
    {
        if (isSniper)
        {

        }
    }

    public void AssaultRifle()
    {
        if (isAssaultRifle)
        {

        }
    }

    public void Chainsaw()
    {
        if (isChainsaw)
        {

        }
    }

    public void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//AR
        {
            isAssaultRifle = true;
            isChainsaw = false;
            isShotgun = false;
            isSniper = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))//Chainsaw
        {
            isChainsaw = true;
            isAssaultRifle = false;
            isShotgun = false;
            isSniper = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))//Shotgun
        {
            isShotgun = true;
            isChainsaw = false;
            isAssaultRifle = false;
            isSniper = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))//Sniper
        {
            isSniper = true;
            isShotgun = false;
            isChainsaw = false;
            isAssaultRifle = false;
        }
        if (isAssaultRifle)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)//Moves up to Sniper
            {
                isSniper = true;
                isAssaultRifle = false;
                isChainsaw = false;
                isShotgun = false;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)//Moves down to Chainsaw
            {
                isChainsaw = true;
                isAssaultRifle = false;
                isShotgun = false;
                isSniper = false;
            }
        }
        if (isShotgun)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)//Moves up to Chainsaw
            {
                isChainsaw = true;
                isAssaultRifle = false;
                isShotgun = false;
                isSniper = false;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)//Moves down to Sniper
            {
                isSniper = true;
                isAssaultRifle = false;
                isChainsaw = false;
                isShotgun = false;
            }
        }
        if (isSniper)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)//Moves up to Shotgun
            {
                isShotgun = true;
                isAssaultRifle = false;
                isSniper = false;
                isChainsaw = false;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)//Moves down to AR
            {
                isAssaultRifle = true;
                isChainsaw = false;
                isSniper = false;
                isShotgun = false;
            }
        }
        if (isChainsaw)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)//Moves up to AR
            {
                isAssaultRifle = true;
                isChainsaw = false;
                isSniper = false;
                isShotgun = false;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)//Moves down to Shotgun
            {
                isShotgun = true;
                isAssaultRifle = false;
                isSniper = false;
                isChainsaw = false;
            }
        }
    }

    public void GunTypeText()
    {
        if (isAssaultRifle)
        {
            gunTypeText.text = "AR";
        }
        if (isShotgun)
        {
            gunTypeText.text = "Shotgun";
        }
        if (isSniper)
        {
            gunTypeText.text = "Sniper";
        }
        if (isChainsaw)
        {
            gunTypeText.text = "Chainsaw";
        }
    }

    private void OnTriggerEnter(Collider other) //When the player enters the trigger of an Ammo Collectable, runs the CollectAmmo function.
    {
        if(other.gameObject.CompareTag("AmmoCollectable"))
        {
            CollectAmmo();
            other.gameObject.SetActive(false);
        }
    }
}
