using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GradientBackground : MonoBehaviour
{
    public Color topColor = Color.white;
    public Color bottomColor = Color.black;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        GenerateGradient();
    }

    void GenerateGradient()
    {
        Texture2D texture = new Texture2D(1, 2);
        texture.SetPixels(new Color[] { bottomColor, topColor });
        texture.Apply();

        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.type = Image.Type.Sliced;
    }
}