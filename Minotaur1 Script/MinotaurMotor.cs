using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurMotor : MonoBehaviour
{
    //MinotaurMotor
    [SerializeField] private float movement;
    public bool isFacingRight = true;
    [SerializeField] private const float MinotaurSpeed = 2.5f;
    public Rigidbody2D myRigidBody;

    private void FixedUpdate()
    {
        myRigidBody.velocity = new Vector2(MinotaurSpeed * movement, myRigidBody.velocity.y);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }
}
