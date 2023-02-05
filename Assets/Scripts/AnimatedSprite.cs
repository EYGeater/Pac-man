using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.125f;
    public int animationFrame { get; private set; }
    public bool loop = true;


    //get sprite renderer component
    public void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //do the funcution advanced every animationTime second, for every animationTime second 
    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    //this function loops through the animation frames by sprites length 
    private void Advance()
    {
        //stop animating if sprite renderer isnt enabled 
        if (!spriteRenderer.enabled)
        {
            return;
        }
        this.animationFrame++;
        //this if statement does the loop if the frame is the length of the sprites
        if (this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }
        //this if statement changes each sprite 
        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }

    }
    //if you ever want to go restart the animation seperately 
    public void Restart()
    {
        this.animationFrame = -1;
        Advance();
    }
    public void NoLoop()
    {
        loop = false;
        Advance();
    }

}
