using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
//using DG.Tweening;

public class PlayerColorSelect : MonoBehaviour
{
    public bool isReady;
    [Header("Color")]
    public Color[] colors;
    private int index = 0;

    [Header("References")]
    public Image image;
    public Image checkmark;
    public RectTransform arrowUp, arrowDown;

    private Player player;

    private void Start()
    {
        SetReady(false);
    }

    public void Initiate(Player player)
    {
        this.player = player;
        SetColor(player.color);
        OnEnable();
    }

    private void OnEnable()
    {
        if (!player) return;
        player.onMove += ChangeColor;
        player.onSouth += OnSubmit;
        player.onEast += OnBack;
    }
    private void OnDisable()
    {
        player.onMove -= ChangeColor;
        player.onSouth -= OnSubmit;
        player.onEast -= OnBack;
    }

    public void ChangeColor(InputValue value)
    {
        if (!image || isReady) return;

        index -= (int)value.Get<Vector2>().y;
        if (index < 0)
            index = colors.Length - 1;

        SetColor(colors[index % colors.Length]);
    }

    private void SetColor(Color color)
    {
        player.color = color;
        image.color = color;
    }

    private void SetReady(bool value)
    {
        isReady = value;
        checkmark.enabled = value;
    }

    private void OnSubmit(InputValue value)
    {
        if (!isReady)
        {
            SetReady(true);
        }
        else
        {
            //GameManager.LoadScene("Level1");
            GameManager.LoadNextLevel();
            Debug.Log("Start game");
        }
    }

    private void OnBack(InputValue value)
    {
        if (isReady)
        {
            SetReady(false);
        }
        else
        {
            player.Disconnect();
            Destroy(gameObject);
            Debug.Log("Leave game");
        }
    }

}
