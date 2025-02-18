import json
from flask import Flask, request, jsonify

app = Flask(__name__)

# Simulated LLM processing function.
def process_game_state(state):
    # Generate narrative based on the existence of an artifact in any tile.
    narrative = f"Turn {state.get('turnCount')}: "
    if any(tile.get('hasArtifact') for tile in state.get('tiles', [])):
        narrative += "An ancient relic glows with mystery, hinting at lost secrets."
    else:
        narrative += "The barren expanse whispers tales of lost civilizations."
    
    # Mechanical change example: grant bonus movement if a relic is discovered.
    mechanical_changes = {}
    if any(tile.get('hasArtifact') for tile in state.get('tiles', [])):
        mechanical_changes['movementBonus'] = 1
    else:
        mechanical_changes['movementBonus'] = 0
    
    return {
        "narrative": narrative,
        "mechanicalChanges": mechanical_changes
    }

@app.route('/update', methods=['POST'])
def update_narrative():
    try:
        state = request.get_json()
        result = process_game_state(state)
        return jsonify(result)
    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=True)