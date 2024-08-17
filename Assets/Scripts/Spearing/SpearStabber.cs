using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearStabber : MonoBehaviour
{
    private Animator animator;
    private SpearMinigameManager manager;
    [SerializeField] private Transform spearTip;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<SpearMinigameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }
    public void SpearStab()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 4f, 2048))
        {
            manager.StartMinigame();
            Transform objectHit = hit.transform;
            objectHit.GetComponent<FishMovement>().enabled = false;
            objectHit.SetParent(spearTip);
            objectHit.localPosition = Vector3.zero;
        }
    }
}
