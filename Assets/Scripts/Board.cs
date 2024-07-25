using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Board : MonoBehaviour
{
    public Tile[,] tiles;
    public int width = 6; // Set to 6 to fit the 6x8 grid
    public int height = 8; // Set to 8 to fit the 6x8 grid
    public GameObject[] tilePrefabs; // Array of Tile UI prefabs
    public RectTransform gameBoard; // Reference to the GameBoard panel
    public TMP_Text gameWonText; // Reference to the GameWon text element

    private void Start()
    {
        tiles = new Tile[width, height];
        InitializeBoard();
        gameWonText.gameObject.SetActive(false); // Hide the GameWon text at the start
    }

    private void InitializeBoard()
    {
        float tileWidth = gameBoard.rect.width / width;
        float tileHeight = gameBoard.rect.height / height;

        List<Vector2Int> positions = new List<Vector2Int>();

        // Create a list of all tile positions
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                positions.Add(new Vector2Int(x, y));
            }
        }

        // Shuffle the list of positions
        for (int i = 0; i < positions.Count; i++)
        {
            Vector2Int temp = positions[i];
            int randomIndex = Random.Range(i, positions.Count);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        int tilePrefabIndex = 0;

        // Assign each tile type to 4 random positions from the shuffled list
        foreach (Vector2Int pos in positions)
        {
            if (tilePrefabIndex >= tilePrefabs.Length)
            {
                tilePrefabIndex = 0;
            }

            // Instantiate the Tile UI prefab
            GameObject tileObject = Instantiate(tilePrefabs[tilePrefabIndex], gameBoard);
            RectTransform rectTransform = tileObject.GetComponent<RectTransform>();

            // Calculate the position relative to the center of the GameBoard
            float posX = (pos.x * tileWidth) - (gameBoard.rect.width / 2) + (tileWidth / 2);
            float posY = (pos.y * tileHeight) - (gameBoard.rect.height / 2) + (tileHeight / 2);

            // Set the size and position of the tile
            rectTransform.sizeDelta = new Vector2(tileWidth, tileHeight);
            rectTransform.anchoredPosition = new Vector2(posX, -posY); // Negative y to position correctly in UI

            // Name the tile object for easier identification
            tileObject.name = $"Tile_{pos.x}_{pos.y}";

            // Get the Tile component
            Tile tile = tileObject.GetComponent<Tile>();

            // Initialize the tile's coordinates
            tile.x = pos.x;
            tile.y = pos.y;

            // Assign the tile to the tiles array
            tiles[pos.x, pos.y] = tile;

            // Increment the tilePrefabIndex for the next tile
            tilePrefabIndex++;
        }
    }

    public bool CheckForMatch(Tile tile)
    {
        // Add your match-checking logic here
        // This could involve checking the rows and columns for matches of 3 or more tiles of the same type
        return false; // Placeholder return value
    }

    public void CheckIfGameWon()
    {
        foreach (var tile in tiles)
        {
            if (!tile.isMatched)
            {
                return; // If any tile is not matched, return early
            }
        }

        // All tiles are matched
        gameWonText.gameObject.SetActive(true);
    }
}
