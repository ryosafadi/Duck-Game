using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideButton : MonoBehaviour
{
    Button buttonToHide;
    // Start is called before the first frame update
    void Start()
    {
        buttonToHide = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IdleDuck.skillPoints);
        if(IdleDuck.skillPoints <= 0){
            buttonToHide.gameObject.SetActive(false);
        }
        if(IdleDuck.skillPoints > 0){
            buttonToHide.gameObject.SetActive(true);
        }
    }
}
