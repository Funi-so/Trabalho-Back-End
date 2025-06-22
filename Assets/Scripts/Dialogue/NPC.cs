using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    //public GameObject myExclamation;
    [SerializeField]
    public NPCSO myData;
    public bool canTalk;

    [Header("Events")]
    public int2[] EventIndexes;
    public UnityEvent[] events;
    private void Start()
    {
        DialogController.controller.myExclamation.SetActive(false);
        canTalk = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTalk)
        {
            if (!DialogController.controller.isTalking)
            {
                DialogController.controller.myExclamation.SetActive(false);
                DialogController.controller.StartDialog(this);
            }
            else
            {
                int dialogIndex = DialogController.controller.dialogIndex;
                if (!(myData.dialog[dialogIndex].answers.Length > 0))
                    DialogController.controller.SkipDialog();
            }
        }

        if (canTalk && !DialogController.controller.isTalking)
        {
            DialogController.controller.myExclamation.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogController.controller.myExclamation.SetActive(true);
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogController.controller.myExclamation.SetActive(false);
            canTalk = false;
        }
    }

    public void CheckEvent(int index)
    {
        foreach(int2 i in EventIndexes)
        {
            if (index == i.x) {  
                events[i.y].Invoke();    
            }
        }
    }
}
