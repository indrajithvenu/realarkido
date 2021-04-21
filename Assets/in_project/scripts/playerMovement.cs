using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerMovement : MonoBehaviour
{
    private Rigidbody myBody;
    private Transform playerTransform;
    Vector3 startPosition, startScale;
    public float moveForce = 10f;
    public string objName;
    private FixedJoystick joystick;
    private Animator anim;

    private AnimationClip[] animationClips;

    List<string> listNameClip = new List<string>();
    private Button attackBtn;
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
        anim = GameObject.Find(objName).GetComponent<Animator>();
        GameObject.Find("attack_button").GetComponent<Button>().onClick.AddListener(attackfn);
        AnimationClip[] arrclip = anim.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in arrclip)
        {
            if (clip.name == "attack")
            {
                GameObject.Find("attack_button").GetComponent<Button>().enabled = false;
            }
            else
            {
                GameObject.Find("attack_button").GetComponent<Button>().enabled = true;

            }
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = playerTransform.localPosition;
        startScale = playerTransform.localScale;

        myBody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
        anim = GameObject.Find(objName).GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {


        myBody.velocity = new Vector3(joystick.Horizontal * moveForce, myBody.velocity.y, joystick.Vertical * moveForce);

        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        {
            anim.SetBool("walk", true);
            transform.rotation = Quaternion.LookRotation(myBody.velocity);
        }
        else
        {

            anim.SetBool("walk", false);

        }



    }
    public void resetPos()
    {
        playerTransform.localPosition = startPosition;
        playerTransform.localScale = startScale;
        playerTransform.localRotation = Quaternion.Euler(0, -145, 0);
    }

    public void attackfn()
    {
        anim.Play("attack");
    }

}
