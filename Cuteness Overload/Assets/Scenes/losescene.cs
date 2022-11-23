using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class losescene : MonoBehaviour
{
    public void playagain()
    {
        if (allscenesmanager.level == 1)
        {
            SceneManager.LoadScene("Level 1");
        }
        if (allscenesmanager.level == 2)
        {
            SceneManager.LoadScene("Level 2");
        }
        if (allscenesmanager.level == 3)
        {
            SceneManager.LoadScene("Level 3");
        }

    }
    public void exit()
    {
        Application.Quit();
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("MAIN MENU");
    }
}
