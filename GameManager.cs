using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json; // Ensure you have the Newtonsoft.Json package installed

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public PlayerController playerController;
    
    // Turn counter
    private int turnCount = 0;
    
    // List of all environment tiles in the scene
    public List<EnvironmentTile> environmentTiles;
    
    // Store narrative text received from the Python narrative service
    public string narrativeText = "";
    
    // API endpoint for the narrative service
    public string narrativeServiceUrl = "http://localhost:5000/update"; // Example URL
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Initialize the game state
        turnCount = 1;
        Debug.Log("Game Start: Turn " + turnCount);
    }
    
    // Called when the player ends their turn
    public void EndPlayerTurn()
    {
        // Capture and serialize the current game state
        GameState state = CaptureGameState();
        string stateJson = JsonConvert.SerializeObject(state);
        Debug.Log("Serialized GameState: " + stateJson);
        
        // Send the state to the Python narrative service
        StartCoroutine(SendStateToNarrativeService(stateJson));
    }
    
    // Capture relevant game state data for transmission
    GameState CaptureGameState()
    {
        GameState state = new GameState();
        state.turnCount = turnCount;
        state.playerPosition = playerController.transform.position;
        state.movementPoints = playerController.movementPoints;
        
        // Gather metadata from each EnvironmentTile
        List<TileMetadata> tilesMeta = new List<TileMetadata>();
        foreach (var tile in environmentTiles)
        {
            TileMetadata meta = new TileMetadata();
            meta.position = tile.transform.position;
            meta.hasArtifact = tile.artifact != null;
            meta.artifactId = tile.artifact != null ? tile.artifact.artifactId : "";
            tilesMeta.Add(meta);
        }
        state.tiles = tilesMeta;
        return state;
    }
    
    // Coroutine to send game state to the narrative service
    IEnumerator SendStateToNarrativeService(string jsonState)
    {
        var req = new UnityEngine.Networking.UnityWebRequest(narrativeServiceUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonState);
        req.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        
        yield return req.SendWebRequest();
        
        if (req.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending game state: " + req.error);
            // Fallback error handling can be implemented here.
        }
        else
        {
            // Process the narrative response from the Python service
            string narrativeResponse = req.downloadHandler.text;
            Debug.Log("Narrative service response: " + narrativeResponse);
            narrativeText = narrativeResponse;
        }
        turnCount++;
    }
}