using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteDisplay : MonoBehaviour
{
    public enum InputSprite
    {
        North, South, East, West
    }
    public enum Scheme
    {
        Playstation, Xbox
    }
    public InputSprite input;
    public Scheme scheme;
    public Image image;

    private void Start()
    {
        SetSprite();
    }

    public void SetSprite()
    {
        if (image != null)
            image.sprite = Resources.Load<Sprite>(GetSpritePath());
    }

    private string GetSpritePath()
    {
        string path = "Sprites/Input/";

        switch (input)
        {
            case InputSprite.East:
                path += "East";
                break;
            case InputSprite.North:
                path += "North";
                break;
            case InputSprite.South:
                path += "South";
                break;
            case InputSprite.West:
                path += "West";
                break;
            default:
                path = string.Empty;
                break;
        }

        return path + "/" + (int)scheme;
    }
}
