using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPos;
    private Vector3 hookPos;
    bool iSeeRod = false;

    [Header("Swiming Attributes")]
    [SerializeField] float minSwimDistance = 5f;
    [SerializeField] float maxSwimDistance = 10f;
    [SerializeField] float minSwimSpeed = 0.5f;
    [SerializeField] float maxSwimSpeed = 5f;
    [SerializeField] float swimDeceleration = 1f;
    [SerializeField] float maxHeight = -2f;

    [Header("Fishing Attributes")]
    [SerializeField] float fishIconMoveSpeed = 25f;
    [SerializeField] int fishIconChanceOfChangingDirection = 3;
    //[Range(0f, 100f)]
    //[SerializeField] float chanceOfMovingToRod;

    float swimSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        CalculateNewDirection();
        swimSpeed = maxSwimSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPos, swimSpeed * Time.deltaTime);
        if (transform.position == targetPos)
        {
            CalculateNewDirection();
            swimSpeed = maxSwimSpeed;
        }
        else
        {
            if (!iSeeRod)
            {
                swimSpeed -= swimDeceleration * Time.deltaTime;
                swimSpeed = Mathf.Clamp(swimSpeed, minSwimSpeed, maxSwimSpeed);
            }
        }
    }

    public void CalculateNewDirection()
    {
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

            transform.rotation = Quaternion.LookRotation(new Vector3(xTarg, yTarg, zTarg));
        }
    }

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
}
