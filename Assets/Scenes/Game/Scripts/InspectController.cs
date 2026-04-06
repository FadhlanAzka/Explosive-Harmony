using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InspectController : MonoBehaviour
{
    public Animator BombAnimator;
    public Animator CameraAnimator;
    public Animator FolderAnimator;

    public bool isPickup = false;
    public bool isOpen = false;
    public float rotationSpeed = 1f;

    private Vector3 lastMousePosition;
    private bool isRotating = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isPickup && !isOpen)
            {
                PickupBomb();
            }
        }

        if (isPickup)
        {
            if (Input.GetMouseButtonDown(1))
            {
                lastMousePosition = Input.mousePosition;
                isRotating = false;
            }

            if (Input.GetMouseButton(1))
            {
                Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
                if (deltaMousePosition != Vector3.zero)
                {
                    isRotating = true;
                    RotateBomb(deltaMousePosition);
                    lastMousePosition = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                if (!isRotating)
                {
                    PutdownBomb();
                }
                lastMousePosition = Vector3.zero;
                isRotating = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPickup)
            {
                PutdownBomb();
            }
            else if (!isPickup && !isOpen)
            {
                isOpen = true;
                CameraAnimator.SetBool("isOpen", true);
                FolderAnimator.SetBool("isOpen", true);
            }
            else if (isOpen)
            {
                isOpen = false;
                CameraAnimator.SetBool("isOpen", false);
                FolderAnimator.SetBool("isOpen", false);
            }
        }
    }

    void PickupBomb()
    {
        BombAnimator.SetBool("isPickup", true);
        CameraAnimator.SetBool("isPickup", true);
        isPickup = true;
    }

    void RotateBomb(Vector3 deltaMousePosition)
    {
        if (BombAnimator != null)
        {
            BombAnimator.enabled = false;
        }

        float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
        float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationY, Space.World);
        transform.Rotate(Vector3.right, rotationX, Space.Self);
    }

    void PutdownBomb()
    {
        BombAnimator.SetBool("isPickup", false);
        CameraAnimator.SetBool("isPickup", false);
        isPickup = false;

        if (BombAnimator != null)
        {
            BombAnimator.enabled = true;
        }
    }
}