using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InputSaw : MonoBehaviour
{
    public float deltaPresed = 0.04f;
    private float timer;
    private Saw saw;
    private bool mouseIsPress;
    private float touchPosY;
    private void Update()
    {
        StartCoroutine(Timer());
        if (mouseIsPress)
        {
            if (Input.GetMouseButtonUp(0) && timer < deltaPresed)
            {
                if (Input.mousePosition.x > SRes.width / 2) saw.ChangeAxis();
                if (Input.mousePosition.x < SRes.width / 2) saw.UseSkill();
            }
            if (timer >= deltaPresed)
            {
                if (Input.mousePosition.x > SRes.width / 2) saw.ChangeLenght(touchPosY);
                if (Input.mousePosition.x < SRes.width / 2) saw.ChangeSkill(ref touchPosY);
            }
            if (Input.GetMouseButtonUp(0)) mouseIsPress = false;
        }
        if (WorldData.in_battle && Input.GetMouseButtonDown(0))
        {
            timer = 0;
            mouseIsPress = true;
            touchPosY = Input.mousePosition.y;
        }
    }
    private void Start()
    {
        saw = GetComponent<Saw>();
    }
    IEnumerator Timer()
    {
        if (timer <= deltaPresed)
        {
            yield return new WaitForSecondsRealtime(0.001f);
            timer += 0.001f;
        }
    }
}
