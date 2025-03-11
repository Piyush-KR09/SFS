using UnityEngine;

[ExecuteInEditMode]
public class OrbitPredictor : MonoBehaviour
{
    public int Steps;
    public bool CheckForCollision;
    private Universe unive;
    public bool useCentralBody;
    public PlanetBody centralBody;

    private void Update()
    {
        if(!Application.isPlaying) DrawOrbits();
    }

    void DrawOrbits()
    {
        //Initialise
        unive = gameObject.GetComponent<Universe>();
        PlanetBody[] Planets = GameObject.FindObjectsOfType<PlanetBody>();
        VirtualBody[] virtualBodies = new VirtualBody[Planets.Length];
        Vector3[][] PlanetPredictedPos = new Vector3[Planets.Length][];

        int centralBodyIndex = 0;
        Vector3 referenceBodyInitialPosition = Vector3.zero;

        int currentStep;

        for (int i = 0; i < Planets.Length; i++)
        {
            virtualBodies[i] = new VirtualBody(Planets[i]);
            PlanetPredictedPos[i] = new Vector3[Steps];

            if (Planets[i] == centralBody && useCentralBody) 
            {
                centralBodyIndex = i;
                referenceBodyInitialPosition = virtualBodies[i].position;
            }
        }

        //Simulate
        for (currentStep = 0; currentStep < Steps; currentStep++)
        {
            Vector3 referenceBodyPosition = (useCentralBody) ? virtualBodies[centralBodyIndex].position : Vector3.zero;

            //Velocity
            for (int i = 0; i < virtualBodies.Length; i++)
            {
                for (int j = 0; j < virtualBodies.Length; j++)
                {
                    if (Planets[i] != Planets[j])
                    {
                        float distanceSqr = Vector3.SqrMagnitude(virtualBodies[j].position - virtualBodies[i].position);
                        Vector3 distanceVector = (virtualBodies[j].position - virtualBodies[i].position).normalized;
                        Vector3 acceleration = unive.GravitationalConstant * virtualBodies[j].mass * distanceVector / distanceSqr;
                        virtualBodies[i].velocity += acceleration * unive.PhysicsTimeStep;
                    }
                }
            }

            //Position
            for (int i = 0; i < virtualBodies.Length; i++)
            {
                Vector3 newPos = virtualBodies[i].position + virtualBodies[i].velocity * unive.PhysicsTimeStep;
                virtualBodies[i].position = newPos;
                
                if (useCentralBody)
                {
                    var referenceFrameOffset = referenceBodyPosition - referenceBodyInitialPosition;
                    newPos -= referenceFrameOffset;
                }
                if (useCentralBody && i == centralBodyIndex)
                {
                    newPos = referenceBodyInitialPosition;
                }

                PlanetPredictedPos[i][currentStep] = newPos;
            }

            if (CheckForCollision)
            {
                for (int i = 0; i < virtualBodies.Length; i++)
                {
                    for (int j = i + 1; j < virtualBodies.Length; j++)
                    {
                        float sumOfRadii = virtualBodies[i].radius + virtualBodies[j].radius;
                        float distance = Vector3.Magnitude(virtualBodies[i].position - virtualBodies[j].position);
                        if (distance < sumOfRadii)
                        {
                            Debug.Log(Planets[i].name + " will collide with " + Planets[j].name + " at " + currentStep + " step");
                            goto DrawLine;
                        }
                    }
                }
            }
        }

    DrawLine:
        //Draw Line
        for (int i = 0; i < virtualBodies.Length; i++)
        {
            Color pathColour = Planets[i].GetComponent<SpriteRenderer>().color;

            for (int j = 0; j < currentStep - 1; j++)
            {
                Debug.DrawLine(PlanetPredictedPos[i][j], PlanetPredictedPos[i][j + 1], pathColour);
            }
        }
    }

    class VirtualBody
    {
        public float radius;
        public Vector3 position;
        public float mass;
        public Vector3 velocity;

        public VirtualBody(PlanetBody pb)
        {
            radius = pb.Radius;
            position = pb.transform.position;
            mass = pb.Mass;
            velocity = pb.PlanetInitialVelocity;
        }
    }
}
