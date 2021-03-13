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
    private UIList outfitList;

    public void Initiate(Player player, Lobby lobby, UIList outfitList)
    {
        SetPlayer(player);
        this.lobby = lobby;
        this.outfitList = outfitList;

        cursorIndex = 0;
        outfitList.MoveTo(cursorIndex);
        UpdateColor();
    }

    private void Update()
    {
        outfitList.SetPosition(new Vector2(lobby.mainCamera.Camera.WorldToScreenPoint(transform.position).x, (Screen.height/3)*2));
    }

    protected override void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input.x != 0)
        {
            switch (cursorIndex)
            {
                case 0:
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
            cursorIndex = outfitList.Move(-(int)input.normalized.y);
        }
        outfitList.AnimateButton(input.normalized);
    }

    private void UpdateColor()
    {
        _renderer.material.SetColor("_BaseColor", colors[colorIndex]);
    }

}
