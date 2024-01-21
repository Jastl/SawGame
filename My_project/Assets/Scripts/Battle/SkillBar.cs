using UnityEngine;
using UnityEngine.UI;
using Skills;
using System.Collections;
using System;

public class SkillBar : MonoBehaviour
{
    public RectTransform barPanel;
    public SkillIcon[] icons = new SkillIcon[5];
    public int timeScroll = 80;

    public int selectedSkill;
    private int SelectedSkill {
        get { return selectedSkill; }
        set
        {
            if (value < 0) selectedSkill = value % skills.Length;
            else selectedSkill = value % skills.Length;
        }
    }
    private Skill[] skills = new Skill[3];
    public void UseSkill()
    {
        skills[selectedSkill].Activate();
    }
    public void Scroll(DirectScroll direct)
    {
        int dir;
        if (direct == DirectScroll.Down) dir = -1;
        else dir = 1;
        
        StartCoroutine(ChangeAlpha(icons[2].iconBack.GetComponent<Image>(), 0.5f, timeScroll));
        StartCoroutine(ChangeAlpha(icons[2 + dir].iconBack.GetComponent<Image>(), 1f, timeScroll));
        StartCoroutine(ChangeAlpha(icons[2 + dir * 2].iconBack.GetComponent<Image>(), 0.5f, timeScroll));
        StartCoroutine(ChangeAlpha(icons[2 - dir].iconBack.GetComponent<Image>(), 0f, timeScroll));
        StartCoroutine(MoveTo(barPanel, new Vector2(barPanel.localPosition.x, barPanel.localPosition.y + icons[0].iconBack.sizeDelta.x * 1.1f * dir), timeScroll));

        SelectedSkill -= dir;
        SetSkillOnIcons(SelectedSkill);
    }
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Saw").GetComponent<Saw>().skillBar = this.GetComponent<SkillBar>();
        //get all skills
        skills[0] = PartsSaw.currentDisc.GetSkill();
        skills[1] = PartsSaw.currentTeeth.GetSkill();
        skills[2] = PartsSaw.currentChain.GetSkill();
        //remove all skills without icons
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].GetIcon() == null)
            {
                if (i == skills.Length - 1) Array.Resize(ref skills, skills.Length - 1);
                else
                {
                    for (int j = i; j < skills.Length - 1; j++) skills[j] = skills[j + 1];
                    Array.Resize(ref skills, skills.Length - 1);
                    i--;
                }
            }
        }
        SetSkillOnIcons(SelectedSkill);
    }
    private void Update()
    {
        foreach(SkillIcon icon in icons)
        {
            icon.iconBack.GetComponent<Image>().fillAmount = icon.skill.GetFillAmount();
        }
    }
    private IEnumerator MoveTo(RectTransform go, Vector3 target, int time)
    {
        Vector3 deltaDir = (target - go.localPosition) / (float)time;
        int counter = 0;
        while (counter < time)
        {
            go.localPosition += deltaDir;
            counter++;
            yield return null;
        }
        go.localPosition -= deltaDir * time;
    }
    private IEnumerator ChangeAlpha(Image img, float alpha, int time)
    {
        float delta = (alpha - img.color.a) / (float)time;
        int counter = 0;
        while (counter < time)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + delta);
            counter++;
            yield return null;
        }
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - delta * time);
    }
    private void SetSkillOnIcons(int centralSkill)
    {
        centralSkill -= 2;
        for (int i = 0; i < icons.Length; i++)
        {
            if (skills.Length > 1)
            {
                if (centralSkill < 0) icons[i].skill = skills[^(centralSkill * -1)];
                else if (centralSkill > skills.Length - 1) icons[i].skill = skills[centralSkill - skills.Length];
                else icons[i].skill = skills[centralSkill];
                centralSkill++;
            }
            else icons[i].skill = skills[0];
        }
        foreach(SkillIcon icon in icons)
        {
            icon.iconBack.GetComponent<Image>().sprite = icon.skill.GetIcon();
        }
    }
}
[System.Serializable]
public class SkillIcon
{
    public Skill skill;
    public RectTransform iconBack;
}
public enum DirectScroll
{
    Up, 
    Down
}
