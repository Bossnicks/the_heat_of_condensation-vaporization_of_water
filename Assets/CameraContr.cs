using UnityEngine;

public class CameraContr : MonoBehaviour
{
    bool move = false;
    float speed = 0.01f;
    float offset = 0;
    Vector3 startPosition;
    Vector3 needPosition;
    Quaternion startRotation;
    Quaternion needRotaton;
    private void Start()
    {
        transform.position = new Vector3(-1291.8f, 11.3f, -76.23f);
        transform.rotation = Quaternion.Euler(10f, 270f, 0f);
    }

    public void MoveToStart()
    {
        move = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        needPosition = new Vector3(-1291.8f, 11.3f, -76.23f);
        needRotaton = Quaternion.Euler(10f, 270f, 0f);
    }
    public void MoveKalorimeter()
    {
        move = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        needPosition = new Vector3(-1297.89f, 10.88f, -77.75f);
        needRotaton = Quaternion.Euler(20.455f, 277.277f, 1.073f);
    }
    public void MoveTAN()
    {
        move = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        needPosition = new Vector3(-1296.99f, 12.21f, -80.25f);
        needRotaton = Quaternion.Euler(10f, 290f, 0f);

    }
    public void MoveParogenerator()
    {
        move = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        needPosition = new Vector3(-1298f, 12.21f, -77.75f);
        needRotaton = Quaternion.Euler(18.902f, 275.479f, 0.863f);
    }

    public void MoveRotameter()
    {
        move = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        needPosition = new Vector3(-1297.89f, 9.51f, -75.98f);
        needRotaton = Quaternion.Euler(20.455f, 261.288f, 1.073f);
    }

    public void MoveStakan()
    {
        move = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        needPosition = new Vector3(-1297.89f, 9.51f, -78.75f);
        needRotaton = Quaternion.Euler(27.44f, 298.915f, 3.225f);
    }

    public void MoveNapornyBak()
    {
        move = true;
        startPosition = transform.position;
        startRotation = transform.rotation;
        needPosition = new Vector3(-1297.89f, 13.61f, -73.9f);
        needRotaton = Quaternion.Euler(27.44f, 253.076f, 3.225f);
    }

    void Update()
    {
        if (move)
        {
            offset += speed;
            transform.position = Vector3.Lerp(startPosition, needPosition, offset);
            transform.rotation = Quaternion.Slerp(startRotation, needRotaton, offset);
            if (offset >= 1)
            {
                move = false;
                offset = 0;
            }
        }
    }
}