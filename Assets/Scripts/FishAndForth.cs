using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class FishAndForth : MonoBehaviour
{
    public GameObject Fish;
    private Vector3 stopA;
    private Vector3 stopB;

    void Start()
    {
        Vector3 playerMove = Fish.transform.position;
        stopA = playerMove;

        int move = UnityEngine.Random.Range(4, 15);
        stopB = new Vector3(move, 0, 0) + playerMove;
    }

    void Update()
    {
         // Inspired from: https://stackoverflow.com/questions/43009515/move-gameobject-back-and-forth
        float time = Mathf.PingPong(Time.time * IdleDuck.actualSpeed, 1);
        Vector3 newPosition = Vector3.Lerp(stopA, stopB, time);

        Vector3 movementDirection = newPosition - transform.position;

        transform.position = newPosition;

        if (movementDirection.x > 0) // Moving right
            transform.rotation = Quaternion.Euler(0, 180, 0); // Flip left
        else if (movementDirection.x < 0) // Moving left
            transform.rotation = Quaternion.Euler(0, 0, 0); // Flip right
    }
}
