using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float distanceH = 9f;
    public float distanceV = 3f;
    public float smoothSpeed = 10f; //平滑参数

    void Start()
    {

    }

    void Update()
    {
        if (distanceH < 2)
        {
            distanceH = 2;
            return;
        }

        if (distanceH > 8)
        {
            distanceH = 8;
            return;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            distanceH = distanceH + 10 * Time.deltaTime;
        }
        //Zoom in  
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            distanceH = distanceH - 10 * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        Vector3 nextpos = target.forward * -1 * distanceH + target.up * distanceV + target.position;

        this.transform.position = Vector3.Lerp(this.transform.position, nextpos, smoothSpeed * Time.deltaTime); //平滑插值

        this.transform.LookAt(target);
    }
}