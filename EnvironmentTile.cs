using UnityEngine;

public class EnvironmentTile : MonoBehaviour
{
    // Each tile can optionally have an artifact
    public Artifact artifact;
    
    // Additional properties such as hazards can be added here
    public bool hasHazard = false;
    public int hazardLevel = 0;
}