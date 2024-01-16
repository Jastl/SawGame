using UnityEngine;
using Skills;
using Parts;
public class PartsSawManager : MonoBehaviour
{
    public GameObject sawPrefab;

    public ChainPart[] chains;
    public DiscPart[] discs;
    public TeethPart[] teeth;
    private void Awake()
    {
        PartsSaw.sawPrefab = sawPrefab;
        PartsSaw.CreateParts(chains, discs, teeth);
        PartsSaw.SetParts(0, 0, 0); //make load
    }
}
public static class PartsSaw
{
    public static GameObject sawPrefab;
    //all parts list
    private static ChainPart[] chains;
    private static DiscPart[] discs;
    private static TeethPart[] teeth;

    public static ChainPart currentChain;
    public static DiscPart currentDisc;
    public static TeethPart currentTeeth;
    

    //set images on saw
    public static void SetSawParts(GameObject saw)
    {
        saw.transform.Find("Teeth").GetComponent<SpriteRenderer>().sprite = currentTeeth.image;
        saw.transform.Find("Disc_1/Teeth").GetComponent<SpriteRenderer>().sprite = currentTeeth.image;

        saw.transform.Find("Disc_1").GetComponent<SpriteRenderer>().sprite = currentDisc.image;
        saw.GetComponent<SpriteRenderer>().sprite = currentDisc.image;

        saw.transform.Find("Chain").GetComponent<SpriteRenderer>().sprite = currentChain.image;
    }
    //create lists with parts
    public static void CreateParts(ChainPart[] chain, DiscPart[] disc, TeethPart[] teeth_)
    {
        chains = chain;
        teeth = teeth_;
        discs = disc;
    }
    public static void SetParts(int chainIndex, int discIndex, int teethIndex) 
    {
        currentChain = chains[chainIndex];
        currentDisc = discs[discIndex];
        currentTeeth = teeth[teethIndex];
    }
}
namespace Parts
{
    [System.Serializable]
    public class Part
    {
        public Sprite image;
        public Skill skill;
        public int lvl;
    }
    [System.Serializable]
    public class ChainPart : Part
    {
        public float minLenght;
        public float maxLenght;
    }
    [System.Serializable]
    public class TeethPart : Part
    {
        public float damage;
    }
    [System.Serializable]
    public class DiscPart : Part
    {
        public float speedRotation;
    }
}

