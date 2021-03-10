using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyPlayerController : PlayerController
{
    public Color[] colors;
    public HybridModel[] hybridModels;
    public GameObject[] hatModels;

    [SerializeField] private int colorIndex, hybridIndex, hatIndex;
    [SerializeField] private int cursorIndex;

    [Header("References")]
    [SerializeField] private Renderer _renderer;
    private Lobby lobby;

    public void Initiate(Player player, Lobby lobby)
    {
        SetPlayer(player);
        this.lobby = lobby;

        cursorIndex = 0;
        lobby.MoveCursor(cursorIndex);
        UpdateColor();
    }

    protected override void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input.x != 0)
        {
            switch (cursorIndex)
            {
                case 3:
                    colorIndex += (int)input.normalized.x;
                    colorIndex = MathUtils.Mod(colorIndex, colors.Length);
                    UpdateColor();
                    break;
                default:
                    break;
            }
        }
        else
        {
            cursorIndex = Mathf.Clamp(cursorIndex - (int)input.normalized.y, 0, 3);
            Debug.Log("Cursor index: " + cursorIndex);
            lobby.MoveCursor(cursorIndex);
        }
    }

    private void UpdateColor()
    {
        _renderer.material.SetColor("_BaseColor", colors[colorIndex]);
    }

}
