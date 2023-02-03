using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 3.0f;

    protected override void Eat() // overrides function 
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }
}
