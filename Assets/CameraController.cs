using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform targetPos;
    [SerializeField]
    int sensivity = 3;
    [SerializeField]
    float scrollSpeed = 10f;
    [SerializeField]
    int maxdistance = 9;
    [SerializeField]
    int mindistance = 5;
    //[SerializeField]
    //int maxdistanceForScroll = 200;
    //[SerializeField]
    //int mindistanceForScroll = 9;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float y = Input.GetAxis("Mouse X") * sensivity;
            if (y != 0)

            { transform.RotateAround(targetPos.position, Vector3.up, y); }

        }
        float x = Input.GetAxis("Horizontal") / sensivity;     // êëàâèøè A, D
        if (x != 0)
        {
            Vector3 newpos = transform.position + transform.TransformDirection(new Vector3(x, 0, 0));

            if (ControlDistance(Vector3.Distance(newpos, targetPos.position))) transform.position = newpos;
        }

        // ÏÐÈÁËÈÆÅÍÈÅ È ÓÄÀËÅÍÈÅ ÊÀÌÅÐÛ Ê ÓÑÒÀÍÎÂÊÅ ÍÀ ÑÖÅÍÅ ÏÐÎÊÐÓÒÊÎÉ ÊÎËÅÑÀ ÌÛØÈ

        float z = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        if (z != 0)
        {
            Vector3 newpos = transform.position + transform.TransformDirection(Vector3.forward * z);
            if (ControlDistance(Vector3.Distance(newpos, targetPos.position))) transform.position = newpos;
        }


    }

    bool ControlDistance(float distance)
    {
        if (distance > mindistance && distance < maxdistance) return true;
        return false;
    }
    //bool ControlDistanceForScroll(float distance)
    //{
    //    if (distance > mindistanceForScroll && distance < maxdistanceForScroll) return true;
    //    return false;
    //}

}
