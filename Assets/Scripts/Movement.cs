using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RigidbodyConstraints2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;

    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstaclelayer;
    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        ResetState();
    }

    
    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody.isKinematic = false;
        this.enabled = true;
    }

    private void Update()
    {
        // Try to move in the next direction while it's queued to make movements
        // more responsive
        if (this.nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier;
        this.rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        //setting the direction based on occupied bool, if it isnt occupied- set direction to direction and next direction to 0,0
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        //if the direction is occupied, set the next direction to direction 
        else
        {
            this.nextDirection = direction;
        }
    }

    //bool returns true if a space direction is occupied, but returns false if not 
    public bool Occupied(Vector2 direction)
    {
        // raycast checks if we can turn that direction information is in order of - where the box starts being drawn, the size of the box, the angle, the direction we want to move in- so that we are 1 movement over - plus the center of the object, finally check which layer you're raycasting 
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstaclelayer);
        return hit.collider != null;
    }
}

