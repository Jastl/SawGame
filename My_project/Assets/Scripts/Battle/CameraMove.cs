using System.Drawing;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float smooth_speed_move = 0.025f;
    public float smooth_speed_size = 0.015f;
    public Vector3 offset = new Vector3(0, 0, -10);
    public Transform target;

    private float target_size;
    private Camera camera;
    private float camera_size => camera.orthographicSize;

    private void Update()
    {
        Vector3 smooth_position = Vector2.Lerp(transform.position, target.position, smooth_speed_move);
        transform.position = smooth_position + offset;

        if (target_size - camera_size > smooth_speed_size || target_size - camera_size < smooth_speed_size * -1) 
        {
            if (target_size < camera_size) camera.orthographicSize -= smooth_speed_size;
            else if (target_size > camera_size) camera.orthographicSize += smooth_speed_size;
        } 
    }
    public void ChangeSize(float size)
    {
        target_size = size;
    }
    private void Start()
    {
        camera = GetComponent<Camera>();
        target_size = camera_size;
    }
}
