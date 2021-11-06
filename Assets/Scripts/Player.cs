using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float move_speed;
    [SerializeField] float jump_height;

    private Animator anim;
    private Rigidbody body;

    private float horizontal;
    private float vertical;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (vertical + horizontal != 0)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);

        // Move Forward/Backward
        if (vertical != 0)
        {
            transform.position += vertical * transform.forward * Time.deltaTime * move_speed;
        }

        // Move Left/Right
        if (horizontal != 0)
        {
            transform.position += horizontal * transform.right * Time.deltaTime * move_speed;
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            body.AddForce(transform.up * jump_height, ForceMode.Impulse);
            anim.SetBool("Jump", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("Jump", false);
    }
}
