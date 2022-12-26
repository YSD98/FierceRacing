using System;
using UnityEngine;
using UnityEngine.UI;
public class Car : MonoBehaviour
{
    [Range(70,190)]
    public int maxSpeed = 100;
    [Range(10,120)]
    public int maxReverseSpeed = 10;
    [Range(50,700)]
    public int accelerationMultiplier = 250;
    [Range(10, 100)]
    public int stoppingForce = 20;
    [Range(25,45)]
    public int maxSteeringAngle = 30;
    [Range(300,600)]
    public int brakeForce = 300;
    //[Range(100, 500)]
    public int HandBrakeMultiplier;

    public Transform frontLeftMesh;
    public WheelCollider frontLeftCollider;
    public Transform frontRightMesh;
    public WheelCollider frontRightCollider;
    public Transform rearLeftMesh;
    public WheelCollider rearLeftCollider;
    public Transform rearRightMesh;
    public WheelCollider rearRightCollider;
    public Button resetCar;
    
   

    public enum wheelDrive
    {
        FWD,RWD,AWD
    }
    public wheelDrive wd;

    public float carSpeed;
    float throttleAxis = 1f;
    float localVelocityZ;
    Rigidbody carRigidBody;

    private void Start()
    {
        carRigidBody = gameObject.GetComponent<Rigidbody>();
        resetCar.onClick.AddListener(ResetCar);
    }
    private void Update()
    {
        carSpeed = (2 * Mathf.PI * frontLeftCollider.radius * frontLeftCollider.rpm * 60) / 1000;
        localVelocityZ = transform.InverseTransformDirection(carRigidBody.velocity).z;

        if (Input.GetKey(KeyCode.W))
        {
            GoForward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            GoReverse();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            HandBrake();
        }
        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
        if (Input.GetKey(KeyCode.R))
        {
            ResetCar();
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            DecelerateCar();
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            ResetSteeringAngle();
        }

        AnimateWheelMeshes();
    }


    public void GoForward()
    {
        if (localVelocityZ < -1f)
        {
            Brakes();
        }
        else
        {
            if (Mathf.RoundToInt(carSpeed) < maxSpeed)
            {
                switch (wd)
                {
                    case wheelDrive.FWD: //Forward Wheel Drive
                        frontLeftCollider.brakeTorque = 0;
                        frontLeftCollider.motorTorque = accelerationMultiplier * throttleAxis;
                        frontRightCollider.brakeTorque = 0;
                        frontRightCollider.motorTorque = accelerationMultiplier * throttleAxis;
                        rearLeftCollider.brakeTorque = 0;
                        
                        rearRightCollider.brakeTorque = 0;
                        
                        break;
                    case wheelDrive.RWD: //Rear Wheel Drive
                        frontLeftCollider.brakeTorque = 0;
                        
                        frontRightCollider.brakeTorque = 0;
                        
                        rearLeftCollider.brakeTorque = 0;
                        rearLeftCollider.motorTorque = accelerationMultiplier * throttleAxis;
                        rearRightCollider.brakeTorque = 0;
                        rearRightCollider.motorTorque = accelerationMultiplier * throttleAxis;
                       // Debug.LogError("rearRightCollider => " + rearRightCollider.motorTorque);
                       // Debug.LogError("rearLeftCollider => " + rearLeftCollider.motorTorque);
                        break;
                    case wheelDrive.AWD: //All Wheel Drive 4X4 
                        frontLeftCollider.brakeTorque = 0;
                        frontLeftCollider.motorTorque = accelerationMultiplier * throttleAxis;
                        frontRightCollider.brakeTorque = 0;
                        frontRightCollider.motorTorque = accelerationMultiplier * throttleAxis;
                        rearLeftCollider.brakeTorque = 0;
                        rearLeftCollider.motorTorque = accelerationMultiplier * throttleAxis;
                        rearRightCollider.brakeTorque = 0;
                        rearRightCollider.motorTorque = accelerationMultiplier * throttleAxis;
                        break;
                    default:
                        Debug.LogError("Select WheelDrive");
                        break;
                }                
            }
            else
            {
                frontLeftCollider.motorTorque = 0;
                frontRightCollider.motorTorque = 0;
                rearLeftCollider.motorTorque = 0;
                rearRightCollider.motorTorque = 0;
            }
        }
    }

