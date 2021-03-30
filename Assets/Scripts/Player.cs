using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class Player : MonoBehaviour
{
    [Header("Stats")]
    public string playerName;
    public Color color;
    [SerializeField] private int score;

    public int Score { get { return score; } }
    public int oldScore { get; private set; }
    public int Index { get { return GetComponent<PlayerInput>().user.index; } } //change if it is called often
    public bool HasHybridModel { get { return hybridModel != null; } }
    private GameObject hybridModel;
    public PlayerController Controller { get; private set; }
    
    private PlayerActions _inputControls;
    public PlayerActions InputControls { get { return _inputControls; } }

    private void Start()
    {
        transform.SetParent(GameManager.Instance.transform);

        _inputControls = new PlayerActions();
        _inputControls.devices = GetComponent<PlayerInput>().devices;

        GameManager.AddPlayer(this);
    }

    public void SetController(PlayerController playerController)
    {
        Controller = playerController;
    }

    public void SaveHybridModel(GameObject hybridGameObject)
    {
        hybridModel = Instantiate(hybridGameObject, transform);
        hybridModel.transform.localPosition = Vector3.zero;
        hybridModel.SetActive(false);
    }
    public HybridModel GetHybridModelCopy(Transform parent)
    {
        if (hybridModel == null) return null;
        return Instantiate(hybridModel, parent).GetComponent<HybridModel>();
    }

    public void Disconnect()
    {
        GameManager.RemovePlayer(this);
        StartCoroutine(DelayedDestruction());
    }

    public void AddScore(int score)
    {
        oldScore = this.score;
        this.score += score;
    }

    private IEnumerator DelayedDestruction()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

}
