using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float speed;

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    void Update()
    {
        transform.Translate((Vector2.down * speed) * Time.deltaTime);
    }
}
