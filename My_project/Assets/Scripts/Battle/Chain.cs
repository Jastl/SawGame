using UnityEngine;

public class Chain : MonoBehaviour
{
    public Transform target;
    private SpriteRenderer sprite_renderer;
    private Vector2 position_target => target.localPosition;
    private void Update()
    {
        sprite_renderer.size = new Vector2(sprite_renderer.size.x, position_target.y);
        transform.localPosition = new Vector3(position_target.x / 2, position_target.y / 2, 1); 
    }
    private void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }
}
