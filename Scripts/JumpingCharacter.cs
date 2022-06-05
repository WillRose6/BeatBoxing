using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingCharacter : MonoBehaviour
{
    
    private float movePos; 

    void Start()
    {
        movePos = gameObject.transform.position.y;
        StartCoroutine(StartJump());
        

        
    }

    IEnumerator StartJump()
    {
        
        gameObject.transform.Translate(new Vector3(transform.position.x, movePos + 2.0f, transform.position.z));
        yield return new WaitForSeconds(1f);
        gameObject.transform.Translate(new Vector3(transform.position.x, movePos - 2.0f, transform.position.z));

   
    }
}
