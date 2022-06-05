using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] views;
    public float transistionSpeed;
    private Transform currentview;
    public Transform[] randomViews;

    public static CameraController instance;



    // Start is called before the first frame update
    void Start()
    {

        currentview = views[0];
        StartCoroutine(IntroWait());

    }
    private void Awake()
    {
        instance = this;
    }

    //private IEnumerator CameraWait()
    //{
    //    yield return new WaitForSeconds(17.0f); //set this to 75f before we submit it please cause that'll be after a min
    //    currentview = views[2];
    //}

    // Update is called once per frame
    //void LateUpdate()
    //{
    //    transform.position = Vector3.Lerp(transform.position, currentview.position, Time.deltaTime * transistionSpeed);

    //    Vector3 currentAngle = new Vector3(
    //        Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentview.transform.rotation.eulerAngles.x, Time.deltaTime * transistionSpeed),
    //        Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentview.transform.rotation.eulerAngles.y, Time.deltaTime * transistionSpeed),
    //        Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentview.transform.rotation.eulerAngles.z, Time.deltaTime * transistionSpeed));

    //    transform.eulerAngles = currentAngle;
    //    //PlayerManager.instance.Players[0].GetScore();
    //}
    private IEnumerator IntroWait()
    {
        yield return new WaitForSeconds(18.5f); //DO NOT CHANGE IF YOU DO BACK TO 18.5
        PlayerOneView();
    }

    public void PlayerOneView()
    {
        currentview = views[1];
        transform.position = currentview.position;

        Quaternion setAngle = Quaternion.Euler(currentview.transform.rotation.eulerAngles.x, currentview.transform.rotation.eulerAngles.y, currentview.transform.rotation.eulerAngles.z);
        transform.rotation = setAngle;

        //Vector3 currentAngle = new Vector3(
        //    Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentview.transform.rotation.eulerAngles.x, Time.deltaTime * transistionSpeed),
        //    Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentview.transform.rotation.eulerAngles.y, Time.deltaTime * transistionSpeed),
        //    Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentview.transform.rotation.eulerAngles.z, Time.deltaTime * transistionSpeed));

        //transform.eulerAngles = currentAngle;
    }

    public void PlayerTwoView()
    {
   
        currentview = views[2];

        transform.position = currentview.position;

        Quaternion setAngle = Quaternion.Euler(currentview.transform.rotation.eulerAngles.x, currentview.transform.rotation.eulerAngles.y, currentview.transform.rotation.eulerAngles.z);
        transform.rotation = setAngle;

        //transform.position = Vector3.Lerp(transform.position, currentview.position, Time.deltaTime * transistionSpeed);

        //Vector3 currentAngle = new Vector3(
        //    Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentview.transform.rotation.eulerAngles.x, Time.deltaTime * transistionSpeed),
        //    Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentview.transform.rotation.eulerAngles.y, Time.deltaTime * transistionSpeed),
        //    Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentview.transform.rotation.eulerAngles.z, Time.deltaTime * transistionSpeed));

        //transform.eulerAngles = currentAngle;
    }

    public void randomAngle()
    {
    
        int rando = Random.Range(0, 3);
        Transform randomSpot = randomViews[rando];
  
        transform.position = randomSpot.position;
        Quaternion randomAngle = Quaternion.Euler(randomSpot.transform.rotation.eulerAngles.x, randomSpot.transform.rotation.eulerAngles.y, randomSpot.transform.rotation.eulerAngles.z);
        transform.rotation = randomAngle;

    }



}
