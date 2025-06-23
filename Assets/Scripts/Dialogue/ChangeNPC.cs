using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNPC : MonoBehaviour
{
    public NPC npc;
    private void Start()
    {
    }

    public void ChangeNPCData()
    {

        DialogController.controller.currentNPC = npc;
        DialogController.controller.StartDialog(DialogController.controller.currentNPC);
    }
}
