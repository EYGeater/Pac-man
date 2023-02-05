using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehaviour

{
    // things to consider that will break: If ghosts are going back and forth, amke the ghost base prefab not taget pacman and instead have the individual insances of the ghost varient prefabs target pacman 
    // if the ghosts are running into each other, make sure in project settings you are working on physics 2d and not physics 

    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines(); 
        // safety just in case theres another coroutine going 
    }

    private void OnDisable()
    {
        //you need to check for the self being active to prevent error when it is destroyed 
        if (gameObject.activeInHierarchy)
        {
            if (this.gameObject.activeSelf)
            {
                StartCoroutine(ExitTransition());
            }
            
        }
        
        //allows you to pause execution temporarily - in order to have a sequence, first move then move to outside with animation 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true); 
        // the true forces you to go up even with the block
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;


        
        while ( elapsed < duration)
        {
            
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition; 
            //lerp- we are going to interperlate between our current position and thre transform we are working to - like box mover script 
            //this.ghost.SetPosition(Vector3.Lerp(position, this.inside.position, elapsed/duration));
            /*newPosition.z = position.z;
            
            this.ghost.transform.position = position; */
            elapsed += Time.deltaTime;
            yield return null; 
            //moves once, wait one frame then continue

        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            
            //lerp- we are going to interperlate between our current position and thre transform we are working to - like box mover script 
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            /*newPosition.z = position.z;

            this.ghost.transform.position = position;*/
            elapsed += Time.deltaTime;
            yield return null;
            
            //moves once, wait one frame then continue

        }


        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f: 1.0f, 0.0f), true); 
        // pick random direction to go
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;

    }

}
