using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3;
    [SerializeField]
    GameObject fpsCamera;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float CameraUpDownRotation = 0f;
    private float CurrentCameraUpAndDownRotation = 0f;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {       
        rigidBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //calculate movement velocity as a 3d vector
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        //final movement velocity
        Vector3 _movementVelocity = (_movementHorizontal+_movementVertical).normalized * speed;

        // Apply Movement
        Move(_movementVelocity);

        //CALCULATE ROTATION AS A 3D WEB VECTOR FOR TURN
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0, _yRotation,0)*lookSensitivity;

        //Apply Rotation
        Rotate(_rotationVector);

        //Calculate look up and down rotation
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y")*lookSensitivity;
        RotateCamera(_cameraUpDownRotation);

    }

    //RUNS PER PHYSICS ITERATION
    private void FixedUpdate() {
        if (velocity != Vector3.zero)
        {
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        }

        rigidBody.MoveRotation(rigidBody.rotation* Quaternion.Euler(rotation));

        if (fpsCamera != null)
        {
            CurrentCameraUpAndDownRotation -= CameraUpDownRotation;
            CurrentCameraUpAndDownRotation = Mathf.Clamp(CurrentCameraUpAndDownRotation, -85,85);
            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation, 0, 0);
        }
    }

    void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }

    void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }

    void RotateCamera(float cameraUpDownRotation)
    {
        CameraUpDownRotation = cameraUpDownRotation;
    }
}
