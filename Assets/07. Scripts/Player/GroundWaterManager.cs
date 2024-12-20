using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundWaterManager : MonoBehaviour
{
    [Header("CHECK ME!!")]
    [SerializeField] private bool waterYLevelDetectMode = false;

    [Header("Water Y Level")]
    [SerializeField] private float waterYLevel;

    [Header("Important Stuff")]
    [SerializeField] public bool inWater = false;
    private bool overlap = false;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private GameObject fishingRod;
    [SerializeField] private GameObject spear;
    [SerializeField] private GameObject walkSFX;
    [SerializeField] private GameObject fishingLine;
    private bool moving = false;

    [Header("Ground Variables")]
    [Space(10)]
    [Tooltip("Move speed of the character in m/s")]
    public float groundMoveSpeed = 4.0f;
    [Tooltip("Sprint speed of the character in m/s")]
    public float groundSprintSpeed = 6.0f;
    [Tooltip("Rotation speed of the character")]
    public float groundRotationSpeed = 1.0f;
    [Tooltip("Acceleration and deceleration")]
    public float groundSpeedChangeRate = 10.0f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float groundJumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float groundGravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float groundJumpTimeout = 0.1f;
    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float groundFallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool groundGrounded = true;
    [Tooltip("Useful for rough ground")]
    public float groundGroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float groundGroundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask groundGroundLayers;
    public float groundTerminalVelocity = 53.0f;
    [Space(20)]

    [Header("Water Variables")]
    [Space(10)]
    [Tooltip("Move speed of the character in m/s")]
    public float waterMoveSpeed = 2.0f;
    [Tooltip("Sprint speed of the character in m/s")]
    public float waterSprintSpeed = 3.0f;
    [Tooltip("Rotation speed of the character")]
    public float waterRotationSpeed = 1.0f;
    [Tooltip("Acceleration and deceleration")]
    public float waterSpeedChangeRate = 5.0f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float waterJumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float waterGravity = -5.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float waterJumpTimeout = 0.1f;
    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float waterFallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool waterGrounded = true;
    [Tooltip("Useful for rough ground")]
    public float waterGroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float waterGroundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask waterGroundLayers;

    public float waterTerminalVel = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateGroundWaterMovementVars();
        StartCoroutine(TryPlaySound());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!waterYLevelDetectMode)
        {
            if (other.tag == "water")
            {
                if (inWater) //If you enter water and are still in water, this is an overlapping area
                {
                    overlap = true;
                }
                else
                {
                    inWater = true;
                    UpdateGroundWaterMovementVars();
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(!waterYLevelDetectMode)
        {
            if (other.tag == "water")
            {
                if (overlap) //this is to stop issues of exiting water with overlapping water triggers
                {
                    overlap = false;
                }
                else
                {
                    inWater = false;
                    UpdateGroundWaterMovementVars();
                }
            }
        }
    }


    private void Update()
    {
        if(waterYLevelDetectMode)
        {
            if (firstPersonController.transform.position.y < waterYLevel)
            {
                if (!inWater)
                {
                    inWater = true;
                    UpdateGroundWaterMovementVars();
                }
            }
            else
            {
                if (inWater)
                {
                    inWater = false;
                    UpdateGroundWaterMovementVars();
                }
            }
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    private IEnumerator TryPlaySound()
    {
        yield return new WaitForSeconds(0.5f);
        if(moving && !inWater)
        {
            Instantiate(walkSFX);
        }
        StartCoroutine(TryPlaySound());
    }

    private void UpdateGroundWaterMovementVars()
    {
        //sets stuff like speed and stuff based on whether player is walking or swiming
        if (inWater)
        {
            RenderSettings.fog = true;
            firstPersonController.MoveSpeed = waterMoveSpeed;
            firstPersonController.SprintSpeed = waterSprintSpeed;
            firstPersonController.RotationSpeed = waterRotationSpeed;
            firstPersonController.SpeedChangeRate = waterSpeedChangeRate;
            firstPersonController.JumpHeight = waterJumpHeight;
            firstPersonController.Gravity = waterGravity;
            firstPersonController.JumpTimeout = waterJumpTimeout;
            firstPersonController.GroundedOffset = waterGroundedOffset;
            firstPersonController.GroundedRadius = waterGroundedRadius;
            //firstPersonController.GroundLayers = waterGroundLayers;
            firstPersonController._terminalVelocity = waterTerminalVel;
            FishHookDetection[] fishies = FindObjectsOfType<FishHookDetection>();
            foreach (var fish in fishies)
            {
                //fish.GetComponent<SphereCollider>().enabled = true;
                fish.GetComponentInParent<BoxCollider>().enabled = true;
            }
            spear.SetActive(true);
            fishingRod.GetComponent<RodFishCaster>().ReelLine();
            fishingRod.GetComponent<RodFishCaster>().SetCast();
            fishingRod.SetActive(false);
            fishingLine.SetActive(false);
        }
        else
        {
            RenderSettings.fog = false;
            firstPersonController.MoveSpeed = groundMoveSpeed;
            firstPersonController.SprintSpeed = groundSprintSpeed;
            firstPersonController.RotationSpeed = groundRotationSpeed;
            firstPersonController.SpeedChangeRate = groundSpeedChangeRate;
            firstPersonController.JumpHeight = groundJumpHeight;
            firstPersonController.Gravity = groundGravity;
            firstPersonController.JumpTimeout = groundJumpTimeout;
            firstPersonController.GroundedOffset = groundGroundedOffset;
            firstPersonController.GroundedRadius = groundGroundedRadius;
            //firstPersonController.GroundLayers = groundGroundLayers;
            firstPersonController._terminalVelocity = groundTerminalVelocity;
            FishHookDetection[] fishies = FindObjectsOfType<FishHookDetection>();
            foreach (var fish in fishies)
            {
                fish.GetComponent<SphereCollider>().enabled = false;
                fish.GetComponentInParent<FishMovement>().IDontSeeRod();
            }
            spear.SetActive(false);
            fishingRod.SetActive(true);
            fishingLine.SetActive(true);
        }
        
    }

    public float GetYLevel()
    {
        return waterYLevel;
    }


    public bool GetDetectMode()
    {
        return waterYLevelDetectMode;
    }
}
