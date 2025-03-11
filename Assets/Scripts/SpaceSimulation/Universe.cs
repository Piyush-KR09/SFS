using System;
using UnityEngine;
using UnityEngine.UI;

public class Universe : MonoBehaviour
{
    public float GravitationalConstant;
    public float PhysicsTimeStep;
    public InputField inputField;

    public PlanetBody[] Planets { get; private set; }
    void Awake()
    {
        Planets = GameObject.FindObjectsOfType<PlanetBody>();
    }

    private void FixedUpdate()
    {
        Time.timeScale = Convert.ToInt32(inputField.text);
        Time.fixedDeltaTime = PhysicsTimeStep;
    }

}
