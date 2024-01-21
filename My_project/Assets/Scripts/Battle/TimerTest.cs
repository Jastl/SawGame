using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    public float time;
    private bool isWork;
    private float deltaTime = 0.05f;
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.F)) isWork = !isWork;
        /*if (isWork) */StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(deltaTime);
            time += deltaTime;
        }
    }
}
