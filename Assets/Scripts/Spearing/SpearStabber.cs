using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearStabber : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    // Update is called once per frame
    public void SpearStab()
    {
        Debug.Log("pew pew");
    }

}
