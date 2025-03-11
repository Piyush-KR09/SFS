using UnityEngine;

public class CamControls : MonoBehaviour
{
    Camera cameraO;
    public float zoomFactor;

    private void Start()
    {
        cameraO = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
        {
            cameraO.orthographicSize = Mathf.Lerp(cameraO.orthographicSize, cameraO.orthographicSize + 300, Time.deltaTime * zoomFactor);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
        {
            cameraO.orthographicSize = Mathf.Lerp(cameraO.orthographicSize, cameraO.orthographicSize - 300, Time.deltaTime * zoomFactor);
        }
    }
}
