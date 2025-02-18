using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int movementPoints = 5;
    
    void Update()
    {
        // Example movement using arrow keys. In a full implementation, this could be more robust.
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.up);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.down);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
        
        // End turn using the Enter key (for demonstration purposes)
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.EndPlayerTurn();
        }
    }
    
    // Handle movement and deduct movement points
    void Move(Vector3 direction)
    {
        if(movementPoints > 0)
        {
            transform.position += direction;
            movementPoints--;
            Debug.Log("Moved to: " + transform.position + ". Points left: " + movementPoints);
        }
        else
        {
            Debug.Log("No movement points left.");
        }
    }
}