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

        Debug.Log("stab stab");
        if (Physics.Raycast(ray, out hit, 4f, 2048))
        {
            Debug.Log("i hit a fish");
            manager.StartMinigame(hit.transform, spearTip);          
        }
        else if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("i hit som,ething ");
            Debug.Log(hit.transform.name);
        }
    }
}
