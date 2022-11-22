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
    [SerializeField]
    private Transform projSpawn;
    [SerializeField]
    private GameObject semiautobullet;
    [SerializeField]
    private GameObject sniperbullet;
    [SerializeField]
    private GameObject shotgunbullet;
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
    public Image crosshair;

    public Animator gunsanim;

    public bool death = false;
    public GameObject chainsawslash;

    public bool cankill, ardeal;

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
        cankill = false;
         chainsawslash.SetActive(false);
//        sniperObject.SetActive(false);
//        shotgunObject.SetActive(false);
//        arObject.SetActive(true);
//
        Cursor.lockState = CursorLockMode.Locked;
        offset = new Vector3(0, 1.54f, 0);
    }

    private void Update() //Runs the functions. Also currently draws a ray where the camera is aiming
    {
        if (death == false)
        {
            ChangeWeapon();
            Shotgun();
            Sniper();
            AssaultRifle();
            Chainsaw();
            //Reload();
            //GunTypeText();
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
        gunsanim.Play("shotgun_reload");
        yield return new WaitForSeconds(shReloadDesired);
        gunsanim.Play("shotgun_default");
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
        gunsanim.Play("sniper_reload");
        yield return new WaitForSeconds(snReloadDesired);
        gunsanim.Play("sniper_default");
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
        gunsanim.Play("AR_reload");
        yield return new WaitForSeconds(arReloadDesired);
        gunsanim.Play("AR_default");
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
//            shotgunObject.SetActive(true);
//          sniperObject.SetActive(false);
//          arObject.SetActive(false);
//          chainsaw.SetActive(false);
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
                    gunsanim.Play("shotgun_default");
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
//            sniperObject.SetActive(true);
//            shotgunObject.SetActive(false);
//            arObject.SetActive(false);
//            chainsaw.SetActive(false);
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
            }
            //Reloading
            if (Input.GetKey(reload))
            {
                if (iscooping == false)
                {
                    StartCoroutine(SniperReloadTime());
                }
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
        ardeal = false;
        if (isAssaultRifle)
        {
            RaycastHit arhit;
            clipAmmoText.text = _arInClip.ToString();
            storedAmmoText.text = _arHeld.ToString();
            crosshair.enabled = true;
            //            arObject.SetActive(true);
            //            shotgunObject.SetActive(false);
            //            sniperObject.SetActive(false);
            //            chainsaw.SetActive(false);
            //Shooting
            if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out arhit, 10f))
            {
                if (arhit.transform.CompareTag("Bearnemy"))
                {
                    ardeal = true;
                }
                else if (arhit.transform.CompareTag("BeeEnemy"))
                {
                    ardeal = true;
                }
                else
                {
                    ardeal = false;
                }
            }
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
                            if (ardeal == true)
                            {
                                EnemyManager enemy = arhit.transform.GetComponent<EnemyManager>();
                                enemy.ardamager();

                            }
                            gunsanim.Play("AR_fire");
                            GameObject arBullet = Instantiate(semiautobullet, projSpawn.transform.position, projSpawn.transform.rotation);
                            //arBullet.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
                            //arBullet.transform.position = FPSCam.transform.forward;
                            _arInClip--;
                        }
                        arElapsed = 0;
                    }

                }
                else
                {
                    gunsanim.Play("AR_default");
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
        cankill = false;
        if (isChainsaw)
        {
            RaycastHit chainhit;
            clipAmmoText.text = "";
            storedAmmoText.text = "";
            crosshair.enabled = false;
            if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out chainhit, 4f))
                {
                if (chainhit.transform.CompareTag("Bearnemy"))
                {
                    cankill = true;
                }
                else
                {
                    cankill = false;
                }
            }
            if (canshoot)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (cankill)
                    {
                        EnemyManager enemy = chainhit.transform.GetComponent<EnemyManager>();
                        enemy.destroy();
                        StartCoroutine(chainsawkill());
                    }
                    else
                    {
                        StartCoroutine(chainsawattack());
                    }
                }
                else
                {
                    gunsanim.Play("chainsaw_default");
                }
            }
            //            chainsaw.SetActive(true);
            //            sniperObject.SetActive(false);
            //            shotgunObject.SetActive(false);
            //            arObject.SetActive(false);
        }
    }

    public void ChangeWeapon()
    {
        if (iscooping == false)
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))//AR
            {
                gunsanim.Play("AR_up");
                isAssaultRifle = true;
                isChainsaw = false;
                isShotgun = false;
                isSniper = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))//Chainsaw
            {
                gunsanim.Play("chainsaw_up");
                isChainsaw = true;
                isAssaultRifle = false;
                isShotgun = false;
                isSniper = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))//Shotgun
            {
                gunsanim.Play("shotgun_up");
                isShotgun = true;
                isChainsaw = false;
                isAssaultRifle = false;
                isSniper = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))//Sniper
            {
                gunsanim.Play("sniper_up");
                isSniper = true;
                isShotgun = false;
                isChainsaw = false;
                isAssaultRifle = false;
            }
            if (isAssaultRifle)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)//Moves up to Sniper
                {
                    gunsanim.Play("sniper_up");
                    isSniper = true;
                    isAssaultRifle = false;
                    isChainsaw = false;
                    isShotgun = false;
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)//Moves down to Chainsaw
                {
                    gunsanim.Play("chainsaw_up");
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
                    gunsanim.Play("chainsaw_up");
                    isChainsaw = true;
                    isAssaultRifle = false;
                    isShotgun = false;
                    isSniper = false;
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)//Moves down to Sniper
                {
                    gunsanim.Play("sniper_up");
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
                    gunsanim.Play("shotgun_up");
                    isShotgun = true;
                    isAssaultRifle = false;
                    isSniper = false;
                    isChainsaw = false;
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)//Moves down to AR
                {
                    gunsanim.Play("AR_up");
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
                    gunsanim.Play("AR_up");
                    isAssaultRifle = true;
                    isChainsaw = false;
                    isSniper = false;
                    isShotgun = false;
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)//Moves down to Shotgun
                {
                    gunsanim.Play("shotgun_up");
                    isShotgun = true;
                    isAssaultRifle = false;
                    isSniper = false;
                    isChainsaw = false;
                }
            }
        }
    }

 //   public void GunTypeText()
 //   {
 //      if (isAssaultRifle)
 //       {
 //           gunTypeText.text = "AR";
