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
        MoveProjPoint();
    }

    public void FireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject instance = Instantiate(bullet, projSpawn.transform.position, projSpawn.transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(FPSCam.transform.forward * bulletspeed);
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
