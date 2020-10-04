using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDir;

    [SerializeField]
    float speed;
    [SerializeField]
    float rotateSpeed;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {

            moveDir.x = Input.GetAxis("Horizontal");
        }

        if (Input.GetAxisRaw("Vertical") != 0)
        {

            moveDir.z = Input.GetAxis("Vertical");
        }

        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * rotateSpeed);
        }

    }

    private void FixedUpdate()
    {

        rb.velocity = moveDir.normalized * speed * Time.fixedDeltaTime;
        moveDir = Vector3.zero;
    }


}
