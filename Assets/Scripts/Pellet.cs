using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10; 


    //protected means you can acees it in subclasses- like when you acess it in powerpellet- virtual will allow you to override it 

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }



}
