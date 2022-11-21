using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu_controller : MonoBehaviour
{
    bool wave;
    public Animator mainmenuanim, playanim, tutanim, exitanim;
    public float counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 10)
        {
            if (wave == false)
            {
                wave = true;
                StartCoroutine(waveplay());
            }
        }
    }
    IEnumerator waveplay()
    {
        mainmenuanim.Play("wave anim");
        yield return new WaitForSeconds(1f);
        mainmenuanim.Play("DEFAULT");
        counter = 0;
        wave = false;
    }
    public void play()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void PLAYONHOVER()
    {
        playanim.Play("PLAY ONHOVER");
    }
    public void PLAYDEFAULT()
    {
        playanim.Play("PLAY DEFAULT");
    }
    public void TUTONHOVER()
    {
        tutanim.Play("TUTORIAL ONHOVER");
    }
    public void TUTDEFAULT()
    {
        tutanim.Play("TUTORIAL DEFAULT");
    }
    public void EXITONHOVER()
    {
        exitanim.Play("EXIT ONHOVER");
    }
    public void EXITDEFAULT()
    {
        exitanim.Play("EXIT DEFAULT");
    }
}
