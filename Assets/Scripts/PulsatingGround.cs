using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingGround : MonoBehaviour
{
    public Material material;        // The HDRP material with the emission property
    public Color baseEmissionColor;  // Base color for emission
    public float pulsateSpeed = 2.0f; // Speed of the pulsation
    public float minEmission = 0.1f;  // Minimum intensity of the emission
    public float maxEmission = 10.0f; // Maximum intensity of the emission

    private void Start()
    {
        if (material == null)
        {
            // Get the material from the Renderer if not explicitly set
            material = GetComponent<Renderer>().material;
        }

        // Ensure the shader has emission enabled
        material.EnableKeyword("_EMISSION");
    }

    private void Update()
    {
        // Calculate the emission intensity over time
        float emissionIntensity = Mathf.Lerp(minEmission, maxEmission, Mathf.PingPong(Time.time * pulsateSpeed, 1.0f));

        // Set the emission color and intensity
        Color finalEmissionColor = baseEmissionColor * emissionIntensity;

        // Apply the emission color to the material
        material.SetColor("_EmissiveColor", finalEmissionColor);
    }
}
