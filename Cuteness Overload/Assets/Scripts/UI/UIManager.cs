using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject deathpanel, defaultpanel, ControlPanel;
    public Image stamholder, healthholder, ammoholder, background ;
    public Sprite stam0, stam1, stam2, stam3, stam4, stam5, stam6, stam7, stam8, stam9, stam10, stam11, stam12, heal1, heal2, heal3, heal4, heal5, heal6, heal7, heal8, snipammosp, shottyammosp, arammosp;
    public PlayerMovement playmove;
    public PlayerDamage playdamn;
    public PlayerShoot playshoot;
    public Text ammoclip, ammomax;
    bool contab;
    private void Start()
    {
        deathpanel.SetActive(false);
        defaultpanel.SetActive(true);
        ControlPanel.SetActive(false);
        contab = false;
    }
    private void Update()
    {
        Controls();
        stamspritechange();
        healthspritechange();
        ammospritechange();
    }

    public void Controls()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (contab)
            {
                ControlPanel.SetActive(false);
                contab = false;
                Time.timeScale = 1;
            }
            else
            {
                ControlPanel.SetActive(true);
                contab = true;
                Time.timeScale = 0;
            }
        }
    }
    private void stamspritechange()
    {
        if (playmove.stamina >= 6000)
        {
            stamholder.sprite = stam12;
        }
        if (playmove.stamina < 6000 && playmove.stamina >= 5500)
        {
            stamholder.sprite = stam11;
        }
        if (playmove.stamina < 5500 && playmove.stamina >= 5000)
        {
            stamholder.sprite = stam10;
        }
        if (playmove.stamina < 5000 && playmove.stamina >= 4500)
        {
            stamholder.sprite = stam9;
        }
        if (playmove.stamina < 4500 && playmove.stamina >= 4000)
        {
            stamholder.sprite = stam8;
        }
        if (playmove.stamina < 4000 && playmove.stamina >= 3500)
        {
            stamholder.sprite = stam7;
        }
        if (playmove.stamina < 3500 && playmove.stamina >= 3000)
        {
            stamholder.sprite = stam6;
        }
        if (playmove.stamina < 3000 && playmove.stamina >= 2500)
        {
            stamholder.sprite = stam5;
        }
        if (playmove.stamina < 2500 && playmove.stamina >= 2000)
        {
            stamholder.sprite = stam4;
        }
        if (playmove.stamina < 2000 && playmove.stamina >= 1500)
        {
            stamholder.sprite = stam3;
        }
        if (playmove.stamina < 1500 && playmove.stamina >= 1000)
        {
            stamholder.sprite = stam2;
        }
        if (playmove.stamina < 1000 && playmove.stamina >= 500)
        {
            stamholder.sprite = stam1;
        }
        if (playmove.stamina < 500)
        {
            stamholder.sprite = stam0;
        }
    }
    private void healthspritechange()
    {
      if (playdamn.playerHealth == 8)
        {
            healthholder.sprite = heal8;
        }
        if (playdamn.playerHealth == 7)
        {
            healthholder.sprite = heal7;
        }
        if (playdamn.playerHealth == 6)
        {
            healthholder.sprite = heal6;
        }
        if (playdamn.playerHealth == 5)
        {
            healthholder.sprite = heal5;
        }
        if (playdamn.playerHealth == 4)
        {
            healthholder.sprite = heal4;
        }
        if (playdamn.playerHealth == 3)
        {
            healthholder.sprite = heal3;
        }
        if (playdamn.playerHealth == 2)
        {
            healthholder.sprite = heal2;
        }
        if (playdamn.playerHealth == 1)
        {
            healthholder.sprite = heal1;
        }
    }
    private void ammospritechange()
    {
        if (playshoot.isSniper)
        {
            ammoholder.sprite = snipammosp;
        }
        if (playshoot.isShotgun)
        {
            ammoholder.sprite = shottyammosp;
        }
        if (playshoot.isAssaultRifle)
        {
            ammoholder.sprite = arammosp;
        }
        if (playshoot.isChainsaw)
        {
            background.enabled = false;
            ammoclip.enabled = false;
            ammomax.enabled = false;
            ammoholder.enabled = false;
        }
        else
        {
            background.enabled = true;
            ammoclip.enabled = true;
            ammomax.enabled = true;
            ammoholder.enabled = true;
        }
    }

    public void deathUI()
    {
        defaultpanel.SetActive(false);
        deathpanel.SetActive(true);
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("MAIN MENU");
    }
    public void playagain()
    {
        SceneManager.LoadScene("Level 1");
    }

}
