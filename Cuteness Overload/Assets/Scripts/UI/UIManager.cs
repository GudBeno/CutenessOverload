using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ControlPanel;

    private void Update()
    {
        Controls();
    }

    public void Controls()
    {
        if (Input.GetKey(KeyCode.Equals))
        {
            ControlPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (Input.GetKey(KeyCode.Backspace))
        {
            ControlPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
