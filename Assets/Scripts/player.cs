using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody2D playerRb;
    
    public float speedMove;
    public float speedTurn;

    
    private void Update()
    {
        //transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0, 0, agle * n), Time.deltaTime * speedTurn ); 
    }
    int n = 0;
    
    public void assignTurnFace(playerController.playerDirection turnFace)
    {
        //Debug.Log("Turn Listener!");
        
        if (turnFace == playerController.playerDirection.left)
        {
            n = 2;
        }
        
        if (turnFace == playerController.playerDirection.up)
        {
            n = 1;
        }

        if (turnFace == playerController.playerDirection.right)
        {
            n = 0;
        }

        if (turnFace == playerController.playerDirection.down)
        {
            n = 3;
        }

        StartCoroutine(Turn());
    }

    IEnumerator Turn()
    {
        // the for is a timer, which controls the player to turn slowly
        //coroutine and return null ensure that this function will be printed each frame
        //instead of only printing out the outcomes
        //Debug.Log("Turn listener!");
        int agle = 90;

        for (float i = 0; i <= 1; i += speedTurn)
        {
            //Debug.Log(i.ToString());
            //transform.rotation = Quaternion.Euler(Vector3.forward * (agle * n * (i  / speedTurn)));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.forward * (agle * n)), i);
            GameManager.instance.transform.Find("PlayerControllerHolder").GetComponent<playerController>()
                .isExecutingTurn = false;
            yield return null;
        }

        
        //this data skip the layer of assignTurnFace(), moveOrTurn()
        //directly responses to <Gamemanager>().Clean();
        //which is to control it not to assign next task to the player
    }
    
    
    private Vector3 moveToPos;
    private Vector3 moveDis;
    public IEnumerator Move( float moveLength)
    {
        //Debug.Log("Move Listener!");
        
        // moveDis = transform.position + transform.rotation.normalized * Vector3.forward * moveLength;
        // moveToPos = Vector3.Lerp(transform.position, moveDis, Time.deltaTime*speedMove);
        // playerRb.MovePosition(moveToPos);
        // //playerRb.MovePosition(moveDis);

        Vector3 playerPos = transform.position;
        // moveDis = transform.forward * moveLength;
        // Debug.Log("transform.forward.x: "+transform.forward.x.ToString());
        // Debug.Log("transform.forwand.y: " + transform.forward.y.ToString());
        moveDis = transform.rotation * new Vector3(moveLength, 0, 0);
        moveToPos = playerPos + moveDis;
        playerRb.MovePosition(moveToPos);
        Debug.Log("moveDis.x: "+moveDis.x);
        GameManager.instance.transform.Find("PlayerControllerHolder").GetComponent<playerController>()
            .isExecutingMove = false;
        yield return null;

        // for (float i = 0; i <= 1; i += speedMove)
        // {
        //     moveToPos = Vector3.Lerp(playerPos, playerPos + moveDis, i);
        //     Debug.Log(moveToPos.x.ToString());
        //     playerRb.MovePosition(moveToPos);
        //     yield return null;
        // }
    }
}
