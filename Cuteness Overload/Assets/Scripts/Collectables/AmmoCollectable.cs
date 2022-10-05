using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : MonoBehaviour
{
    public float ammoGiven;

    public void Randomize()
    {
        ammoGiven = Random.Range(10f, 75f);
    }

    private void OnEnable()
    {
        Randomize();
    }

}
