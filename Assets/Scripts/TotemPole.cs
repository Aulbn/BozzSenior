using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TotemPole : PlayerController
{
    public Transform TotemPoleParent;
    public GameObject[] TotemPrefabs;
    public ParticleSystem StunFx;
    public ParticleSystem BreakFx;
    public ParticleSystem FinishFX;
    public TextMeshProUGUI ScoreText;

    [Header("Debug")]
    public Color[] IndexColors;
    public Color[] PrizeColors;

    private Queue<int> QueuedInputs = new Queue<int>();
    private Queue<Totem> QueuedTotems = new Queue<Totem>();

    private Vector3 _TotemPoleTargetPosition;
    private GameBrain_TotemPoles _GameBrain;
    private struct Totem
    {
        public int Index;
        public GameObject gameObject;

        public Totem(int index, GameObject gameObject)
        {
            Index = index;
            this.gameObject = gameObject;
        }
    }

    public void Activate(float cooldownTime, float stunTime)
    {
        StartCoroutine(IEUpdate(cooldownTime, stunTime));
    }

    private void Start()
    {
        _GameBrain = FindObjectOfType<GameBrain_TotemPoles>();
        _GameBrain.AddTotemPole(this);
        CreateTotemQueue(_GameBrain.NTotems);
        _TotemPoleTargetPosition = TotemPoleParent.position;
        ScoreText.text = "";
    }

    private void Update()
    {
        TotemPoleParent.position = Vector3.Lerp(TotemPoleParent.position, _TotemPoleTargetPosition, Time.deltaTime * 5f * (1 + QueuedInputs.Count * _GameBrain.SpeedMultiplier));
    }

    private IEnumerator IEUpdate(float cooldownTime, float stunTime)
    {
        bool hasScored = false;
        float speedMultiplier = 1;

        while(QueuedTotems.Count > 0)
        {
            speedMultiplier = 1 + QueuedInputs.Count * _GameBrain.SpeedMultiplier;
            if (hasScored)
            {
                yield return new WaitForSeconds(cooldownTime / speedMultiplier);
                hasScored = false;
            }
            if (QueuedInputs.Count > 0)
            {
                HybridModel.AnimationHandler.PlayAnimation("Headbutt");
                yield return new WaitForSeconds(.25f);
                if (QueuedInputs.Dequeue() == QueuedTotems.Peek().Index)
                {
                    //Same! Remove totem
                    Debug.Log("Correct!");
                    hasScored = true;
                    Destroy(QueuedTotems.Dequeue().gameObject);
                    BreakFx.Play();
                    _TotemPoleTargetPosition += Vector3.down * .52f; //Move down other totems
                }
                else
                {
                    //Not same! Stun!
                    //Debug.Log("Stun!");
                    QueuedInputs.Clear();
                    AddInputLock();
                    StunFx.gameObject.SetActive(true);
                    StunFx.Play();
                    HybridModel.AnimationHandler.PlayAnimation("Stunned");

                    yield return new WaitForSeconds(stunTime);

                    HybridModel.AnimationHandler.StopAnimaiton();
                    RemoveInputLock();
                    StunFx.gameObject.SetActive(false);
                }
            }
            yield return new WaitForEndOfFrame();
        }

        int score = _GameBrain.RegisterFinish();
        FinishFX.Play();
        ScoreText.text = score.ToString();
        ScoreText.color = PrizeColors[score-1];
    }

    protected override void OnSouth(InputAction.CallbackContext context)
    {
        EnqueueInput(0);
    }
    protected override void OnWest(InputAction.CallbackContext context)
    {
        EnqueueInput(1);
    }
    protected override void OnEast(InputAction.CallbackContext context)
    {
        EnqueueInput(2);
    }
    protected override void OnNorth(InputAction.CallbackContext context)
    {
        EnqueueInput(3);
    }

    public void CreateTotemQueue(int nTotems)
    {
        int randomInput;
        for (int i = 0; i < nTotems; i++)
        {
            randomInput = Random.Range(0, 4);
            GameObject totemGO = Instantiate(TotemPrefabs[randomInput], TotemPrefabs[0].transform.position + Vector3.up * i * .52f, TotemPrefabs[0].transform.rotation, TotemPoleParent);
            QueuedTotems.Enqueue(new Totem(randomInput, totemGO));
            totemGO.SetActive(true);
        }
    }

    public void EnqueueInput(int inputIndex)
    {
        QueuedInputs.Enqueue(inputIndex);
    }
}
