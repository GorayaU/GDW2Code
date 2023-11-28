using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float SpeedFactor = 2;
    [SerializeField] private float JumpForce;
    [SerializeField] private LayerMask GroundLayers;

    private Rigidbody2D Rb;
    private Collider2D MyCollider;
    private float Depth;

    private bool IsGrounded;
    private Vector3 NewPos;

    void Start()
    {
        ImputManager.Init(this);
        ImputManager.GameMode();

        Rb = GetComponent<Rigidbody2D>();
        MyCollider = GetComponent<Collider2D>();
        Depth = GetComponent<Collider2D>().bounds.size.y;

        NewPos = Vector3.right/SpeedFactor;
    }
    void FixedUpdate()
    {
        //gameObject.transform.position += NewPos;
        CheckGrounded();
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            Rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckGrounded()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector3.down, Depth, GroundLayers);
    }
}
