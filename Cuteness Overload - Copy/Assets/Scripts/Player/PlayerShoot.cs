using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Camera FPSCam;
    public GameObject guncam;
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

    public bool isShotgun = false;
    public bool isSniper = false;
    public bool isAssaultRifle = true;
    public bool isChainsaw = false;

    bool canshoot = true;
    bool iscooping = false;

    private Vector3 offset;

    private float arElapsed = 0f;
    private float arDesired = 0.07f;

    private float snReloadElapsed = 0f;
    private float snReloadDesired = 0.7f;
    private float shReloadElapsed = 0f;
    private float shReloadDesired = 2f;
    private float arReloadElapsed = 0f;
    private float arReloadDesired = 0.9f;

    public float bulletspeed = 10f;

    [SerializeField]
    private Text clipAmmoText;
    [SerializeField]
    private Text storedAmmoText;
    [SerializeField]
    private Text gunTypeText;
    public Image crosshair, scoped;

    public Animator chainsawanim, sniperanim, shotgunanim, aranim;

    public bool death = false;

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
        scoped.enabled = false;
        chainsaw.SetActive(false);
        sniperObject.SetActive(false);
        shotgunObject.SetActive(false);
        arObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        offset = new Vector3(0, 1.54f, 0);
    }

    private void Update() //Runs the functions. Also currently draws a ray where the camera is aiming
    {
        if (death == false)
        {
            Shotgun();
            Sniper();
            AssaultRifle();
            Chainsaw();
            //Reload();
            ChangeWeapon();
            GunTypeText();
            Debug.DrawRay(FPSCam.transform.position, FPSCam.transform.forward, Color.magenta);
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

    IEnumerator ShotgunReloadTime()
    {
        canshoot = false;
        shotgunanim.Play("shotgun_reload");
        yield return new WaitForSeconds(shReloadDesired);
        shotgunanim.Play("shotgun_default");
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
        canshoot = true;

    }

    IEnumerator SniperReloadTime()
    {
        canshoot = false;
        sniperanim.Play("sniper_reload");
        yield return new WaitForSeconds(snReloadDesired);
        sniperanim.Play("sniper_default");
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
        canshoot = true;
    }

    IEnumerator ARReloadTime()
    {
        canshoot = false;
        aranim.Play("AR_reload");
        yield return new WaitForSeconds(arReloadDesired);
        aranim.Play("AR_default");
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
        canshoot = true;
    }

    public void Shotgun()
    {
        if (isShotgun)
        {
            clipAmmoText.text = _sgInClip.ToString();
            storedAmmoText.text = _sgHeld.ToString();
            crosshair.enabled = true;
            shotgunObject.SetActive(true);
            sniperObject.SetActive(false);
            arObject.SetActive(false);
            chainsaw.SetActive(false);
            //Shooting
            if (canshoot)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    if (_sgInClip > 0)
                    {
                        StartCoroutine(shotgunshoot());
                        //sgBullet.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                        //sgBullet.transform.position = FPSCam.transform.forward;
                        //_sgInClip--;
                    }
                }
                else
                {
                    shotgunanim.Play("shotgun_default");
                }
            }
            //Reloading
            if (Input.GetKey(reload))
            {
                StartCoroutine(ShotgunReloadTime());           
            }
        }
    }

    public void Sniper()
    {
        if (isSniper)
        {
            clipAmmoText.text = _snInClip.ToString();
            storedAmmoText.text = _snHeld.ToString();
            crosshair.enabled = false;
            sniperObject.SetActive(true);
            shotgunObject.SetActive(false);
            arObject.SetActive(false);
            chainsaw.SetActive(false);
            //Shooting
            if (canshoot)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_snInClip > 0)
                    {
                        if (iscooping)
                        {
                            StartCoroutine(scopedshot());
                        }
                        else
                        {
                            StartCoroutine(snipershoot());
                        }
                        //GameObject snBullet = Instantiate(sniperbullet, FPSCam.transform.position, FPSCam.transform.rotation);
                        //GameObject snBullet = Instantiate(sniperbullet, FPSCam.transform.position, FPSCam.transform.rotation);
                        //snBullet.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                        //snBullet.transform.position = FPSCam.transform.forward;
                        //_snInClip--;
                    }
                }
                else
                {
                    sniperanim.Play("sniper_default");
                }
            }
            //Reloading
            if (Input.GetKey(reload))
            {
                StartCoroutine(SniperReloadTime());

            }
            if (canshoot)
            {
                if (Input.GetMouseButton(1))
                {
                    if (iscooping == false)
                    {
                        StartCoroutine(scopein());
                    }

                }
                else
                { if (iscooping)
                    {
                        StartCoroutine(scopeout());
                    }
                }
            }
        }
    }

    public void AssaultRifle()
    {
        if (isAssaultRifle)
        {
            clipAmmoText.text = _arInClip.ToString();
            storedAmmoText.text = _arHeld.ToString();
            crosshair.enabled = true;
            arObject.SetActive(true);
            shotgunObject.SetActive(false);
            sniperObject.SetActive(false);
            chainsaw.SetActive(false);
            //Shooting
            if (canshoot)
            {
                if (Input.GetMouseButton(0))
                {

                    if (arElapsed <= arDesired)
                    {

                        arElapsed += Time.deltaTime;
                    }
                    else
                    {

                        if (_arInClip > 0)
                        {
                            aranim.Play("AR_fire");
                            GameObject arBullet = Instantiate(semiautobullet, FPSCam.transform.position, FPSCam.transform.rotation);
                            //arBullet.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                            //arBullet.transform.position = FPSCam.transform.forward;
                            _arInClip--;
                        }
                        arElapsed = 0;
                    }

                }
                else
                {
                    aranim.Play("AR_default");
                }

            }

            //Reloading
            if (Input.GetKey(reload))
            {
                 StartCoroutine(ARReloadTime());
            }
        }
    }

    public void Chainsaw()
    {
        if (isChainsaw)
        {
            clipAmmoText.text = "";
            storedAmmoText.text = "";
            chainsawanim.Play("chainsaw_default");
            crosshair.enabled = false;
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
     IEnumerator snipershoot()
     {
        canshoot = false;
        _snInClip--;
        sniperanim.Play("sniper_fire");
        GameObject snBullet = Instantiate(sniperbullet, FPSCam.transform.position, FPSCam.transform.rotation);
        yield return new WaitForSeconds(0.15f);
        sniperanim.Play("sniper_default");
        canshoot = true;
     }
    IEnumerator scopein()
    {
        iscooping = true;
        canshoot = false;
        sniperanim.Play("sniper_scope_in");
        yield return new WaitForSeconds(0.1f);
        guncam.SetActive(false);
        FPSCam.fieldOfView = 15f;
        scoped.enabled = true;
        canshoot = true;

    }
    IEnumerator scopeout()
    {
        iscooping = false;
        canshoot = false;
        scoped.enabled = false;
        guncam.SetActive(true);
        FPSCam.fieldOfView = 90f;
        sniperanim.Play("sniper_scope_out");
        yield return new WaitForSeconds(0.1f);
        canshoot = true;
    }
    IEnumerator shotgunshoot()
    {
        canshoot = false;
        GameObject sgBullet = Instantiate(shotgunbullet, FPSCam.transform.position, FPSCam.transform.rotation);
        _sgInClip--;
        shotgunanim.Play("shotgun_fire");
        yield return new WaitForSeconds(0.4f);
        shotgunanim.Play("shotgun_default");
        canshoot = true;
    }

    IEnumerator scopedshot()
    {
        canshoot = false;
        GameObject snBullet = Instantiate(sniperbullet, FPSCam.transform.position, FPSCam.transform.rotation);
        _snInClip--;
        yield return new WaitForSeconds(0.15f);
        canshoot = true;
    }

}
