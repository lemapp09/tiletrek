using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    public bool isMatched = false;
    private Board board;
    private static Tile firstSelectedTile = null;
    private static Tile secondSelectedTile = null;
    private Image image;
    private Button button;

    private void Start()
    {
        board = FindObjectOfType<Board>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnTileClick);
    }

    private void OnTileClick()
    {
        if (isMatched || firstSelectedTile == this)
            return;

        if (firstSelectedTile == null)
        {
            // First tile selected
            firstSelectedTile = this;
            HighlightTile();
        }
        else if (secondSelectedTile == null)
        {
            // Second tile selected
            secondSelectedTile = this;
            HighlightTile();
            StartCoroutine(CheckMatch());
        }
    }

    private void HighlightTile()
    {
        // Optionally change the tile's appearance to indicate it's selected
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f); // Example: change transparency
    }

    private void ResetTile()
    {
        // Reset the tile's appearance
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f); // Example: reset transparency
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f); // Pause to let player see the second tile

        if (firstSelectedTile.image.sprite == secondSelectedTile.image.sprite)
        {
            // Tiles match, remove them
            firstSelectedTile.isMatched = true;
            secondSelectedTile.isMatched = true;
            firstSelectedTile.gameObject.SetActive(false);
            secondSelectedTile.gameObject.SetActive(false);
            board.CheckIfGameWon(); // Check if the game is won
        }
        else
        {
            // Tiles don't match, reset them
            firstSelectedTile.ResetTile();
            secondSelectedTile.ResetTile();
        }

        // Reset the selected tiles
        firstSelectedTile = null;
        secondSelectedTile = null;
    }
}
