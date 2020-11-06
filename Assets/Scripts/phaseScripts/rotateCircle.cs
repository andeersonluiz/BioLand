using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCircle : MonoBehaviour
{
    private float speed=5f;
    private float angle=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle+=speed*Time.deltaTime;
        this.GetComponent<RectTransform>().rotation =  Quaternion.EulerAngles(0f,0f,angle);
    
    }
}
