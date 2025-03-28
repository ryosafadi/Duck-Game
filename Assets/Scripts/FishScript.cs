using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishScript : MonoBehaviour
{
    public GameObject Player;
    public TMP_Text fishCounter;

    public AudioSource audioSource;  
    public AudioClip fishCollectSound; // Assign this in the Inspector

    // Start is called before the first frame update
    void Start()
    {
       // Fish = GetComponent<GameObject>();
        if (!Player)
        {
            UnityEngine.Debug.Log("Did not show");
        }
        FishChange();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    void FishChange()
    {
        fishCounter.text = "Fish: " + GameManager.Instance.fishCount;
    }

    void OnTriggerEnter(Collider collision)
    {
        //Hopefully there's a better way to do this but I can't right now
        if (collision.CompareTag("Silver"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.fishCount++;
            IdleDuck.silverFish ++;
            FishChange();

            if (audioSource != null && fishCollectSound != null)
            {
                audioSource.PlayOneShot(fishCollectSound);
            }


            StartCoroutine(Cooldown(10, collision.gameObject));
        }
        else if (collision.CompareTag("Red"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.fishCount++;
            IdleDuck.redFish ++;
            FishChange();

            if (audioSource != null && fishCollectSound != null)
            {
                audioSource.PlayOneShot(fishCollectSound);
            }


            StartCoroutine(Cooldown(10, collision.gameObject));
        }
        else if (collision.CompareTag("Green"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.fishCount++;
            IdleDuck.greenFish ++;
            FishChange();

            if (audioSource != null && fishCollectSound != null)
            {
                audioSource.PlayOneShot(fishCollectSound);
            }


            StartCoroutine(Cooldown(10, collision.gameObject));
        }
        else
        {
            UnityEngine.Debug.Log("Did not show");
        }
    }
    IEnumerator Cooldown(float Time, GameObject Fish)
    {

        yield return new WaitForSeconds(Time);
        Fish.SetActive(true);

    }
}
