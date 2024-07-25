using UnityEngine;

// Basic Tile Script
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    public bool isMatched = false;
    private Board board;
    private static Tile selectedTile = null;

    private void Start()
    {
        board = FindObjectOfType<Board>();
    }

    private void OnMouseDown()
    {
        // If no tile is currently selected
        if (selectedTile == null)
        {
            // Select this tile
            selectedTile = this;
            // Optionally, highlight the selected tile visually
            // e.g., change the tile's color or outline
        }
        else
        {
            // Swap the selected tile with this tile
            SwapTiles(selectedTile);
            selectedTile.CheckMatch();
            CheckMatch();
            
            // Deselect the selected tile
            selectedTile = null;
        }
    }

    public void SwapTiles(Tile otherTile)
    {
        // Swap the positions in the board array
        int tempX = otherTile.x;
        int tempY = otherTile.y;
        otherTile.x = this.x;
        otherTile.y = this.y;
        this.x = tempX;
        this.y = tempY;

        // Swap the positions in the Unity scene
        Vector3 tempPosition = otherTile.transform.position;
        otherTile.transform.position = this.transform.position;
        this.transform.position = tempPosition;
    }

    private void CheckMatch()
    {
        // Match-checking logic
        if (board != null)
        {
            isMatched = board.CheckForMatch(this);
        }
    }
}
