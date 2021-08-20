using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TotemPole : PlayerController
{
    public Transform TotemPoleParent;
    public GameObject[] TotemPrefabs;

    private Queue<int> QueuedInputs = new Queue<int>();
    private Queue<Totem> QueuedTotems = new Queue<Totem>();

    private Vector3 _TotemPoleTargetPosition;

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
        CreateTotemQueue(25);
        FindObjectOfType<GameBrain_TotemPoles>().AddTotemPole(this);
        _TotemPoleTargetPosition = TotemPoleParent.position;
    }

    private void Update()
    {
        TotemPoleParent.position = Vector3.Lerp(TotemPoleParent.position, _TotemPoleTargetPosition, Time.deltaTime * 5f);
    }

    private IEnumerator IEUpdate(float cooldownTime, float stunTime)
    {
        bool hasScored = false;

        while(QueuedTotems.Count > 0)
        {
            if (hasScored)
            {
                yield return new WaitForSeconds(cooldownTime);
                hasScored = false;
            }
            if (QueuedInputs.Count > 0)
            {
                if (QueuedInputs.Dequeue() == QueuedTotems.Peek().Index)
                {
                    //Same! Remove totem
                    Debug.Log("Correct!");
                    hasScored = true;
                    Destroy(QueuedTotems.Dequeue().gameObject);
                    //Move down other totems
                    _TotemPoleTargetPosition += Vector3.down * .52f;
                }
                else
                {
                    //Not same! Stun!
                    Debug.Log("Stun!");
                    QueuedInputs.Clear();
                    AddInputLock();
                    yield return new WaitForSeconds(stunTime);
                    RemoveInputLock();
                }
            }
            yield return new WaitForEndOfFrame();
        }
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
            randomInput = Random.Range(0, 3);
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
