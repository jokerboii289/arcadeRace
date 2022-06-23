using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    public Rigidbody rbody;

    public float forwardAccel, reverseAccel, maxSpeed, turnStrength,gravityForce=10f,dragOnGround=3f;

    private float speedInput, turnInput;

    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength=.5f;
    public Transform groundRayPoint;

    [Header("wheels")]
    [SerializeField] GameObject wheel1, wheel2;

    // Start is called before the first frame update
    void Start()
    {
        rbody.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if ((Input.GetAxis("Vertical") < 0))
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical")), transform.eulerAngles.z);
        }

        transform.position = rbody.transform.position;
    }

    private void FixedUpdate()   
    {
        grounded = false;
        RaycastHit hit;
        if(Physics.Raycast(groundRayPoint.position,-transform.up,out hit, groundRayLength,whatIsGround))
        {
            print(hit.transform.name);
            grounded = true; 
            transform.rotation *= Quaternion.FromToRotation(transform.up,hit.normal);
        }
        if (grounded)
        {
            rbody.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                rbody.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            rbody.drag = .1f;
            rbody.AddForce(Vector3.up * -gravityForce * 300f);//adding extra gravity
        }
    }

    //simulate wheels
    void WheelSimulate()
    {

    }

    void TurningAngle() // when turning make the bike tilt
    {
        
    }

    void DisorientBike()
    {

    }
  
}
