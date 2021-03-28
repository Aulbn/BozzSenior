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
    public ToggleSwitch _readyToggle { get; private set; }
    private GameObject _currentHat;
    [SerializeField] private HybridModel currentHybrid;

    private bool isReady;

    public void Initiate(Player player, Lobby lobby, UIList outfitList, ToggleSwitch toggleSwitch)
    {
        SetPlayer(player);
        this.lobby = lobby;
        _outfitList = outfitList;
        _readyToggle = toggleSwitch;

        player.SetController(this);
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

    protected override void OnMove(InputAction.CallbackContext context)
    {
        if (isReady) return;

        Vector2 input = context.ReadValue<Vector2>();

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

    protected override void OnSouth()
    {
        ToggleReady(_readyToggle.Toggle());
    }

    protected override void OnEast()
    {
        if (!isReady)
        {
            Disconnect();
            return;
        }
        _readyToggle.Toggle(false);
        ToggleReady(false);
    }

    private void ToggleReady(bool isOn)
    {
        isReady = isOn;
        _outfitList.gameObject.SetActive(!isReady);
        if (isReady)
            lobby.nOReadySwitches++;
        else
            lobby.nOReadySwitches--;
    }

    private void Disconnect()
    {
        Player.Disconnect();
        Destroy(_readyToggle.gameObject);
        Destroy(_outfitList.gameObject);
        Destroy(gameObject);
    }

    private void UpdateColor()
    {
        _renderer.material.SetColor("_BaseColor", colors[colorIndex]);
    }
    public void SaveHybridCharacter()
    {
        Player.SaveHybridModel(currentHybrid.gameObject);
        Player.color = colors[colorIndex];
    }
}
