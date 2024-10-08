using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Ladder : Interactable
{
    private bool onLadder = false;
    private Transform player;

    [SerializeField] private float climbSpeed = 1f;
    [SerializeField] private Transform topOfLadder;
    [SerializeField] private Transform bottomOfLadder;
    [SerializeField] private Transform offLadder;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>().transform;
    }

    public override void Interact()
    {
        if(onLadder)
        {
            GetOffLadder();
        }
        else
        {
            GetOnLadder();
        }
    }

    private void Update()
    {
        if (onLadder)
        {
            if(Input.GetKey(KeyCode.W))
            {
                player.position = new Vector3(player.position.x, player.position.y + (Time.deltaTime * climbSpeed), player.position.z);
            }
            if(Input.GetKey(KeyCode.S))
            {
                player.position = new Vector3(player.position.x, player.position.y - (Time.deltaTime * climbSpeed), player.position.z);
            }

            if(player.position.y >= topOfLadder.position.y)
            {
                GetOffLadder();
            }
            if(player.position.y < bottomOfLadder.position.y)
            {
                player.position = new Vector3(player.position.x, bottomOfLadder.position.y, player.position.z);
            }
        }
    }

    private void GetOnLadder()
    {
        onLadder = true;
        Pausing.FreezePlayer();
        player.position = bottomOfLadder.position;
        player.localEulerAngles = new Vector3(0, 52f, 0);
    }

    private void GetOffLadder()
    {
        onLadder = false;
        player.position = offLadder.position;
        Physics.SyncTransforms();
        Pausing.ResumePlayer();
    }
}
