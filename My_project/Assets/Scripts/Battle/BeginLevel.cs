using UnityEngine;

public class BeginLevel : MonoBehaviour
{
    void Start()
    {
        Transform saw = Instantiate(PartsSaw.sawPrefab).transform;
        this.GetComponent<CameraMove>().target = saw;
        PartsSaw.SetSawParts(saw.gameObject);
    }
}
