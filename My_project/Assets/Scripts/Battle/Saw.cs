using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public Transform saw0;
    public Transform saw1;

    private float lenght_chain;
    private float speed_rotation;
    private float dir_rotation = -1;

    private void Update()
    {
        if (WorldData.in_battle && Input.GetMouseButtonDown(0)) ChangeAxis();
        saw1.localPosition = new Vector2(0, lenght_chain);
        saw0.Rotate(0, 0, speed_rotation * dir_rotation);
    }
    private void ChangeAxis()
    {
        saw0.position = saw1.position;
        saw0.Rotate(0, 0, 180);
        dir_rotation *= -1;
    }
    private void Start()
    {
        lenght_chain = (PartsSaw.currentChain.maxLenght + PartsSaw.currentChain.minLenght) / 2;
        speed_rotation = PartsSaw.currentDisc.speedRotation;
    }
}