//        }
//        if (isShotgun)
//        {
//            gunTypeText.text = "Shotgun";
//        }
//        if (isSniper)
//        {
//            gunTypeText.text = "Sniper";
//       }
 //       if (isChainsaw)
 //       {
   //         gunTypeText.text = "Chainsaw";
  //      }
  //  }

     IEnumerator snipershoot()
     {
        canshoot = false;
        _snInClip--;
        gunsanim.Play("sniper_fire");
        GameObject snBullet = Instantiate(sniperbullet, projSpawn.transform.position, projSpawn.transform.rotation);
        yield return new WaitForSeconds(0.15f);
        gunsanim.Play("sniper_default");
        canshoot = true;
     }
    IEnumerator scopein()
    {
        iscooping = true;
        canshoot = false;
        gunsanim.Play("sniper_scope_in");
        yield return new WaitForSeconds(0.1f);
        gunsanim.Play("scopedin");
        FPSCam.fieldOfView = 15f;
        canshoot = true;

    }
    IEnumerator scopeout()
    {
        iscooping = false;
        canshoot = false;
        FPSCam.fieldOfView = 90f;
        gunsanim.Play("sniper_scope_out");
        yield return new WaitForSeconds(0.1f);
        gunsanim.Play("sniper_default");
        canshoot = true;
    }
    IEnumerator shotgunshoot()
    {
        canshoot = false;
        GameObject sgBullet = Instantiate(shotgunbullet, projSpawn.transform.position, projSpawn.transform.rotation);
        _sgInClip--;
        gunsanim.Play("shotgun_fire");
        yield return new WaitForSeconds(0.4f);
        gunsanim.Play("shotgun_default");
        canshoot = true;
    }

    IEnumerator scopedshot()
    {
        canshoot = false;
        GameObject snBullet = Instantiate(sniperbullet, projSpawn.transform.position, projSpawn.transform.rotation);
        _snInClip--;
        yield return new WaitForSeconds(0.15f);
        canshoot = true;
    }
    IEnumerator chainsawattack()
    {
        canshoot = false;
        gunsanim.Play("chainsaw_attack");
        yield return new WaitForSeconds(0.3f);
        chainsawslash.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        chainsawslash.SetActive(false);
        gunsanim.Play("chainsaw_default");
        canshoot = true;
    }
    IEnumerator chainsawkill()
    {
        canshoot = false;
        gunsanim.Play("chainsaw_kill");
        yield return new WaitForSeconds(0.7f);
        gunsanim.Play("chainsaw_default");
        canshoot = true;
    }

}
