using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIList : MonoBehaviour
{
    [Serializable] struct Arrows
    {
        [SerializeField] private RectTransform arrowUp, arrowDown, arrowLeft, arrowRight;
        public void Animate(MonoBehaviour owner, Vector2 direction)
        {
            RectTransform arrow;
            switch (direction)
            {
                case Vector2 v when v.Equals(Vector2.up):
                    arrow = arrowUp;
                    break;
                case Vector2 v when v.Equals(Vector2.down):
                    arrow = arrowDown;
                    break;
                case Vector2 v when v.Equals(Vector2.left):
                    arrow = arrowLeft;
                    break;
                case Vector2 v when v.Equals(Vector2.right):
                    arrow = arrowRight;
                    break;
                default:
                    arrow = arrowUp;
                    break;
            }
            Tweener.Pulse(owner, arrow, 2, .1f, () => { });
        }
    }

    public float scrollSpeed;
    public bool wrapAround;
    [Space]
    [SerializeField] private HorizontalOrVerticalLayoutGroup layoutGroup;
    [SerializeField] private Arrows arrows;

    public int Count => content.childCount;
    public int CurrentIndex { get; private set; }

    private RectTransform content;
    private Vector2 scrollTargetPosition;
    private bool isVerical;
    private float directionalSize;

    protected void Start()
    {
        isVerical = layoutGroup is VerticalLayoutGroup;
        if (content == null) content = (RectTransform)layoutGroup.transform;

        MoveTo(0);

        Canvas.ForceUpdateCanvases();
        directionalSize = ((isVerical ? content.rect.height : content.rect.width) - layoutGroup.spacing * (Count - 1)) / Count;
    }

    private void Update()
    {
        content.localPosition = Vector2.Lerp(content.localPosition, scrollTargetPosition, Time.deltaTime * scrollSpeed);
    }

    public void MoveTo(int index)
    {
        if (content == null) Start();
        CurrentIndex = wrapAround ? MathUtils.Mod(index, Count) : Mathf.Clamp(index, 0, Count - 1);

        float indexZeroPos = -((isVerical ? content.rect.height : content.rect.width) / 2 - directionalSize / 2);
        scrollTargetPosition = new Vector2(0, indexZeroPos + ((directionalSize + layoutGroup.spacing) * CurrentIndex));
    }
    public int Move(int offset)
    {
        MoveTo(CurrentIndex + offset);
        return CurrentIndex;
    }

    public void AnimateButton(Vector2 direction)
    {
        arrows.Animate(this, direction);
    }
    public Transform GetElement(int index)
    {
        return content.GetChild(index);
    }
    public Transform GetCurrentElement(int index)
    {
        return content.GetChild(index);
    }
    public void SetPosition(Vector2 position)
    {
        ((RectTransform)transform).position = position;
    }
}
