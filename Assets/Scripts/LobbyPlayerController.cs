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
    [SerializeField] private int cursorIndex = 0;

    [Header("References")]
    [SerializeField] private Renderer _renderer;
    private Lobby lobby;
    private UIList _outfitList;
    private ToggleSwitch _readyToggle;
    private GameObject _currentHat;
    [SerializeField] private HybridModel currentHybrid;

    private bool isReady;

    public void Initiate(Player player, Lobby lobby, UIList outfitList, ToggleSwitch toggleSwitch)
    {
        SetPlayer(player);
        this.lobby = lobby;
        _outfitList = outfitList;
        _readyToggle = toggleSwitch;

        //cursorIndex = 0;
        hatIndex = hatModels.Length;
        outfitList.MoveTo(cursorIndex);
        UpdateColor();
    }

    private void Update()
    {
        _outfitList.SetPosition(new Vector2(lobby.mainCamera.Camera.WorldToScreenPoint(transform.position).x, (Screen.height/3)*2));
        ((RectTransform)_readyToggle.transform).position = new Vector2(lobby.mainCamera.Camera.WorldToScreenPoint(transform.position).x, ((RectTransform)_readyToggle.transform).rect.height);
    }

    protected override void OnMove(InputValue value)
    {
        if (isReady) return;

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
                case 1:
                    hatIndex += (int)input.normalized.x;
                    hatIndex = MathUtils.Mod(hatIndex, hatModels.Length + 1);
                    if (_currentHat != null) Destroy(_currentHat);
                    if (hatIndex != hatModels.Length) //Keep one slot open for no hat
                        _currentHat = Instantiate(hatModels[hatIndex], currentHybrid.hatParent);
                    break;
                default:
                    break;
            }
        }
        else
        {
            cursorIndex = _outfitList.Move(-(int)input.normalized.y);
        }
        _outfitList.AnimateButton(input.normalized);
    }

    protected override void OnSouth(InputValue value)
    {
        isReady = _readyToggle.Toggle();
        _outfitList.gameObject.SetActive(!isReady);
    }

    protected override void OnEast(InputValue value)
    {
        _readyToggle.Toggle(false);
        isReady = false;
        _outfitList.gameObject.SetActive(!isReady);
    }

    private void UpdateColor()
    {
        _renderer.material.SetColor("_BaseColor", colors[colorIndex]);
    }
    private void SaveHybridCharacter()
    {
        Player.SaveHybridModel(currentHybrid.gameObject);
    }

}
