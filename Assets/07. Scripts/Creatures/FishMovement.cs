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
    private GroundWaterManager waterManager;

    private enum BehaviourMode { neutral, aggresive, scared, faceFish, coralFish };
    private enum FishName { normal, babarusa };

    [Header("Identity Atrributes")]
    [SerializeField] FishName fishName = FishName.normal;
    [SerializeField] BehaviourMode behaviour = BehaviourMode.neutral;
    [SerializeField] float reactionRadius = 2f;
    [SerializeField] private FishHome home;
    [SerializeField] public Sprite image;

    [Header("Swiming Attributes")]
    [SerializeField] float minSwimDistance = 5f;
    [SerializeField] float maxSwimDistance = 10f;
    [SerializeField] float minSwimSpeed = 0.5f;
    [SerializeField] float maxSwimSpeed = 5f;
    [SerializeField] float swimDeceleration = 1f;
    [SerializeField] float maxHeight = -2f;
    

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
        waterManager = FindObjectOfType<GroundWaterManager>();

        CalculateNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if player is close enough to fish
        if (Vector3.Distance(transform.position, player.transform.position) <= reactionRadius)
        {
            // Checks player is in the water
            if(waterManager.inWater)
            {
                ReactToPlayer();
            }
            
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

            //Targets a specific area
            if(home != null)
            {
                targetPos = new Vector3(
                    Mathf.Clamp(transform.position.x + xTarg, home.transform.position.x - home.radius, home.transform.position.x + home.radius),
                    Mathf.Clamp(transform.position.y + yTarg, home.transform.position.y - home.radius, home.transform.position.y + home.radius),
                    Mathf.Clamp(transform.position.z + zTarg, home.transform.position.z - home.radius, home.transform.position.z + home.radius));
            }
            else
            {
                //Creates the target position from the target direction
                targetPos = new Vector3(
                    transform.position.x + xTarg, 
                    Mathf.Clamp(transform.position.y + yTarg, -999, maxHeight), 
                    transform.position.z + zTarg);
            }


            //Makes the fish look towards the target
            transform.LookAt(targetPos);


            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Vector3.Distance(transform.position, targetPos), 65536))
            {
                CalculateNewDirection();
            }
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

    private void CalculateNewDirection(Vector3 position, float speed)
    {
        swimSpeed = speed;

        targetPos = position;

        transform.LookAt(player);
    }

    //Called by FishHookDetection, 
    public void ISeeRod(Vector3 inputHookPos)
    {
        iSeeRod = true;
        hookPos = inputHookPos;
    }

    public void IDontSeeRod()
    {
        if(iSeeRod)
        {
            iSeeRod = false;
            CalculateNewDirection();
        }
        else
        {
            iSeeRod = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            //Runs when the fish goes into the hook 
            case "hook":
                FishHookDetection[] fishies = FindObjectsOfType<FishHookDetection>();
                collision.gameObject.GetComponent<Buoyancy>().enabled = false;
                foreach (var fish in fishies)
                {
                    fish.BuoyOutWater();
                    fish.GetComponentInParent<BoxCollider>().enabled = false;
                    fish.GetComponentInParent<FishMovement>().IDontSeeRod();
                    fish.GetComponentInParent<FishMovement>().CalculateNewDirection();
                }
                collision.gameObject.transform.SetParent(transform);         
                FindObjectOfType<FishRodMinigameManager>().StartMinigame(fishIconMoveSpeed, fishIconChanceOfChangingDirection, this);
                break;

            case "Player":
                if(behaviour == BehaviourMode.aggresive)
                {
                    FindObjectOfType<Health>().ChangeHealth(-34);
                    FindObjectOfType<FishBiteScare>().Scare();
                    Destroy(gameObject);
                }
                else
                {
                    CalculateNewDirection();
                }
                
                break;
            case "terrain":
                CalculateNewDirection();
                Debug.Log("ouch");
                break;

            default:
                Debug.Log("i hit my  head");
                CalculateNewDirection();
                break;
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
            case BehaviourMode.faceFish:
                //Checks player is looking at faceFish
                //RaycastHit hit;
                //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, 2048))
                //{
                //    Debug.Log("raycasting");
                //    LookedAt();
                //}
                LookedAt();
                    break;
            case BehaviourMode.coralFish:
                //Sees which coral is closer
                GameObject[] coral = GameObject.FindGameObjectsWithTag("hidingSpot");
                float distance = Vector3.Distance(transform.position, coral[0].transform.position);
                GameObject selectedCoral = coral[0];
                foreach (var coralItem in coral)
                {
                    float tempDis = Vector3.Distance(transform.position, coralItem.transform.position);
                    if (tempDis < distance)
                    {
                        distance = tempDis;
                        selectedCoral = coralItem;
                    }
                }

                //Move away from player
                CalculateNewDirection(selectedCoral.transform.position);
                break;
            default:
                break;
        }
    }

    //Runs when the player is looking at the fish, used for face fish behaviour
    public void LookedAt()
    {
        Vector3 movementDirection = transform.position - player.transform.position;
        movementDirection = Vector3.Normalize(movementDirection);
        CalculateNewDirection(transform.position + movementDirection, minSwimSpeed);

    }

    public int GetFishIndex()
    {
        return (int)fishName;
    }
}
