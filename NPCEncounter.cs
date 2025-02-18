using UnityEngine;

public class NPCEncounter : MonoBehaviour
{
    public string npcName;
    public string dialogue;
    
    // Trigger an NPC or event-based encounter
    public void TriggerEncounter()
    {
        Debug.Log("Encounter with: " + npcName + " says: " + dialogue);
        // Narrative integration, such as updating game state based on NPC dialogue, goes here.
    }
}