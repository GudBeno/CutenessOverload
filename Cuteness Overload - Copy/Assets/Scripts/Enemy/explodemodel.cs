using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodemodel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(explode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator explode()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
