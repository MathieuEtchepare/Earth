using UnityEngine;

public class MyCamera : MonoBehaviour {
    public float minSize = 15f;
    public float maxSize = 60f;
    public float sensitivity = 10f;
    public float delta = 10f; // Pixels. The width border at the edge in which the movement work

    void Update()
    {
        Zoom();
        Move();
    }

    private void Zoom()
    {
        float size = Camera.main.orthographicSize;
        size -= Input.GetAxis("Mouse ScrollWheel") * sensitivity * Time.deltaTime;
        size = Mathf.Clamp(size, minSize, maxSize);
        Camera.main.orthographicSize = size;
    }

    private void Move()
    {
        if (Input.mousePosition.x >= Screen.width - delta)
        {
            transform.position += Vector3.right * Time.deltaTime * sensitivity / 4;
        }else if(Input.mousePosition.x <= 0 + delta)
        {
            transform.position -= Vector3.right * Time.deltaTime * sensitivity / 4;
        }

        if (Input.mousePosition.y >= Screen.height - delta)
        {
            transform.position += Vector3.up * Time.deltaTime * sensitivity / 4;
        }
        else if (Input.mousePosition.y <= 0 + delta)
        {
            transform.position -= Vector3.up * Time.deltaTime * sensitivity / 4;
        }
    }
}
