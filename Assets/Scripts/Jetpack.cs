using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    private Animator animator;
    private bool activeJetpack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        activeJetpack = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isOnAir", activeJetpack);
    }

    public void IsOnAir(bool value)
    {
        activeJetpack = value;
    }
}
