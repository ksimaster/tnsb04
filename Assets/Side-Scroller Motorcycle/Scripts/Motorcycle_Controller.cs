using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Motorcycle_Controller : MonoBehaviour
{
    //if this is activated the controlls will be got from touches else it'll be keyboard or joystick buttons
    public bool forMobile = false;

    public bool is2D = false;
    public bool usingAccelerometer = false;

    //used to determine when player is crashed
    public static bool crash = false;
    public static bool crashed = false;

    //used to enable/disable motorcycle controlling
    public static bool isControllable = true;

    //used to count scores
    public static int score = 0;
    public static int Bonus = 0;
    public static int Total = 0;

    //used to change motorcycle characteristics
    public Rigidbody body;
    public Rigidbody frontFork;
    public Rigidbody rearFork;
    public Rigidbody frontWheel;
    public Rigidbody rearWheel;

    public float speed = 60.0f;
    public float groundedWeightFactor = 20.0f;
    public float inAirRotationSpeed = 10.0f;
    public float wheelieStrength = 15.0f;

    //used to make biker detach from bike when crashed
    public HingeJoint leftHand;
    public HingeJoint rightHand;
    public HingeJoint leftFoot;
    public HingeJoint rightFoot;
    public ConfigurableJoint hips;

    //used for lean backward/forward, changes hips' targetposition value
    public Vector3 leanBackwardTargetPosition;
    public Vector3 leanForwardTargetPosition;
    public float leanSpeed = 2.0f;

    //used to start/stop dirt particles
    public ParticleSystem dirt;

    //used for showing score particles when flips are done
    public ParticleSystem backflipParticle;
    public ParticleSystem frontflipParticle;

    //used to show scores
    public Text scoreText;
    public Text Level_Reward;
    public Text Level_Bonus;
    public Text Total_Score;
    //public Color scoreTextColor;

    //used to determine if motorcycle is grounded or in air
    private RaycastHit hit;
    private bool onGround = false;
    private bool inAir = false;

    //used to manipulate engine sound pitch
    private AudioSource audioSource;
    private float pitch;

    //used to determine when flip is done
    private bool flip = false;

    //used for knowing input	
    private bool accelerate = false;
    private bool brake = false;
    private bool left = false;
    private bool right = false;
    private bool leftORright = false;
    private Vector3 TouchPosition;


    //start function is called once when game starts
    void Start()
    {
        Input.multiTouchEnabled = true;
        //reset static variables
        crash = false;
        crashed = false;
        isControllable = true;

        //find particles
        backflipParticle = GameObject.Find("backflip particle").GetComponent<ParticleSystem>();
        frontflipParticle = GameObject.Find("frontflip particle").GetComponent<ParticleSystem>();

        //scoreText = GameObject.Find("score text").GetComponent<Text>();
        scoreText.text = "SCORE : " + score;
        //Level_Reward.text = score.ToString();
        //Bonus = score + 50;
        //Level_Bonus.text = Bonus.ToString();
        //Total = score + Bonus;
        //Total_Score.text = Total.ToString();
        //MainManager.Instance.Coins = MainManager.Instance.Coins + Total;

        //adding motorcycle body as a follow target for camera
        if (is2D)//if there is activated is2D checkbox on motorcycle, than you need to assign "CameraFollow2D.cs" script to camera
            Camera.main.GetComponent<CameraFollow2D>().target = body.transform;
        else
            Camera.main.GetComponent<SmoothFollow>().target = body.transform;

        //ignoring collision between motorcycle wheels and body
        Physics.IgnoreCollision(frontWheel.GetComponent<Collider>(), body.GetComponent<Collider>());
        Physics.IgnoreCollision(rearWheel.GetComponent<Collider>(), body.GetComponent<Collider>());

        //ignoring collision between motorcycle and ragdoll
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Ragdoll"), true);

        //ignoring collision between motorcycle colliders
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Motorcycle"), true);

        //used to manipulate engine sound pitch
        audioSource = body.GetComponent<AudioSource>();

        //setting wheels max angular rotation speed
        rearWheel.GetComponent<Rigidbody>().maxAngularVelocity = speed;
        frontWheel.GetComponent<Rigidbody>().maxAngularVelocity = speed;
    }

    public void Accelerate()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                accelerate = true;
            }
        }
    }
    public void Accelerate_Off()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                accelerate = false;
            }
        }
    }
    public void Brake()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                brake = true;
            }
        }
    }
    public void Brake_Off()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                brake = false;
            }
        }
    }
    public void Left()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                left = true;
            }
        }
    }
    public void Left_Off()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                left = false;
            }
        }
    }
    public void Right()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                right = true;
            }
        }
    }
    public void Right_Off()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                right = false;
            }
        }
    }
    public void LeftOrRight()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                leftORright = true;
            }
        }
    }
    public void LeftOrRight_Off()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                leftORright = false;
            }
        }
    }

    //  Update is called once per frame
    void Update()
    {
        if (isControllable)
        {
            if (forMobile)
            {
                //use accelerometer for rotatin motorcycle left and right
                if (usingAccelerometer)
                {
                    if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
                    {
                        if (Input.acceleration.x > 0.07f)
                            left = true;
                        else if (Input.acceleration.x < -0.07f)
                            right = true;

                        if (left || right) //left or right button is touched
                            leftORright = true;
                    }
                }

            }
            else
            {
                //detect which keys are pressed. keys relevant to "Horizontal" and "Vertical" keywords are set in: Edit -> Project Settings -> Input
                if (Input.GetAxisRaw("Horizontal") != 0)
                    leftORright = true;
                else
                    leftORright = false;

                if (Input.GetAxisRaw("Horizontal") > 0)
                    right = true;
                else
                    right = false;

                if (Input.GetAxisRaw("Horizontal") < 0)
                    left = true;
                else
                    left = false;

                if (Input.GetAxisRaw("Vertical") > 0 || Input.GetKey(KeyCode.Joystick1Button2))
                    accelerate = true;
                else
                    accelerate = false;

                if (Input.GetAxisRaw("Vertical") < 0 || Input.GetKey(KeyCode.Joystick1Button1))
                    brake = true;
                else
                    brake = false;
                //----------------------------------
            }


            if (body.rotation.eulerAngles.z > 210 && body.rotation.eulerAngles.z < 220)
                flip = true;

            if (body.rotation.eulerAngles.z > 320 && flip) //backflip is done
            {
                flip = false;
                backflipParticle.Emit(1);
                score += 100;
                //scoreText.text = "SCORE : " + score;
                //Level_Reward.text = score.ToString();
                //Bonus = score + Random.Range(50,200);
                //Level_Bonus.text = Bonus.ToString();
                //Total = score + Bonus;
                //Total_Score.text = Total.ToString();
                //MainManager.Instance.Coins = MainManager.Instance.Coins + Total;
            }

            if (body.rotation.eulerAngles.z < 30 && flip) //frontflip is done
            {
                flip = false;
                frontflipParticle.Emit(1);
                score += 150;
                //scoreText.text = "SCORE : " + score;
                //Level_Reward.text = score.ToString();
                //Bonus = score + Random.Range(50, 200);
                //Level_Bonus.text = Bonus.ToString();
                //Total = score + Bonus;
                //Total_Score.text = Total.ToString();
                //MainManager.Instance.Coins = MainManager.Instance.Coins + Total;
            }

            //if any horizontal key (determined in edit -> project settings -> input)  is pressed or if "formobile" is activated, left or right buttons are touched or accelerometer is used
            if (leftORright)
            {
                if (left)//left horizontal key is pressed or left button is touched on mobile or using accelerometer
                {
                    hips.targetPosition = Vector3.Lerp(hips.targetPosition, leanBackwardTargetPosition, leanSpeed * Time.deltaTime); //lean backward
                }

                if (right)//right horizontal key is pressed or if "formobile" is activated, right button is touched or using accelerometer
                {
                    hips.targetPosition = Vector3.Lerp(hips.targetPosition, leanForwardTargetPosition, leanSpeed * Time.deltaTime); //lean forward
                }
            }

            //changing engine sound pitch depending rear wheel rotational speed
            if (accelerate)
            {
                pitch = rearWheel.angularVelocity.sqrMagnitude / speed;
                pitch *= Time.deltaTime * 2;
                pitch = Mathf.Clamp(pitch + 1, 0.5f, 1.8f);
            }
            else
                pitch = Mathf.Clamp(pitch - Time.deltaTime * 2, 0.5f, 1.8f);
        }

        if (crash && !crashed) //if player just crashed
        {
            if (is2D)//if there is activated is2D checkbox on motorcycle, than you need to assign "CameraFollow2D.cs" script to camera
                Camera.main.GetComponent<CameraFollow2D>().target = leftHand.transform; //make camera to follow biker's hips
            else
                Camera.main.GetComponent<SmoothFollow>().target = leftHand.transform; //make camera to follow biker's hips

            //disable hinge joints, so biker detaches from motorcycle
            Destroy(leftHand);
            Destroy(rightHand);
            Destroy(leftFoot);
            Destroy(rightFoot);
            Destroy(hips);

            //turn on collision between ragdoll and motorcycle
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Ragdoll"), false);

            if (!is2D) //disable all physics constraints if 2D isn't activated for motorcycle in inspector menu, so physics calculation will occur on all axis
            {
                body.constraints = RigidbodyConstraints.None;
                frontFork.constraints = RigidbodyConstraints.None;
                frontWheel.constraints = RigidbodyConstraints.None;
                rearFork.constraints = RigidbodyConstraints.None;
                rearWheel.constraints = RigidbodyConstraints.None;
            }

            isControllable = false;
            crashed = true;
        }

        //manipulating engine sound pitch
        pitch = Mathf.Clamp(pitch - Time.deltaTime * 2, 0.5f, 1.8f);
        //audioSource.pitch = pitch;

        //if player is crashed and "R" is pressed or touch is detected on mobile, than restart current level
        if ((Input.GetKeyDown(KeyCode.R) || (Input.touchCount >= 2 && Input.GetTouch(0).phase == TouchPhase.Began)) && crashed)
        {
            score = 0;
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Checkpoint.lastPoint)
        {
            if ((Input.GetKeyDown(KeyCode.C) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && crashed)
            {
                Checkpoint.Reset();
                Destroy(gameObject);
            }
        }
    }



    //physics are calculated in FixedUpdate function
    void FixedUpdate()
    {
        if (isControllable)
        {
            if (accelerate)
            {
                rearWheel.freezeRotation = false; //allow rotation to rear wheel
                rearWheel.AddTorque(new Vector3(0, 0, -speed * Time.deltaTime), ForceMode.Impulse); //add rotational speed to rear wheel

                if (onGround)//if motorcycle is standing on object tagged as "Ground"
                {
                    if (!dirt.isPlaying)
                        dirt.Play(); //play dirt particle

                    dirt.transform.position = rearWheel.position; //allign dirt to rear wheel

                }
                else dirt.Stop();

            }
            else dirt.Stop();

            if (brake)
                rearWheel.freezeRotation = true; //disable rotation for rear wheel if player is braking								
            else
                rearWheel.freezeRotation = false; //enable rotation for rear wheel if player isn't braking

            if (left)
            { //left horizontal key (determined in edit -> project settings -> input) is pressed or left button is touched on mobile if "formobile" is activated
                if (!inAir)
                { //rotate left the motorcycle body
                    body.AddTorque(new Vector3(0, 0, (forMobile ? /*Mathf.Abs(Input.acceleration.x)*/2 : 1) * groundedWeightFactor * 100 * Time.deltaTime));
                    body.AddForceAtPosition(body.transform.up * Mathf.Abs(Input.acceleration.x) * wheelieStrength * body.velocity.sqrMagnitude / 300,
                        new Vector3(frontWheel.position.x, frontWheel.position.y - 0.5f, body.transform.position.z));//add wheelie effect
                }
                else
                {
                    body.AddTorque(new Vector3(0, 0, (forMobile ? /*Mathf.Abs(Input.acceleration.x)*/2 : 1) * inAirRotationSpeed * 100 * Time.deltaTime));
                }

            }
            else if (right)
            { //right horizontal key is pressed or right button is touched on mobile
                if (!inAir)
                { //rotate right the motorcycle body
                    body.AddTorque(new Vector3(0, 0, (forMobile ? /*Mathf.Abs(Input.acceleration.x)*/2 : 1) * -groundedWeightFactor * 100 * Time.deltaTime));
                }
                else
                {
                    body.AddTorque(new Vector3(0, 0, (forMobile ? /*Mathf.Abs(Input.acceleration.x)*/2 : 1) * -inAirRotationSpeed * 100 * Time.deltaTime));
                }

            }

            if (Physics.Raycast(rearWheel.position, -body.transform.up, out hit, 0.4f)) // cast ray to know if motorcycle is in air or grounded	
            {
                //Debug.Log("Bike is on the ground!");
                if (hit.collider.tag == "Ground") //if motorcycle is standig on object taged as "Ground"
                    onGround = true;
                else
                    onGround = false;

                inAir = false;
            }
            else
            {
                //Debug.Log("Bike is OFF the ground");
                onGround = false;
                inAir = true;
            }

        }
        else dirt.Stop();
    }
}