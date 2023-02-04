using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ RequireComponent(typeof(Movement))]

public class Pacman : MonoBehaviour
{

    public AudioSource deathSound;
    public AnimatedSprite animatedSprite; 
    public SpriteRenderer spriteRenderer { get; private set; }
    public CircleCollider2D collider { get; private set; }
    public Movement movement { get; private set; }
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponent<Movement>();
        this.collider = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up);
        }

        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right);
        }
        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x); //converts into radians
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); //muiltiplication converts to degrees, and the vection is what point you rotate




    }

    



    public void ResetState()
    {
        animatedSprite.spriteRenderer.enabled = false;
        animatedSprite.enabled = false;
        this.spriteRenderer.enabled = true;
        //this.enabled = true;
        this.movement.enabled = true;
        this.collider.enabled = true;
        this.movement.ResetState();
        this.gameObject.SetActive(true);
        
    }

    public void DeathSequence()
    {
        deathSound.Play();
        //this.enabled = false;
        this.collider.enabled = false; 
        this.spriteRenderer.enabled = false;
        animatedSprite.spriteRenderer.enabled = true;
        animatedSprite.enabled = true;
        this.movement.enabled = false;
        animatedSprite.Restart();

        

    }


}
