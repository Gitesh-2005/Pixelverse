using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController_Motor : MonoBehaviour {

    public float speed = 10.0f;
    public float sensitivity = 30.0f;
    public float WaterHeight = 15.5f;
    public float jumpHeight = 0.2f;    // Reduced jump height further
    public float gravityValue = -10.0f; // Increased gravity value
    private float gravity;

    CharacterController character;
    public GameObject cam;
    float moveFB, moveLR;
    float rotX, rotY;
    public bool webGLRightClickRotation = true;
    private Vector3 playerVelocity;
    private bool isGrounded;

    void Start() {
        character = GetComponent<CharacterController>();
        if (Application.isEditor) {
            webGLRightClickRotation = false;
            sensitivity = sensitivity * 1.5f;
        }
    }

    void CheckForWaterHeight() {
        if (transform.position.y < WaterHeight) {
            gravity = 0f;
        } else {
            gravity = gravityValue;
        }
    }

    void Update() {
        // Check if the character is on the ground
        isGrounded = character.isGrounded;	

        // Reset vertical velocity if grounded
        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -2f;  // Small negative value to keep the player grounded
        }

        // Movement input
        moveFB = Input.GetAxis("Vertical") * speed;
        moveLR = Input.GetAxis("Horizontal") * speed;

        // Camera rotation input
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

        CheckForWaterHeight();

        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        movement = transform.rotation * movement;

        // Move the character
        character.Move(movement * Time.deltaTime);

        // Camera rotation
        if (webGLRightClickRotation) {
            if (Input.GetKey(KeyCode.Mouse0)) {
                CameraRotation(cam, rotX, rotY);
            }
        } else {
            CameraRotation(cam, rotX, rotY);
        }

        // Jump logic
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            playerVelocity.y = jumpHeight; // Directly set the jump height
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Move the character based on vertical velocity (gravity/jumping)
        character.Move(playerVelocity * Time.deltaTime);
    }

    void CameraRotation(GameObject cam, float rotX, float rotY) {
        transform.Rotate(0, rotX * Time.deltaTime, 0);
        cam.transform.Rotate(-rotY * Time.deltaTime, 0, 0);
    }
}
