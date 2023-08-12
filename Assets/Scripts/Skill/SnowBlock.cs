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
            if (Grabplayer == null) { Destroy(gameObject); return; }
            transform.position = Grabplayer.transform.position + new Vector3(0, 1.1f, 0);
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
            transform.position = Grabplayer.transform.position + new Vector3(1.2f, 0f, 0f);
        }
        else {
            transform.position = Grabplayer.transform.position + new Vector3(-1.2f, 0f, 0f);
        }

    }
}
