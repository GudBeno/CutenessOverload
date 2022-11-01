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
    private GameObject chainsaw;
    [SerializeField]
    private GameObject sniperObject;
    [SerializeField]
    private GameObject shotgunObject;
    [SerializeField]
    private GameObject arObject;
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    [SerializeField]
    private float allowedAmmo = 10f; //This is the size of the magazine clip, the amount of ammo the gun can store at one time
    [SerializeField]
    private float maxAmmoStored = 100f; //This is the size of amount of ammo you can store outside of the clip at one time

    public float _ammoInClip = 10f; //This is the ammo in the clip. Public so it can be used in UI
    public float _storedAmmo = 100f; //This is the ammo stored on the body outside of the clip. Public so it can be used in UI

    [SerializeField]
    private float sgClipSize = 2f;//Shotgun Amount allowed in Clip
    [SerializeField]
    private float sgMaxHeld = 10f;//Shotgun Amount allowed to Store
    [SerializeField]
    private float snClipSize = 1f;//Sniper Amount allowed in Clip
    [SerializeField]
    private float snMaxHeld = 5f;//Sniper Amount allowed to Store
    [SerializeField]
    private float arClipSize = 50f;//AR Amount allowed in Clip
    [SerializeField]
    private float arMaxHeld = 100f;//AR Amount allowed to Store

    public float _sgInClip = 2f;//Shotgun amount currently in Clip
    public float _sgHeld = 10f;//Shotgun amount currently in Store
    public float _snInClip = 1f;//Sniper amount currently in Clip
    public float _snHeld = 5f;//Sniper amount currently in Store
    public float _arInClip = 50f;//AR amount currently in Clip
    public float _arHeld = 100f;//AR amount currently in Store

    private KeyCode reload = KeyCode.R;

    private bool isShotgun = false;
    private bool isSniper = false;
    private bool isAssaultRifle = true;
    private bool isChainsaw = false;

    private Vector3 offset;

    private float arElapsed = 0f;
    private float arDesired = 0.07f;

    private float snReloadElapsed = 0f;
    private float snReloadDesired = 3f;
    private float shReloadElapsed = 0f;
    private float shReloadDesired = 2f;
    private float arReloadElapsed = 0f;
    private float arReloadDesired = 1.5f;

    public float bulletspeed = 10f;

    [SerializeField]
    private Text clipAmmoText;
    [SerializeField]
    private Text storedAmmoText;
    [SerializeField]
    private Text gunTypeText;

    //public enum weapon
    //{
    //    ar = 1,
    //    chainsaw =2,
    //    shotgun = 3,
    //    sniper = 4
    //}
    //weapon currentWeapon;
    private void Start() //Locks the cursor to the centre and hides it
    {
        chainsaw.SetActive(false);
        sniperObject.SetActive(false);
        shotgunObject.SetActive(false);
        arObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        offset = new Vector3(0, 1.54f, 0);
    }

    private void Update() //Runs the functions. Also currently draws a ray where the camera is aiming
    {
        //FireBullet();
        Shotgun();
        Sniper();
        AssaultRifle();
        Chainsaw();
        Aim();
        //Reload();
        ChangeWeapon();
        GunTypeText();
        Debug.DrawRay(FPSCam.transform.position, FPSCam.transform.forward, Color.magenta);
        AmmoTextChange();
    }

    public void AmmoTextChange()
    {
        //clipAmmoText.text = _ammoInClip.ToString();
        //storedAmmoText.text = _storedAmmo.ToString();

        if (isShotgun)
        {
            clipAmmoText.text = _sgInClip.ToString();
            storedAmmoText.text = _sgHeld.ToString();
        }
        if (isSniper)
        {
            clipAmmoText.text = _snInClip.ToString();
            storedAmmoText.text = _snHeld.ToString();
        }
        if (isAssaultRifle)
        {
            clipAmmoText.text = _arInClip.ToString();
            storedAmmoText.text = _arHeld.ToString();
        }
        if (isChainsaw)
        {
            clipAmmoText.text = "";
            storedAmmoText.text = "";
        }
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

    public void CollectARAmmo() //Collect AR Ammo function. Has checks so you dont get negative ammo
    {
        if(_arHeld < arMaxHeld)
        {
            float ammoCollectable = 50f;
            float ammoAllowable = arMaxHeld - _arHeld;
            if(ammoAllowable < ammoCollectable)
            {
                _arHeld = _arHeld + ammoAllowable;
            }
            else if (ammoAllowable > ammoCollectable)
            {
                _arHeld = _arHeld + ammoCollectable;
            }
        }
    }

    public void CollectSGAmmo() //Collect Shotgun Ammo function. Has checks so you dont get negative ammo
    {
        if (_sgHeld < sgMaxHeld)
        {
            float ammoCollectable = 2f;
            float ammoAllowable = sgMaxHeld - _sgHeld;
            if (ammoAllowable < ammoCollectable)
            {
                _sgHeld = _sgHeld + ammoAllowable;
            }
            else if (ammoAllowable > ammoCollectable)
            {
                _sgHeld = _sgHeld + ammoCollectable;
            }
        }
    }

    public void CollectSNAmmo() //Collect Sniper Ammo function. Has checks so you dont get negative ammo
    {
        if (_snHeld < snMaxHeld)
        {
            float ammoCollectable = 1f;
            float ammoAllowable = snMaxHeld - _snHeld;
            if (ammoAllowable < ammoCollectable)
            {
                _snHeld = _snHeld + ammoAllowable;
            }
            else if (ammoAllowable > ammoCollectable)
            {
                _snHeld = _snHeld + ammoCollectable;
            }
        }
    }
    //IEnumerator reloadCoRoutine(float reloadTime,float ammoReference,float maxAmmo)
    //{
    //    float desiredTime = reloadTime;
    //    float elapsedTime = 0;
    //    while (elapsedTime < desiredTime)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    ammoReference = maxAmmo;
    //    yield return null;
    //}

    IEnumerator ShotgunReloadTime()
    {
        yield return new WaitForSeconds(shReloadDesired);
    }

    IEnumerator SniperReloadTime()
    {
        yield return new WaitForSeconds(snReloadDesired);
    }

    IEnumerator ARReloadTime()
    {
        yield return new WaitForSeconds(arReloadDesired);
    }

    public void Shotgun()
    {
        if (isShotgun)
        {
            shotgunObject.SetActive(true);
            sniperObject.SetActive(false);
            arObject.SetActive(false);
            chainsaw.SetActive(false);
            //Shooting
            if (Input.GetMouseButtonDown(0))
            {
                if(_sgInClip > 0)
                {
                    GameObject sgBullet = Instantiate(shotgunbullet, FPSCam.transform.position, FPSCam.transform.rotation);
                    //sgBullet.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                    //sgBullet.transform.position = FPSCam.transform.forward;
                    _sgInClip--;
                }
            }

            //Reloading
            if (Input.GetKeyDown(reload))
            {
                StartCoroutine(ShotgunReloadTime());

                if (_sgInClip < sgClipSize && _sgHeld > 0)
                {
                    if (_sgHeld < sgClipSize)
                    {
                        float reloaded = sgClipSize - _sgInClip;
                        if (reloaded >= _sgHeld)
                        {
                            _sgInClip = _sgInClip + _sgHeld;
                            _sgHeld = 0;
                        }
                        else if (reloaded < _sgHeld)
                        {
                            _sgInClip = _sgInClip + reloaded;
                            _sgHeld = _sgHeld - reloaded;
                        }
                    }
                    else if (_sgHeld >= sgClipSize)
                    {
                        float reloaded = sgClipSize - _sgInClip;
                        _sgInClip = _sgInClip + reloaded;
                        _sgHeld = _sgHeld - reloaded;
                    }
                }
            }
        }
    }

    public void Sniper()
    {
        if (isSniper)
        {
            sniperObject.SetActive(true);
            shotgunObject.SetActive(false);
            arObject.SetActive(false);
            chainsaw.SetActive(false);
            //Shooting
            if (Input.GetMouseButtonDown(0))
            {
                if (_snInClip > 0)
                {
                    GameObject snBullet = Instantiate(sniperbullet, FPSCam.transform.position, FPSCam.transform.rotation);
                    //snBullet.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                    //snBullet.transform.position = FPSCam.transform.forward;
                    _snInClip--;
                }
            }

            //Reloading
            if (Input.GetKeyDown(reload))
            {
                StartCoroutine(SniperReloadTime());

                if (_snInClip < snClipSize && _snHeld > 0)
                {
                    if (_snHeld < snClipSize)
                    {
                        float reloaded = snClipSize - _snInClip;
                        if (reloaded >= _snHeld)
                        {
                            _snInClip = _snInClip + _snHeld;
                            _snHeld = 0;
                        }
                        else if (reloaded < _snHeld)
                        {
                            _snInClip = _snInClip + reloaded;
                            _snHeld = _snHeld - reloaded;
                        }
                    }
                    else if (_snHeld >= snClipSize)
                    {
                        float reloaded = snClipSize - _snInClip;
                        _snInClip = _snInClip + reloaded;
                        _snHeld = _snHeld - reloaded;
                    }
                }
            }
        }
    }

    public void AssaultRifle()
    {
        if (isAssaultRifle)
        {
            arObject.SetActive(true);
            shotgunObject.SetActive(false);
            sniperObject.SetActive(false);
            chainsaw.SetActive(false);
            //Shooting
            if (Input.GetMouseButton(0))
            {
                if (arElapsed <= arDesired) 
                {

                    arElapsed += Time.deltaTime;
                }
                else { 
                    
                    if (_arInClip > 0)
                    {
                        GameObject arBullet = Instantiate(semiautobullet, FPSCam.transform.position, FPSCam.transform.rotation);
                        //arBullet.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                        //arBullet.transform.position = FPSCam.transform.forward;
                        _arInClip--;
                    }
                    arElapsed = 0;
                }
            }

            //Reloading
            if (Input.GetKeyDown(reload))
            {
                StartCoroutine(ARReloadTime());
                if (_arInClip < arClipSize && _arHeld > 0)
                {
                    if (_arHeld < arClipSize)
                    {
                        float reloaded = arClipSize - _arInClip;
                        if (reloaded >= _arHeld)
                        {
                            _arInClip = _arInClip + _arHeld;
                            _arHeld = 0;
                        }
                        else if (reloaded < _arHeld)
                        {
                            _arInClip = _arInClip + reloaded;
                            _arHeld = _arHeld - reloaded;
                        }
                    }
                    else if (_arHeld >= arClipSize)
                    {
                        float reloaded = arClipSize - _arInClip;
                        _arInClip = _arInClip + reloaded;
                        _arHeld = _arHeld - reloaded;
                    }
                }
            }
        }
    }

    public void Chainsaw()
    {
        if (isChainsaw)
        {
            chainsaw.SetActive(true);
            sniperObject.SetActive(false);
            shotgunObject.SetActive(false);
            arObject.SetActive(false);
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
        else if (isShotgun)
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
        else if (isSniper)
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
        else if (isChainsaw)
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
        if(other.gameObject.CompareTag("ARAmmoCollectable"))
        {
            CollectARAmmo();
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("ShotgunAmmoCollectable"))
        {
            CollectSGAmmo();
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("SniperAmmoCollectable"))
        {
            CollectSNAmmo();
            other.gameObject.SetActive(false);
        }
    }
}
