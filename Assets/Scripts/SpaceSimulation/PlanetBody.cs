using UnityEngine;

[ExecuteInEditMode]
public class PlanetBody : MonoBehaviour
{
    public float Radius;
    public float Mass;
    public Vector3 PlanetInitialVelocity;
    public float PlanetDistanceFromSun;

    private void Update()
    {
        transform.localScale = Vector3.one * Radius * 2;
    }
}
