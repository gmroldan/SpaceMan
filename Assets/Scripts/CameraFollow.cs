using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f);
    public float dampingTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    void Awake() 
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth)
    {
        var destination = this.GetCameraDestination();

        if (smooth)
        {
            this.transform.position = this.GetSmoothVector(destination);
        }
        else
        {
            this.transform.position = destination;
        }
    }

    Vector3 GetCameraDestination()
    {
        return new Vector3(
            this.target.position.x - this.offset.x,
            this.offset.y,
            this.offset.z);
    }

    Vector3 GetSmoothVector(Vector3 destination)
    {
        return Vector3.SmoothDamp(
                this.transform.position,
                destination,
                ref velocity,
                this.dampingTime
            );
    }
}