   public  void GoReverse()
    {

        if (localVelocityZ > 1f)
        {
            Brakes();
        }
        else
        {
            if (Mathf.Abs(Mathf.RoundToInt(carSpeed)) < maxReverseSpeed)
            {
                switch (wd)
                {
                    case wheelDrive.FWD: //Forward Wheel Drive
                        frontLeftCollider.brakeTorque = 0;
                        frontLeftCollider.motorTorque = accelerationMultiplier * -throttleAxis;
                        frontRightCollider.brakeTorque = 0;
                        frontRightCollider.motorTorque = accelerationMultiplier * -throttleAxis;

                        rearLeftCollider.brakeTorque = 0;
                        
                        rearRightCollider.brakeTorque = 0;
                        
                        break;
                    case wheelDrive.RWD: //Rear Wheel Drive
                        frontLeftCollider.brakeTorque = 0;
                        
                        frontRightCollider.brakeTorque = 0;
                        
                        rearLeftCollider.brakeTorque = 0;
                        rearLeftCollider.motorTorque = accelerationMultiplier * -throttleAxis;
                        rearRightCollider.brakeTorque = 0;
                        rearRightCollider.motorTorque = accelerationMultiplier * -throttleAxis;
                        break;
                    case wheelDrive.AWD: //All Wheel Drive 4X4 
                        frontLeftCollider.brakeTorque = 0;
                        frontLeftCollider.motorTorque = accelerationMultiplier * -throttleAxis;
                        frontRightCollider.brakeTorque = 0;
                        frontRightCollider.motorTorque = accelerationMultiplier * -throttleAxis;
                        rearLeftCollider.brakeTorque = 0;
                        rearLeftCollider.motorTorque = accelerationMultiplier * -throttleAxis;
                        rearRightCollider.brakeTorque = 0;
                        rearRightCollider.motorTorque = accelerationMultiplier * -throttleAxis;
                        break;
                    default:
                        Debug.LogError("Select WheelDrive");
                        break;
                }
            }
            else
            {
                frontLeftCollider.motorTorque = 0;
                frontRightCollider.motorTorque = 0;
                rearLeftCollider.motorTorque = 0;
                rearRightCollider.motorTorque = 0;
            }
        }
    }

    public void DecelerateCar()
    {
        
        frontLeftCollider.motorTorque = 0;
        frontLeftCollider.brakeTorque = stoppingForce;

        frontRightCollider.motorTorque = 0;
        frontRightCollider.brakeTorque = stoppingForce;

        rearLeftCollider.motorTorque = 0;
        rearLeftCollider.brakeTorque = stoppingForce;

        rearRightCollider.motorTorque = 0;
        rearRightCollider.brakeTorque = stoppingForce;
    }

    public void Brakes()
    {
        frontLeftCollider.brakeTorque = brakeForce;
        frontRightCollider.brakeTorque = brakeForce;
        rearLeftCollider.brakeTorque = brakeForce;
        rearRightCollider.brakeTorque = brakeForce;
    }

    public void HandBrake()
    {
        frontLeftCollider.motorTorque = 0;
        frontLeftCollider.brakeTorque = HandBrakeMultiplier;
        frontRightCollider.motorTorque = 0;
        frontRightCollider.brakeTorque = HandBrakeMultiplier;
        rearLeftCollider.motorTorque = 0;
        rearLeftCollider.brakeTorque = HandBrakeMultiplier;
        rearRightCollider.motorTorque = 0;
        rearRightCollider.brakeTorque = HandBrakeMultiplier;
    }

    public void ResetCar()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y + 1f,transform.position.z);
        //transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void TurnLeft()
    {
        
        frontLeftCollider.steerAngle = -maxSteeringAngle;
        frontRightCollider.steerAngle = -maxSteeringAngle;
    }

    public void TurnRight()
    {
        
        frontLeftCollider.steerAngle = maxSteeringAngle; ;
        frontRightCollider.steerAngle = maxSteeringAngle; ;
    }

    public void ResetSteeringAngle()
    {

        frontLeftCollider.steerAngle = 0;
        frontRightCollider.steerAngle = 0;
    }

    void AnimateWheelMeshes()
    {
        try
        {
            Quaternion FLWRotation;
            Vector3 FLWPosition;
            frontLeftCollider.GetWorldPose(out FLWPosition, out FLWRotation);
            frontLeftMesh.transform.position = FLWPosition;
            frontLeftMesh.transform.rotation = FLWRotation;

            Quaternion FRWRotation;
            Vector3 FRWPosition;
            frontRightCollider.GetWorldPose(out FRWPosition, out FRWRotation);
            frontRightMesh.transform.position = FRWPosition;
            frontRightMesh.transform.rotation = FRWRotation;

            Quaternion RLWRotation;
            Vector3 RLWPosition;
            rearLeftCollider.GetWorldPose(out RLWPosition, out RLWRotation);
            rearLeftMesh.transform.position = RLWPosition;
            rearLeftMesh.transform.rotation = RLWRotation;

            Quaternion RRWRotation;
            Vector3 RRWPosition;
            rearRightCollider.GetWorldPose(out RRWPosition, out RRWRotation);
            rearRightMesh.transform.position = RRWPosition;
            rearRightMesh.transform.rotation = RRWRotation;
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }
}