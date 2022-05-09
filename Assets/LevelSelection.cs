using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelection : MonoBehaviour
{
    private Button _CurrentSelection;
    private bool _IsInFocus;

    public List<LevelObject> LevelList;

    [Header("UI")]
    public GameObject UIButtonPrefab;

    [Header("References")]
    public Transform LevelListParent;
    public Transform SelectedLevelsParent;

    private void Start()
    {
        CreateLevelList();
        _CurrentSelection = LevelListParent.GetChild(0).GetComponent<Button>();
        _CurrentSelection.Select();
    }

    private void CreateLevelList()
    {
        foreach (LevelObject levelObject in LevelList)
        {
            GameObject go = Instantiate(UIButtonPrefab, LevelListParent);
            TextMeshProUGUI text = go.GetComponentInChildren<TextMeshProUGUI>();
            Button button = GetComponent<Button>();

            text.text = levelObject.levelName;
        }
    }
}
