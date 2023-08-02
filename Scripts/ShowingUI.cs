using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowingUI : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-14, 0, -10), speed * Time.deltaTime);
    }
}
