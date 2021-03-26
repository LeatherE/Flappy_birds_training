using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class FlappyBird_Agent : Agent
{
    private bool dead = false;
    private bool flap = false;

    private Rigidbody2D rb2d;
    private Vector3 startPos;

    const float height = 2f;
    public float counter = 0f;
    private float upforce = 7.5f;

    public PipeSet pipes;

    private void Update()
    {
        counter += Time.deltaTime;
    }

    private void Push()
    {
        rb2d.velocity = Vector3.zero;
        rb2d.AddForce(new Vector3(0, upforce, 0));
    }

    public override void InitializeAgent()
    {
        startPos = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
    }

    public override void CollectObservations()
    {
        //bird y
        AddVectorObs( gameObject.transform.position.y / height );

        //bird velocity y
        AddVectorObs( Mathf.Clamp(rb2d.velocity.y, -height, height) / height );

        Vector3 Pipe_top_Pos;
        Pipe_top_Pos = pipes.GetNextPipe().localPosition;

        Debug.Log(Pipe_top_Pos.y);

        /*
        //bird Pipe_top_bottom
        AddVectorObs( (Pipe_top_Pos.y - 1.6f) / height );
        //bird Pipe_bot_top
        AddVectorObs( (Pipe_top_Pos.y - 2.8f) / height );
        */

        AddVectorObs((Pipe_top_Pos.y - 2.2f) / height);

        //bird and pipe distance
        //AddVectorObs( Pipe_top_Pos.x / height ) ;

        //flap
        AddVectorObs(flap ? 1f : -1f);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (dead)
        {
            SetReward(-1f);
            Done();
        }
        else
        {
            SetReward(0.01f);
            int tap = Mathf.FloorToInt(vectorAction[0]);
            Debug.Log(tap);
            if(tap == 0)
            {
                flap = false;
            }
            if(tap == 1 && !flap)
            {
                flap = true;
                Push();
            }
        }
    }

    public override void AgentReset()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb2d.velocity = Vector3.zero;
        dead = false;
        counter = 0f;
        pipes.ResetPos();        
    }
    
    private void OnTriggerEnter2D(Collider2D collision2d)
    {
        dead = true;
    }

}
