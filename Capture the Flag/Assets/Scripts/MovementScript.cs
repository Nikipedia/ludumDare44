﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript : MonoBehaviour
{

    class CameraState
    {
        public float yaw;
        public float pitch;
        public float roll;
        public void SetFromTransform(Transform t)
        {
            pitch = t.eulerAngles.x;
            yaw = t.eulerAngles.y;
            roll = t.eulerAngles.z;
        }
        public void LerpTowards(CameraState target, float rotationLerpPct)
        {
            yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
            pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
            roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);
        }
        public void UpdateTransform(Transform t)
        {
            t.eulerAngles = new Vector3(pitch, yaw, roll);
        }
    }
    public float movementSpeed;
    CameraState m_TargetCameraState = new CameraState();
    CameraState m_InterpolatingCameraState = new CameraState();
    [Header("Rotation Settings")]
    [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
    public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

    [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
    public float rotationLerpTime = 0.01f;

    [Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
    public bool invertY = false;
    public Transform cameraTrans;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpMultiplier = 0.2f;
    Rigidbody rb;
    Collider c;
    public bool doubleJump, dash;
    private float dashTimer;
    public float dashCooldown;
    public float dashRange;
    public GameObject dashArrow;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<Collider>();
        
    }

    void OnEnable()
    {
        m_TargetCameraState.SetFromTransform(cameraTrans);
        m_InterpolatingCameraState.SetFromTransform(cameraTrans);
    }



    Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            direction += cameraTrans.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += cameraTrans.forward * -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += cameraTrans.right * -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += cameraTrans.right;
        }
        /*if (Input.GetKey(KeyCode.Q))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.E))
        {
            direction += Vector3.up;
        }*/
        return direction;
    }

    // Update is called once per frame
    void Update()
    {
        dashTimer--;
        if(Input.GetKey(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.velocity += Vector3.up * -Physics.gravity.y * jumpMultiplier;
        }
        /*if (Input.GetKey(KeyCode.Space)&&rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }*/
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if(Input.GetMouseButtonDown(0) && dashTimer <= 0 && dash)
        {
            dashArrow.SetActive(true);
        }
        if (Input.GetMouseButton(0) && dashTimer <= 0 && dash)
        {
            dashArrow.transform.eulerAngles = new Vector3(dashArrow.transform.eulerAngles.x, dashArrow.transform.eulerAngles.y, cameraTrans.rotation.y);
        }
        if (Input.GetMouseButtonUp(0) && dash && dashTimer <= 0)
        {
            dashArrow.SetActive(false);
            dashTimer = dashCooldown;
            var translation = cameraTrans.forward * dashRange;
            translation.y = 0;
            var newPos = transform.position + translation;
            NavMeshHit myNavHit;
            if(NavMesh.SamplePosition(newPos, out myNavHit, 100, -1))
            {
                transform.position = newPos;
            }
        }
        else
        {
            var translation = GetInputTranslationDirection() * Time.deltaTime * movementSpeed;
            translation.y = 0;
            transform.Translate(translation);
        }
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

        var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

        m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
        m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;
        var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
        m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, rotationLerpPct);

        m_InterpolatingCameraState.UpdateTransform(cameraTrans);
        //Vector2 camOffset = cameraTrans.forward * -0.5f;
        //cameraTrans.localPosition = new Vector3(camOffset.x, 1, camOffset.y);
    }
}
