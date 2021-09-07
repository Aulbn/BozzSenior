using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerScoreCard : PlayerController
{
    [SerializeField] private Image background;
    public TextMeshProUGUI scoreText, nameText;
    public ToggleSwitch toggleSwitch;

    //public bool isReady { get; private set; }
    public bool isReady;

    [Header("SmoothFollow")]
    [SerializeField] private bool isSmoothFollowing;
    [SerializeField] private float smoothFollowSpeed = 5f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 oldPos;

    public void Init(Player player)
    {
        SetPlayer(player);
        background.color = player.color;
        scoreText.text = player.GetLastShownScore().ToString(); //Show old score first
        nameText.text = player.playerName;

        oldPos = transform.position;
        //Debug.Log(gameObject.name + ", " + transform.position);

        toggleSwitch.gameObject.SetActive(false);
        AddInputLock();
    }

    private void Update()
    {
        if (isSmoothFollowing)
            SmoothFollowUpdate();
        else if (background.transform.localPosition != Vector3.zero)
            background.transform.localPosition = Vector3.zero;
    }

    public void ToggleSmoothFollow(bool isSmoothFollowing)
    {
        oldPos = transform.position;
        this.isSmoothFollowing = isSmoothFollowing;
    }

    private void SmoothFollowUpdate()
    {
        if (transform.position != oldPos)
        {
            background.transform.localPosition = oldPos - transform.position;
        }

        background.transform.localPosition = Vector3.Lerp(background.transform.localPosition, Vector3.zero, Time.deltaTime * smoothFollowSpeed);

        oldPos = transform.position;
    }

    public void AddPoints (int points, float jumpTime)
    {
        Tweener.Jump(this, (RectTransform)scoreText.transform, 20, jumpTime, () => {
            scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
        }, () => {
            points--;
            if (points > 0)
                AddPoints(points, jumpTime);
        });
    }

    protected override void OnSouth(InputAction.CallbackContext context)
    {
        ToggleReady(!isReady);
    }

    protected override void OnEast(InputAction.CallbackContext context)
    {
        ToggleReady(false);
    }

    private void ToggleReady(bool isReady)
    {
        this.isReady = isReady;
        toggleSwitch.Toggle(isReady, .15f);
    }

    public void ShowToggleSwitch()
    {
        toggleSwitch.gameObject.SetActive(true);
        RemoveInputLock();
    }


}
