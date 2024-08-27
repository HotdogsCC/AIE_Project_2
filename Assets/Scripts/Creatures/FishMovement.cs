using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPos;
    private Vector3 hookPos;
    bool iSeeRod = false;
    private Transform player;

    private enum BehaviourMode { neutral, aggresive, scared };

    [Header("Swiming Attributes")]
    [SerializeField] float minSwimDistance = 5f;
    [SerializeField] float maxSwimDistance = 10f;
    [SerializeField] float minSwimSpeed = 0.5f;
    [SerializeField] float maxSwimSpeed = 5f;
    [SerializeField] float swimDeceleration = 1f;
    [SerializeField] float maxHeight = -2f;

    [Header("Player Behaviour Attributes")]
    [SerializeField] BehaviourMode behaviour = BehaviourMode.neutral;
    [SerializeField] float reactionRadius = 2f;

    [Header("Fishing Minigame Attributes")]
    [SerializeField] float fishIconMoveSpeed = 25f;
    [SerializeField] int fishIconChanceOfChangingDirection = 3;
    //[Range(0f, 100f)]
    //[SerializeField] float chanceOfMovingToRod;

    float swimSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Camera>().transform;

        CalculateNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if player is close enough to fish
        if (Vector3.Distance(transform.position, player.transform.position) <= reactionRadius)
        {
            ReactToPlayer();
        }

        //Continue movement towards target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, swimSpeed * Time.deltaTime);

        //Check if the fish has reached its target
        if (transform.position == targetPos)
        {
            CalculateNewDirection();
        }
        else
        {
            if (!iSeeRod)
            {
                //Slows down the fish, gives it a natural feeling deceleration thru the water
                swimSpeed -= swimDeceleration * Time.deltaTime;
                swimSpeed = Mathf.Clamp(swimSpeed, minSwimSpeed, maxSwimSpeed);
            }
        }
        
    }

    public void CalculateNewDirection()
    {
        //New direction, new speed
        swimSpeed = maxSwimSpeed;

        //If the fish sees the rod, set target position for thr rod
        if (iSeeRod)
        {
            targetPos = hookPos;
            transform.rotation = Quaternion.LookRotation(targetPos - transform.position);
        }
        else
        {
            //Randomly picks a target position from each axis
            float xTarg = Random.Range(minSwimDistance, maxSwimDistance);
            float yTarg = Random.Range(minSwimDistance, maxSwimDistance);
            float zTarg = Random.Range(minSwimDistance, maxSwimDistance);

            //Makes some negative
            if (Random.Range(0, 2) == 1)
            {
                xTarg = -xTarg;
            }
            if (Random.Range(0, 2) == 1)
            {
                yTarg = -yTarg;
            }
            if (Random.Range(0, 2) == 1)
            {
                zTarg = -zTarg;
            }

            //Creates the target position from the target direction
            targetPos = new Vector3(transform.position.x + xTarg, Mathf.Clamp(transform.position.y + yTarg, -999, maxHeight), transform.position.z + zTarg);

            //Makes the fish look towards the target
            transform.LookAt(targetPos);
        }
    }

    public void CalculateNewDirection(Vector3 position)
    {
        //New direction, new speed
        swimSpeed = maxSwimSpeed;

        //Sets the target position from the target direction
        targetPos = position;

        //Makes the fish look towards the target
        transform.LookAt(targetPos);
    }

    //Called by FishHookDetection, 
    public void ISeeRod(Vector3 inputHookPos)
    {
        iSeeRod = true;
        hookPos = inputHookPos;
    }

    public void IDontSeeRod()
    {
        iSeeRod = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Runs when the fish goes into the hook 
        if(collision.transform.tag == "hook")
        {
            FishHookDetection[] fishies = FindObjectsOfType<FishHookDetection>();
            foreach (var fish in fishies)
            {
                fish.GetComponent<SphereCollider>().enabled = false;
                fish.GetComponentInParent<BoxCollider>().enabled = false;
                fish.GetComponentInParent<FishMovement>().IDontSeeRod();
                fish.GetComponentInParent<FishMovement>().CalculateNewDirection();
            }
            collision.gameObject.transform.SetParent(transform);
            FindObjectOfType<FishRodMinigameManager>().StartMinigame(fishIconMoveSpeed, fishIconChanceOfChangingDirection);
        }
    }

    private void ReactToPlayer()
    {
        switch (behaviour)
        {
            case BehaviourMode.neutral:
                // Does nothing atm, neutral fish
                break;
            case BehaviourMode.aggresive:
                //Move towards player
                CalculateNewDirection(player.transform.position);
                break;
            case BehaviourMode.scared:
                //Calculates direction for direction away from the player
                Vector3 movementDirection = transform.position - player.transform.position;
                movementDirection = Vector3.Normalize(movementDirection);
                movementDirection *= Random.Range(minSwimDistance, maxSwimDistance);
                //Checks the fish isn't going to swim out of the water
                if(movementDirection.y >= maxHeight)
                {
                    movementDirection = new Vector3(movementDirection.x, Random.Range(minSwimDistance, maxSwimDistance)/3, movementDirection.z);
                }

                //Move away from player
                CalculateNewDirection(transform.position + movementDirection);
                break;
            default:
                break;
        }
    }
}
