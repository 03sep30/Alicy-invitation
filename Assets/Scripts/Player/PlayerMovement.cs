using Cinemachine;
using StarterAssets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

enum CharacterState
{
    Idle,
    Walking,
    Running,
    Dead
}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public bool isJumping;

    public GameObject blindnessPanel;
    private RawImage blindnessRawImage;
    private RectTransform blindnessRectTransform;

    public float dashSpeed = 2f;

    public bool isDashing;
    public bool isBreaking;
    public bool isWalking;
    public bool isBoosting;

    public float originalMoveSpeed;
    public float originalJumpHeight;
    public float originalSprintSpeed;

    public ParentConstraint cheshireParentConstraint;
    private PlayerHealth playerHealth;
    private PlayerStamina playerStamina;
    private PlayerController playerController;
    private ThirdPersonController thirdPersonController;
    private CinemachineVirtualCamera virtualCamera;

    [Header("BGM")]
    public AudioClip breakingSound;

    void Start()
    {
        playerHealth = GetComponentInChildren<PlayerHealth>();
        playerStamina = GetComponent<PlayerStamina>();
        playerController = GetComponent<PlayerController>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        blindnessRawImage = blindnessPanel.GetComponent<RawImage>();
        blindnessRectTransform = blindnessPanel.GetComponent<RectTransform>();

        originalMoveSpeed = thirdPersonController.MoveSpeed;
        originalJumpHeight = thirdPersonController.JumpHeight;
        originalSprintSpeed = thirdPersonController.SprintSpeed;
    }

    void Update()
    {
        if (!playerHealth.isDie && GameManager.Instance.currentSize == CharacterSize.Small)
        {
            Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);
            gameObject.transform.localScale = scale;
            //Boost();
            if (cheshireParentConstraint != null && cheshireParentConstraint.sourceCount > 0)
            {
                Vector3 newOffset = new Vector3(0.75f, 0.5f, 0f);
                cheshireParentConstraint.SetTranslationOffset(0, newOffset);
            }


            thirdPersonController.MoveSpeed = originalMoveSpeed;
            thirdPersonController.JumpHeight = originalJumpHeight;

            blindnessRectTransform.sizeDelta = new Vector2(1920f, 1080f);
        }
        if (!playerHealth.isDie && !playerController.crushing && GameManager.Instance.currentSize == CharacterSize.Normal)
        {
            Vector3 scale = Vector3.one;
            gameObject.transform.localScale = scale;
            virtualCamera.m_Lens.FieldOfView = 40f;

            if (cheshireParentConstraint != null)
            {
                Vector3 newOffset = new Vector3(0.75f, 1f, 0f);
                cheshireParentConstraint.SetTranslationOffset(0, newOffset);
            }
            //thirdPersonController.MoveSpeed = originalMoveSpeed;
            thirdPersonController.JumpHeight = originalJumpHeight;

            blindnessRectTransform.sizeDelta = new Vector2(1920f, 1080f);
        }
        if (!playerHealth.isDie && GameManager.Instance.currentSize == CharacterSize.Big)
        {
            Vector3 scale = new Vector3(2f, 2f, 2f);
            gameObject.transform.localScale = scale;
            virtualCamera.m_Lens.FieldOfView = 80f;

            if (cheshireParentConstraint != null)
            {
                Vector3 newOffset = new Vector3(0.75f, 2f, 0f);
                cheshireParentConstraint.SetTranslationOffset(0, newOffset);
            }

            thirdPersonController.MoveSpeed = originalMoveSpeed;
            thirdPersonController.JumpHeight = originalJumpHeight;

            blindnessRectTransform.sizeDelta = new Vector2(2800f, 1600f);
        }
    }

    private void FixedUpdate()
    {
        if (!playerHealth.isDie && !playerController.isPanelActive)
        {
            thirdPersonController.Move();
        }
    }
    public void Dash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //playerStamina.DecreaseStamina(1);

            if (GameManager.Instance.currentSize == CharacterSize.Big && !isBreaking)
            {
                isBreaking = true;
                Debug.Log("Break");
            }
        }
    }

    public void Boost()
    {
        float boostSpeed = 30f;
        float jumpForce = 5f;

        thirdPersonController.MoveSpeed = boostSpeed;
        thirdPersonController.JumpHeight = jumpForce;

        isBoosting = true;
    }

    
}