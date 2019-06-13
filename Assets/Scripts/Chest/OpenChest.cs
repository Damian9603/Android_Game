using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : Interactable
{
    public Item[] items;
    private Animator openChest;

    private void Start()
    {
        openChest = GetComponent<Animator>();
    }

    public override void Interact()
    {
        openChest.SetTrigger("OpenChest");
    }
}
