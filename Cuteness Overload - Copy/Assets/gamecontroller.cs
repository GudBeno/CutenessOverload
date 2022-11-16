using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamecontroller : MonoBehaviour
{
    public int killcount;
    public Text kills;
    public GameObject defaultscreen, winscrene, chainsaw, shotgun, sniper, ar, ragdoll, wincam, guncam, maincam, ControlPanel;
    public PlayerShoot shooter;
    public PlayerMovement mover;
    bool contab = false;
    // Start is called before the first frame update
    void Start()
    {
        wincam.SetActive(false);
        winscrene.SetActive(false);
        ControlPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        kills.text = killcount.ToString();
        if (killcount >= 50)
        {
            youwin();
        }
        if (Input.GetKey("I"))
        {
            if (contab)
            {
                ControlPanel.SetActive(false);
                defaultscreen.SetActive(true);
                Time.timeScale = 1;
            }
            else
            {
                ControlPanel.SetActive(true);
                defaultscreen.SetActive(false);
                Time.timeScale = 0;
            }
        }
    }
    public void youwin()
    {
        shooter.death = true;
        mover.death = true;
        defaultscreen.SetActive(false);
        winscrene.SetActive(true);

        ragdoll.SetActive(true);
        wincam.SetActive(true);

        chainsaw.SetActive(false);
        sniper.SetActive(false);
        shotgun.SetActive(false);
        ar.SetActive(false);
        maincam.SetActive(false);
        guncam.SetActive(false);
        Time.timeScale = 0;
    }
}
