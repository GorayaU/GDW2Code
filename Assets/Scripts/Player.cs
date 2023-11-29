using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float SpeedFactor = 5;
    [SerializeField] private float JumpForce;
    [SerializeField] private LayerMask GroundLayers;
    [SerializeField] private Boss Boss;

    private Rigidbody2D Rb;
    private Collider2D MyCollider;
    private float Depth;
    private bool IsGrounded;
    private Vector3 NewPos;
    private bool inFight;
    private Vector3 moveDir;

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
        if (!inFight)
        {
            gameObject.transform.position += NewPos;
        }
        transform.position += transform.rotation * (SpeedFactor * Time.deltaTime * moveDir);
        CheckGrounded();
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            Rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    public void Fly(Vector3 newDirection)
    {
        moveDir = newDirection;
    }

    private void CheckGrounded()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector3.down, Depth, GroundLayers);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inFight = !inFight;
        Boss.inBossFight = !Boss.inBossFight;
        ImputManager.inFight = !ImputManager.inFight;
    }
}
