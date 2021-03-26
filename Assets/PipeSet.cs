using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSet : MonoBehaviour
{
    public void ResetPos()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Pipe>().InitialPosition();
            /*
            GameObject Pipe_top, Pipe_bot;
            Pipe_top = child.GetChild(0).gameObject;
            Pipe_bot = child.GetChild(1).gameObject;

            Debug.Log(Pipe_top.transform.position);
            Debug.Log(Pipe_bot.transform.position);
            */
        }
    }
    
    //find the pipe_top
    public Transform GetNextPipe()
    {
        float left = float.MaxValue;
        Transform left_Pipe = null;
        foreach(Transform child in transform)
        {
            //Debug.Log(child.GetChild(0).transform.localPosition.x);
            if (child.GetChild(0).transform.position.x < left && child.GetChild(0).transform.localPosition.x > -0.3f)
            {
                left_Pipe = child.GetChild(0);
                left = child.GetChild(0).transform.position.x;
                //Debug.Log(left);
            }
        }
        //Debug.Log(left_Pipe.name);
        //Debug.Log(left_Pipe.transform.localPosition.x);
        return left_Pipe;
    }

    
    void Start()
    {

    }
    void Update()
    {
        GetNextPipe();
    }
    
}

