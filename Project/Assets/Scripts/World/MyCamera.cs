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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= Vector3.right * Time.deltaTime * sensitivity / 4;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Time.deltaTime * sensitivity / 4;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * Time.deltaTime * sensitivity / 4;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= Vector3.up * Time.deltaTime * sensitivity / 4;
        }
    }
}
