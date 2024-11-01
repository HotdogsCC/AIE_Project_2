using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearStabber : MonoBehaviour
{
    private Animator animator;
    private SpearMinigameManager manager;
    [SerializeField] private Transform spearTip;
    private bool animationFinished = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<SpearMinigameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && animationFinished)
        {
            animator.SetTrigger("Attack");
            animationFinished = false;
        }
    }
    public void SpearStab()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 4f, 2048))
        {
            manager.StartMinigame(hit.transform, spearTip);          
        }
    }

    public void AnimationFinished()
    {
        animationFinished = true;
    }
}
