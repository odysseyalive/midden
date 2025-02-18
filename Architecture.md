```markdown
# Midden Game Architecture

## Overview
Midden is a turn-based, post-apocalyptic roguelike game built in Unity (C#) with dynamic narrative elements driven by a Python-based LLM service.

## Core Components

### Unity (C#)
- **GameManager.cs**: Manages game state, turn sequencing and integration of narrative updates.
- **PlayerController.cs**: Processes archaeologist team actions, handling movement and interactions.
- **EnvironmentTile.cs**: Represents procedural world tiles (initially using cursor-style placeholders).
- **Artifact.cs**: Encapsulates discovered artifact information (origin, significance, gameplay effects).
- **NPCEncounter.cs**: Facilitates narrative events and non-player encounters.
- **GameState.cs**: Contains definitions for serializing game state data (player position, tile metadata, etc.).

### Python Integration
- **narrative_service.py**: A Flask-based REST API that receives serialized game state from Unity, processes it using predefined rules (simulating an LLM), and returns narrative updates and mechanical changes.

## Turn-Based Gameplay Loop
1. **Player Turn**
   - The player navigates through a procedurally generated world using cursor-based controls.
   - Movement points and interactions are tracked via the PlayerController.

2. **State Serialization and End of Turn**
   - The GameManager serializes the current game state (including player data and environment metadata).
   - The game state is POSTed to the Python narrative service.

3. **LLM (Python) Turn**
   - The Python service processes the game state to generate narrative output based on conditional logic.
   - Both narrative updates and mechanical modifications (e.g., bonus movement points) are returned to Unity.
   - GameManager integrates these updates for the next turn.

## Communication Protocol
- **REST API**: Unity uses a HTTP POST request to send the serialized game state to the Python service at `/update`.
- **Error Handling**: Should the narrative service fail to respond or return an error, the GameManager logs the error and can use fallback procedures.

## Narrative & Gameplay Integration
- **Dynamic Storytelling**: The narrative service leverages game state data (such as artifact discovery) to generate contextually relevant narrative updates.
- **Mechanical Feedback**: Updates from the Python module (for example, granting bonus movement points) influence subsequent game turns.

## Future Extensions
- Transition from cursor-based tile representations to full graphical assets.
- Expand artifact interactions and narrative depth.
- Enhance error handling of the communication protocol for smoother gameplay transitions.
```