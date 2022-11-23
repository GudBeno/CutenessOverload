using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamecontroller : MonoBehaviour
{
    public int killcount;
    public Text kills;
    public PlayerShoot shooter;
    public PlayerMovement mover;
    bool contab = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        kills.text = killcount.ToString();
        if (killcount >= 30)
        {
            youwin();
        }
    }
    public void youwin()
    {
        SceneManager.LoadScene("win scene"); 
    }
}
