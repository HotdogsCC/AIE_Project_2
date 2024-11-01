using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSway : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 1f;
    [SerializeField] private float step = 0.01f;
    [SerializeField] private float maxStepDistance = 0.06f;
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobStrength = 0.25f;

    private StarterAssetsInputs input;
    Vector3 swayPos;
    Vector3 bobPos; 
    // Start is called before the first frame update
    void Start()
    {
        input = FindObjectOfType<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        Sway();
        SwayRotation();
        BobOffset();
        BobRotation();

        transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos + bobPos, Time.deltaTime * lerpSpeed);
    }

    private void Sway()
    {
        Vector3 invertLook = input.look * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

        swayPos = invertLook;
    }

    private void SwayRotation()
    {

    }

    private void BobOffset()
    {
        bobPos = new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad * bobSpeed) * bobStrength * input.move.magnitude, 0);
    }

    private void BobRotation()
    {

    }
}
