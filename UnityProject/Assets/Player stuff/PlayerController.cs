using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private int playerNumber;
    public Transform mainCamera;

    public float moveSpeed = 5;
    public float jumpPower = 5;
    public float lookSensitivityHorizontal = 5;
    public float lookSensitivityVertical = 5;

    public float minLookAngleVertical = -90;
    public float maxLookAngleVertical = 90;


    public List<AudioClip> footStepSounds = new List<AudioClip>();
    public List<AudioClip> landSounds = new List<AudioClip>();
    public List<AudioClip> takeDamageSounds = new List<AudioClip>();


    private float moveHorizontalInput = 0;
    private float moveVerticalInput = 0;
    private float lookHorizontalInput = 0;
    private float lookVerticalInput = 0;

    private static PlayerController m_Player1Controller;
    private static PlayerController m_Player2Controller;

    private Animator animator;

    private Rigidbody rigi;
    //private CharacterController characterController;

    private float verticalLookAngle;

    private bool jumpButtonDown = false;

    private Vector3 playerVelocity = new Vector3();

    private float distToGround = 0;

    private CameraView m_CameraView = CameraView.Horizontal;


    public CameraView cameraView {
        get {
            return m_CameraView;
        }
        set {
            m_CameraView = value;
            AdjustCameraView();
        }
    }

    public static PlayerController player1Controller {
        get {
            return m_Player1Controller;
        }
    }
    public static PlayerController player2Controller {
        get {
            return m_Player2Controller;
        }
    }





    // Use this for initialization
    void Start () {
        rigi = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        animator = GetComponent<Animator>();

        //characterController = GetComponent<CharacterController>();

        if (playerNumber == 1 && m_Player1Controller == null) {
            m_Player1Controller = this;
        }
        else if (playerNumber == 2 && m_Player2Controller == null) {
            m_Player2Controller = this;
        }
        else {
            //Destroy(this.gameObject);
        }


    }
	
	// Update is called once per frame
	void Update () {


        moveHorizontalInput = Input.GetAxis("Player" + playerNumber + "MoveHorizontal");
        moveVerticalInput = Input.GetAxis("Player" + playerNumber + "MoveVertical");
        lookHorizontalInput = Input.GetAxis("Player" + playerNumber + "LookHorizontal");
        lookVerticalInput = Input.GetAxis("Player" + playerNumber + "LookVertical");

        if (Input.GetAxisRaw("Player" + playerNumber + "Jump") != 0) {
            if (!jumpButtonDown && IsGrounded()) {
                rigi.velocity += (transform.up * jumpPower) + new Vector3(0, -rigi.velocity.y, 0);

                if (moveVerticalInput < 0) {
                    animator.SetTrigger("Jump Backward");
                }
                else {
                    animator.SetTrigger("Jump Forward");
                }
                animator.SetBool("Falling", true);

                jumpButtonDown = true;
            }
        }
        else {
            jumpButtonDown = false;
        }

        if (IsGrounded() && animator.GetBool("Falling")) {
            animator.SetBool("Falling", false);
        }
    }

    private void FixedUpdate() {

        Vector3 forward = transform.forward * moveVerticalInput * moveSpeed;

        Vector3 horizontal = transform.right * moveHorizontalInput * moveSpeed;
        
        rigi.velocity = forward + horizontal + new Vector3(0,rigi.velocity.y,0);

        rigi.angularVelocity = new Vector3(0, -lookHorizontalInput * lookSensitivityHorizontal, 0);

        verticalLookAngle += lookVerticalInput * lookSensitivityVertical;

        verticalLookAngle = Mathf.Clamp(verticalLookAngle, minLookAngleVertical, maxLookAngleVertical);

        mainCamera.localRotation = Quaternion.Euler(-verticalLookAngle, 180, 0);
        

        if (moveVerticalInput > 0) {
            animator.SetBool("Walk Forward", true);
            animator.speed = moveVerticalInput;
        }
        else {
            animator.SetBool("Walk Forward", false);
            animator.speed = 1;
        }

        if (moveVerticalInput < 0) {
            animator.SetBool("Walk Backward", true);
            animator.speed = -moveVerticalInput;
        }
        else {
            animator.SetBool("Walk Backward", false);
            animator.speed = 1;
        }

        

        if (verticalLookAngle > 0) {
            float weight = verticalLookAngle / maxLookAngleVertical;
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(1, weight);
        }
        else if (verticalLookAngle < 0) {
            float weight = verticalLookAngle / minLookAngleVertical;
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, weight);
        }
        else {
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, 0);
        }
        

    }

    private void AdjustCameraView() {

        if (playerNumber == 1 && m_CameraView == CameraView.Horizontal) {
            mainCamera.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
        }
        else if (playerNumber == 1 && m_CameraView == CameraView.Vertical) {
            mainCamera.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
        }
        if (playerNumber == 2 && m_CameraView == CameraView.Horizontal) {
            mainCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1, 0.5f);
        }
        else if (playerNumber == 2 && m_CameraView == CameraView.Vertical) {
            mainCamera.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
        }
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }


    public void PlaySound(AudioToPlay audioToPlay) {

        AudioSource[] audioSources = GetComponents<AudioSource>();

        AudioSource audioSourceToUse = null;

        for (int i = 0; i < audioSources.Length; i ++) {
            if (!audioSources[i].isPlaying) {
                audioSourceToUse = audioSources[i];
                break;
            }
        }
        if (audioSourceToUse == null) {
            audioSourceToUse = gameObject.AddComponent<AudioSource>();
        }

        if (audioToPlay == AudioToPlay.FootStep) {
            if (footStepSounds.Count > 0) {
                int i = Random.Range(0, footStepSounds.Count - 1);
                audioSourceToUse.PlayOneShot(footStepSounds[i]);
            }
        }
        if (audioToPlay == AudioToPlay.Land) {
            if (landSounds.Count > 0) {
                int i = Random.Range(0, landSounds.Count - 1);
                audioSourceToUse.PlayOneShot(landSounds[i]);
            }
        }
        if (audioToPlay == AudioToPlay.TakeDamage) {
            if (takeDamageSounds.Count > 0) {
                int i = Random.Range(0, takeDamageSounds.Count - 1);
                audioSourceToUse.PlayOneShot(takeDamageSounds[i]);
            }
        }
    }



    public enum CameraView {
        Horizontal,
        Vertical
    }

    public enum AudioToPlay {
        FootStep,
        Land,
        TakeDamage,
    }
}
