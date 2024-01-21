using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public Transform saw0;
    public Transform saw1;

    public float lenght_chain;
    public float speed_rotation;
    public float damage;
    private float dir_rotation = -1;

    private float chainBeefore;
    public SkillBar skillBar;

    private void Update()
    {
        saw1.localPosition = new Vector2(0, lenght_chain);
        saw0.Rotate(0, 0, speed_rotation * dir_rotation);
    }
    public void ChangeAxis()
    {
        saw0.position = saw1.position;
        saw0.Rotate(0, 0, 180);
        dir_rotation *= -1;
    }
    public void ChangeLenght(float posTouchY)
    {
        lenght_chain = chainBeefore + (Input.mousePosition.y - posTouchY) / 200;
        lenght_chain =  Clamp(lenght_chain, PartsSaw.currentChain.minLenght, PartsSaw.currentChain.maxLenght);
        if (Input.GetMouseButtonUp(0)) chainBeefore = lenght_chain;
    }
    public void UseSkill()
    {
        skillBar.UseSkill();
    }
    public void ChangeSkill(ref float yPosTap)
    {
        if (Input.mousePosition.y - yPosTap > SRes.width / 5)
        {
            skillBar.Scroll(DirectScroll.Up);
            yPosTap += SRes.width / 5;
        }
        if (Input.mousePosition.y - yPosTap < SRes.width / -5)
        {
            skillBar.Scroll(DirectScroll.Down);
            yPosTap -= SRes.width / 5;
        }
    }
    public float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
    private void Start()
    {
        lenght_chain = (PartsSaw.currentChain.maxLenght + PartsSaw.currentChain.minLenght) / 2;
        speed_rotation = PartsSaw.currentDisc.speedRotation;
        chainBeefore = lenght_chain;
    }
}
