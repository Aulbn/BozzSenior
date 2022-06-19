using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelObject")]
public class LevelObject : ScriptableObject
{
    [System.Flags]
    public enum Category
    {
        Pvp = (1 << 0), 
        TimeTrial = (1 << 1), 
        Race = (1 << 2)
    }

    [SerializeField] private string _sceneName;
    [Header("Level Info")]
    [SerializeField] private string _levelName;
    [Multiline] private string _description;
    [SerializeField][Range(0, 5)] private int _difficulty;
    [EnumFlag] [SerializeField] private Category _categories;
    [SerializeField] private Vector2 _EstimatedPlaytime;
    [SerializeField] private Sprite _picture;

    public string sceneName { get { return _sceneName; } }
    public string levelName { get { return _levelName; } }
    public string description { get { return _description; } }
    public int difficulty { get { return _difficulty; } }
    public Category categories { get { return _categories; } }
    public Sprite picture { get { return _picture; } }
    public float MinTime { get { return _EstimatedPlaytime.x; } }
    public float MaxTime { get { return _EstimatedPlaytime.y; } }

    public string GetCategoryName()
    {
        switch (_categories)
        {
            case Category.Pvp:
                return "PvP";
            default:
                return _categories.ToString();
        }
    }

    public IEnumerable<Category> GetFlags() //Ska gå att iterera igenom via t.ex. foreach: https://www.youtube.com/watch?v=F7L9seU_mak
    {
        foreach (Category value in Enum.GetValues(_categories.GetType()))
            if (_categories.HasFlag(value))
                yield return value;
    }
}
