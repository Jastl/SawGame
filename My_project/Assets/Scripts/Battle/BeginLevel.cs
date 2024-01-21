using UnityEngine;

public class BeginLevel : MonoBehaviour
{
    void Awake()
    {
        Transform saw = Instantiate(PartsSaw.sawPrefab).transform;
        this.GetComponent<CameraMove>().target = saw;
        PartsSaw.SetSawParts(saw.gameObject);
        PartsSaw.SetSkills(saw.gameObject);
    }
}
