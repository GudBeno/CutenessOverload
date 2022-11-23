using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winscene : MonoBehaviour
{
  public void playagain()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void exit()
    {
        Application.Quit();
    }
}
