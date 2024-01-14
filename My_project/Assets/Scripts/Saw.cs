using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public Transform saw0;
    public Transform saw1;

    private float lenght_chain = 4;
    private float speed_rotation = 1;
    private float dir_rotation = 1;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) ChangeAxis();
        saw1.localPosition = new Vector2(0, lenght_chain);
        saw0.Rotate(0, 0, speed_rotation * dir_rotation);
    }
    private void ChangeAxis()
    {
        saw0.position = saw1.position;
        saw0.Rotate(0, 0, 180);
        dir_rotation *= -1;
    }
}
