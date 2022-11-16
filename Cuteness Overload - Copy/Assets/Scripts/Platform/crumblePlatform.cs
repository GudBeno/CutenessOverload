using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crumblePlatform : MonoBehaviour
{
    [SerializeField] bool built;
    //[SerializeField] Animator crumblee;

    void Awake()
    {
        built = true;
    }
    // Start is called before the first frame update
    void Start()
    {
       
       
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.tag=="Player")&&(built))
        {
           
            
            StartCoroutine(Crumble());
           // crumblee.SetTrigger("crumble");

        }
       
    }

    IEnumerator Crumble()
    {
        //builds back the crumbled up block
       
        float crumbleTime = 3.0f;
        
        yield return new WaitForSeconds(crumbleTime);
        this.gameObject.GetComponent<MeshRenderer>().enabled =false;
        this.gameObject.GetComponent<MeshCollider>().enabled =false;
        print("crumbling");
        built = false;
        StartCoroutine(Rebuild());

        

    }

    IEnumerator Rebuild()
    {
        //builds back the crumbled up block

        float rebuildTime = 3.0f;

        yield return new WaitForSeconds(rebuildTime);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<MeshCollider>().enabled = true;
        print("rebuilding");
        built = true;

    }
    
}
