using UnityEngine;

public class NBodySimulation : MonoBehaviour
{
    private Vector3[] currentVec;
    private Universe unive;

    //[SerializeField] private int TimeScale = 1;

    private void Start()
    {
        unive = gameObject.GetComponent<Universe>();
        currentVec = new Vector3[unive.Planets.Length];
        for (int i = 0; i < unive.Planets.Length; i++)
        {
            currentVec[i] = unive.Planets[i].PlanetInitialVelocity;
        }
    }

    private void FixedUpdate()
    {
        UpdateVelocities();
        UpdatePositions();
        Time.fixedDeltaTime = unive.PhysicsTimeStep;
    }

    void UpdateVelocities()
    {
        for (int i = 0; i < unive.Planets.Length; i++)
        {
            for (int j = 0; j < unive.Planets.Length; j++)
            {
                if (unive.Planets[i] != unive.Planets[j])
                {
                    float distanceSqr = Vector3.SqrMagnitude(unive.Planets[j].transform.position - unive.Planets[i].transform.position);
                    Vector3 distanceVector = (unive.Planets[j].transform.position - unive.Planets[i].transform.position).normalized;
                    Vector3 acceleration = unive.GravitationalConstant * unive.Planets[j].Mass * distanceVector / distanceSqr;
                    currentVec[i] += acceleration * unive.PhysicsTimeStep;
                }
            }
        }
    }

    void UpdatePositions()
    {
        for (int i = 0; i < unive.Planets.Length; i++)
        {
            unive.Planets[i].transform.position += currentVec[i] * unive.PhysicsTimeStep;
        }
    }
}
