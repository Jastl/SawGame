using System.Collections;
using UnityEngine;


namespace Skills
{
    public class SkillManager : MonoBehaviour
    {
        private Skill.UpdateD[] update = new Skill.UpdateD[3];
        private Skill.TimerD[] timers = new Skill.TimerD[3];
        private void Start()
        {
            update[0] = PartsSaw.currentDisc.GetUpdate();
            update[1] = PartsSaw.currentChain.GetUpdate();
            update[2] = PartsSaw.currentTeeth.GetUpdate();

            timers[0] = PartsSaw.currentDisc.GetTimer();
            timers[1] = PartsSaw.currentChain.GetTimer();
            timers[2] = PartsSaw.currentTeeth.GetTimer();

            StartCoroutine(Timer());
        }
        private void Update()
        {
            foreach (Skill.UpdateD update in update) if (update != null) update();
        }
        private IEnumerator Timer()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(0.05f);
                foreach (Skill.TimerD timer in timers) if (timer != null) timer();
            }
        }
    }
    [System.Serializable]
    public class Skill
    {
        private Sprite icon;
        protected Saw saw { get; private set; }
        public delegate void UpdateD();
        public delegate void TimerD();
        public UpdateD updateD { get; protected set; }
        public TimerD timerD { get; protected set; }

        public Skill(GameObject saw, string nameSprite)
        {
            this.saw = saw.GetComponent<Saw>();
            updateD = Update;
            timerD = Timer;
            icon = Resources.Load<Sprite>(nameSprite);
        }
        public Sprite GetIcon() { return icon; }
        public virtual void Activate() { }
        public virtual void Update() { }
        public virtual void Timer() { }
        public virtual float GetFillAmount() {  return 0; }
    }

    public class PassiveSkill : Skill
    {
        public PassiveSkill(GameObject saw, string nameSprite) : base(saw, nameSprite) { }
    }
    public class ActiveSkill : Skill
    {
        private float duration;
        private float cooldown;
        private float timer;
        private protected bool active;

        public ActiveSkill(GameObject saw, float duration, float cooldown, string nameSprite) : base(saw, nameSprite)
        {
            timer = cooldown;
            this.duration = duration;
            this.cooldown = cooldown;
        }
        public override void Timer()
        {
            //the timer works only when the skill is active or the skill is on cooldown
            if (active || timer < cooldown)
            {
                timer += 0.05f;
            }
            //disactivate the skill when the duration expires
            if (timer >= duration && active)
            {
                active = false;
                timer = 0f;
            }
        }
        public override float GetFillAmount()
        {
            if (!active) return timer / cooldown;
            else return 0f;
        }

        public override void Activate()
        {
            if (!active) active = timer >= cooldown;
            timer = 0f;
        }
    }
    public class MultipleA : ActiveSkill
    {
        public ParametrSaw ParametrType { get; set; }
        public float Multiplier { get; set; }
        public MultipleA(GameObject saw, ParametrSaw parametr, float multimlier, float duration, float cooldown, string nameSprite) : base(saw, duration, cooldown, nameSprite)
        {
            ParametrType = parametr;
            Multiplier = multimlier;
        }
        public override void Update()
        {
            if (active)
            {
                switch (ParametrType)
                {
                    case ParametrSaw.SpeedRotation:
                        saw.speed_rotation = PartsSaw.currentDisc.speedRotation * Multiplier;
                        break;
                    case ParametrSaw.Damage:
                        saw.damage = PartsSaw.currentTeeth.damage * Multiplier;
                        break;
                }
            }
            else
            {
                saw.speed_rotation = PartsSaw.currentDisc.speedRotation;
                saw.damage = PartsSaw.currentTeeth.damage;
            }
        }
    }   
    public enum ParametrSaw
    {
        SpeedRotation,
        Damage,
        LenghtChain
    }
}
