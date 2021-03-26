using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    const float space = 2f;
    const int totalPipes = 3;
    private Vector3 startPos_top;
    private Vector3 startPos_bot;
    public float Varience = 1.1f;

    GameObject pipe_top;
    GameObject pipe_bot;

    private void Awake()
    {
        pipe_top = gameObject.transform.GetChild(0).gameObject;
        pipe_bot = gameObject.transform.GetChild(1).gameObject;
        //Debug.Log(pipe_top.transform.localPosition);
        //Debug.Log(pipe_bot.transform.localPosition);
        startPos_top = pipe_top.transform.localPosition;
        startPos_bot = pipe_bot.transform.localPosition;

        RandomY();
    }

    private void LateUpdate()
    {
        pipe_top.transform.Translate(Vector3.right * Time.deltaTime);
        pipe_bot.transform.Translate(Vector3.left * Time.deltaTime);

        if(pipe_top.transform.localPosition.x < -space)
        {
            pipe_top.transform.Translate(Vector3.left * space * totalPipes);
            pipe_bot.transform.Translate(Vector3.right * space * totalPipes);

            var tmp = pipe_top.transform.localPosition;
            tmp.y = 2f;
            pipe_top.transform.localPosition = tmp;
            tmp.y = -2.4f;
            pipe_bot.transform.localPosition = tmp;
            RandomY();
        }
    }

    public void InitialPosition()
    {
        pipe_top.transform.localPosition = startPos_top;
        pipe_bot.transform.localPosition = startPos_bot;
        RandomY();
    }

    private void RandomY()
    {
        float r;
        r = Random.Range(-Varience, Varience);
        Vector3 offset;
        offset = new Vector3(0, r, 0);
        pipe_top.transform.position = pipe_top.transform.position + offset;

        pipe_bot.transform.position = pipe_bot.transform.position + offset;
    }
}
