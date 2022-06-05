using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public float speed = 3f;
    public Transform pathParent;
    Transform targetPoint;
    int index;
    public Camera camera1;
   
    private int count;
    public Transform endPoint;
    public static PathFollower instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnDrawGizmos()
    {
        Vector3 from;
        Vector3 to;
        for (count = 0; count < pathParent.childCount; count++)
        {
           
            from = pathParent.GetChild(count).position;
            to = pathParent.GetChild((count + 1) % pathParent.childCount).position;
            Gizmos.color = new Color(1, 0, 0);
            Gizmos.DrawLine(from, to);
  
            
        }
    }

    private void Start()
    {
        index = 0;
        targetPoint = pathParent.GetChild(index);
    }

    private void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            index++;
            index %= pathParent.childCount;
            targetPoint = pathParent.GetChild(index);
        }

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, targetPoint.transform.rotation.eulerAngles.x, Time.deltaTime * speed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, targetPoint.transform.rotation.eulerAngles.y, Time.deltaTime * speed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetPoint.transform.rotation.eulerAngles.z, Time.deltaTime * speed));

        transform.eulerAngles = currentAngle;
        ChangeCamera();
        

    }

    public void ChangeCamera()
    {
        if (Vector3.Distance(transform.position, endPoint.position) < .8f)
        {
            gameObject.SetActive(false);
            AudioManager.instance.StartMusic();
        }
    }

    public void cancelCamera()
    {
        gameObject.SetActive(false);
        AudioManager.instance.StartMusic();
    }

}
