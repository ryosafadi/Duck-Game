using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class FishAndForth : MonoBehaviour
{
    public GameObject Fish;
    private Vector3 playerMove;
    private Vector3 stopA;
    private Vector3 stopB;
    private float speed = 1.19f;
    // Start is called before the first frame update
    void Start()
    {
       playerMove = Fish.transform.position;
       stopA = new Vector3(0, 0, 0);
        int move = UnityEngine.Random.Range(4, 10);
       stopB = new Vector3(move, 0, 0);
        stopB += playerMove;
        stopA += playerMove;
    }

    // Update is called once per frame
    void Update()
    {
        // Inspired from: https://stackoverflow.com/questions/43009515/move-gameobject-back-and-forth
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(stopA, stopB, time);
    }
}
