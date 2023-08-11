using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlock : MonoBehaviour
{
    public GameObject Grabplayer;
    private bool isGrab = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (isGrab)
        {
            transform.position = Grabplayer.transform.position + new Vector3(0, 1f, 0);
        }
        else { 
        
        }
    }
    public void GrabBlock(GameObject Player) {
        Grabplayer = Player;
        isGrab = true;
    }
    public void NonGrab(bool isRight) { 
        isGrab = false;
        if (isRight)
        {
            transform.position = Grabplayer.transform.position + new Vector3(1f, 0.2f, 0f);
        }
        else {
            transform.position = Grabplayer.transform.position + new Vector3(-1f, 0.2f, 0f);
        }

    }
}
