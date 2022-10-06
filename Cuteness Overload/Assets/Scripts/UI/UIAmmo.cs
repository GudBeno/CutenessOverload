using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAmmo : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro ammoStored;
    [SerializeField]
    private TextMeshPro ammoClip;

    private PlayerShoot playShoot;

    private void Update()
    {
        ammoStored.text = (playShoot._ammoInClip).ToString();
        ammoClip.text = (playShoot._storedAmmo).ToString();
    }
}
