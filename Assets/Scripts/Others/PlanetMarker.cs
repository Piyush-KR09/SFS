using UnityEngine;

public class PlanetMarker : MonoBehaviour
{
    [SerializeField] private GameObject Ptag;
    [SerializeField] private Canvas canvas;
    private GameObject[] planetTags;
    [SerializeField] private Camera Mcamera;
    [SerializeField]private float conversionFactor = 245f;
    private Universe unive;

    private void Start()
    {
        unive = GetComponent<Universe>();
        Mcamera = Camera.main;
        planetTags = new GameObject[unive.Planets.Length];
        for (int i = 0; i < unive.Planets.Length; i++)
        {
            planetTags[i] = Instantiate(Ptag, unive.Planets[i].transform.position, Quaternion.identity, canvas.transform);
        }
    }

    private void Update()
    {
        for (int i = 0; i < unive.Planets.Length; i++)
        {
            planetTags[i].transform.position = unive.Planets[i].transform.position;
        }
        canvas.transform.localScale = Quaternion.identity * Vector3.one * Mcamera.orthographicSize / conversionFactor;
    }
}
