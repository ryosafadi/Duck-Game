using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public GameObject Fish;
    // Start is called before the first frame update
    void Start()
    {
       // Fish = GetComponent<GameObject>();
        if (!Fish)
        {
            UnityEngine.Debug.Log("Did not show");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Fish)
            {
                Fish.SetActive(false);

            }
            
        }
        else
        {
            UnityEngine.Debug.Log("Did not show");
        }
    }
}
