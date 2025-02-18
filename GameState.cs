using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameState
{
    public int turnCount;
    public Vector3 playerPosition;
    public int movementPoints;
    public List<TileMetadata> tiles;
}

[System.Serializable]
public class TileMetadata
{
    public Vector3 position;
    public bool hasArtifact;
    public string artifactId;
}