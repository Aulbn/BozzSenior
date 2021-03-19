using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] private Image toggleBackground;
    [SerializeField] private Color onColor, offColor;
    [SerializeField] private SpriteDisplay toggleButton;
    [SerializeField] private Image toggleMaskOn, toggleMaskOff;
    [SerializeField] private SpriteDisplay.InputSprite onSprite;
    [SerializeField] private SpriteDisplay.InputSprite offSprite;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private Transform onPosition, offPosition;
    public bool isOn { get; private set; }

    public void Toggle(bool isOn)
    {
        Toggle(isOn, .15f);
    }

    public void Toggle(bool isOn, float transitionSpeed)
    {
        StopAllCoroutines();
        StartCoroutine(IETransition(isOn, transitionSpeed));
    }

    public bool Toggle()
    {
        Toggle(!isOn);
        return isOn;
    }

    private IEnumerator IETransition(bool isOn, float transitionTime)
    {
        float time = 0;
        float animVal;

        Vector3 buttonStartPos = toggleButton.transform.position;
        float offMaskStartVal = toggleMaskOff.fillAmount, onMaskStartVal = toggleMaskOn.fillAmount;
        Color startColor = toggleBackground.color;

        this.isOn = isOn;

        while (time < transitionTime)
        {
            time += Time.deltaTime;
            animVal = animationCurve.Evaluate(time / transitionTime);

            toggleMaskOff.fillAmount = Mathf.Lerp(offMaskStartVal, isOn ? 0 : 1, animVal);
            toggleMaskOn.fillAmount = Mathf.Lerp(onMaskStartVal, isOn ? 1 : 0, animVal);
            
            toggleButton.transform.position = Vector3.Lerp(buttonStartPos, isOn ? onPosition.position : offPosition.position, animVal);
            toggleButton.SetSprite(toggleMaskOff.fillAmount > 0.5f ? onSprite : offSprite);

            toggleBackground.color = Color.Lerp(startColor, isOn ? onColor : offColor, animVal);

            yield return null;
        }

    }

    private float Revealuate(float value, bool anti)
    {
        return (anti ? 1 : 0) - value * (anti ? 1 : -1);
    }
    public void SetPosition(Vector2 position)
    {
        ((RectTransform)transform).position = position;
    }

}
