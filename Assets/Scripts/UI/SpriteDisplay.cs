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
    public Image image, background;

    private void Start()
    {
        SetSprite();
    }

    public void SetSprite()
    {
        if (image != null)
        {
            SpriteCollection.SpriteVariant variant = GetSpriteVariant(Resources.Load<SpriteCollection>(GetSpritePath()));
            image.sprite = variant.sprite;
            image.color = variant.color;
            if (background != null)
                background.sprite = variant.backgroundSprite;
        }
    }
    public void SetSprite(InputSprite input)
    {
        this.input = input;
        SetSprite();
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

        return path;
    }

    private SpriteCollection.SpriteVariant GetSpriteVariant(SpriteCollection collection)
    {
        switch (scheme)
        {
            case Scheme.Playstation:
                return collection.sv_playstation;
            case Scheme.Xbox:
                return collection.sv_xbox;
            default:
                return collection.sv_pc;
        }
    }

}
