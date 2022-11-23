using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamecontroller : MonoBehaviour
{
    public int killcount;
    public int killgoal;
    public Text kills, killgoaltext;
    bool contab = false;
    public GameObject goop, openportaltext, killcard;
    // Start is called before the first frame update
    void Start()
    {
        goop.SetActive(false);
        killgoaltext.text = "/" + killgoal.ToString();
        openportaltext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        kills.text = killcount.ToString();
        if (killcount >= killgoal)
        {
            youwin();
        }
    }
    public void youwin()
    {
        killcard.SetActive(false);
        openportaltext.SetActive(true);
        goop.SetActive(true);
    }
}
