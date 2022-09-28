using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject FPSCam;
    [SerializeField]
    private Transform projSpawn;
    [SerializeField]
    private GameObject bullet;

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
            GameObject instance = Instantiate(bullet, FPSCam.transform.position, FPSCam.transform.rotation);
            instance.transform.position = FPSCam.transform.position + FPSCam.transform.forward;
            instance.transform.forward = FPSCam.transform.forward;
        }
    }

    public void Aim()
    {

    }

    public void Reload()
    {

    }

    public void MoveProjPoint()
    {
        Vector3 EulerRotation = new Vector3(FPSCam.transform.eulerAngles.x, FPSCam.transform.eulerAngles.y, FPSCam.transform.eulerAngles.z);

        projSpawn.transform.rotation = Quaternion.Euler(EulerRotation);
    }
}
