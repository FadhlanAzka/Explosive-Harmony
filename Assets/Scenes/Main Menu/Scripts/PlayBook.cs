using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBook : MonoBehaviour
{
    public Animator BookAnimator;
    public Animator CameraAnimator;
    public Animator CoverAnimator;
    public Animator FolderAnimator;

    public float rotationSpeed = 1f;

    public bool isPickup = false; 
    public bool isOpen = false;   

    private bool isRotating = false;

    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isPickup && !isOpen)
            {
                PickupBook();
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
                    RotateBook(deltaMousePosition);
                    lastMousePosition = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                if (!isRotating)
                {
                    PutdownBook();
                }
                lastMousePosition = Vector3.zero;
                isRotating = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPickup)
            {
                PutdownBook();
            }
            else if (!isPickup && !isOpen)
            {
                isOpen = true;
                CameraAnimator.SetBool("OpenFolder", true);
                FolderAnimator.SetBool("isOpen", true);
            }
            else if (isOpen)
            {
                isOpen = false;
                CameraAnimator.SetBool("OpenFolder", false);
                FolderAnimator.SetBool("isOpen", false);
            }
        }
    }

    void PickupBook()
    {
        BookAnimator.SetBool("isPickup", true);
        CameraAnimator.SetBool("OpenBook", true);
        CoverAnimator.SetBool("isPickup", true);
        isPickup = true;
    }

    void RotateBook(Vector3 deltaMousePosition)
    {
        if (BookAnimator != null)
        {
            BookAnimator.enabled = false;
            CoverAnimator.enabled = false;
        }

        float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
        float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationY, Space.World);
        transform.Rotate(Vector3.right, rotationX, Space.Self);
    }

    void PutdownBook()
    {
        BookAnimator.SetBool("isPickup", false);
        CameraAnimator.SetBool("OpenBook", false);
        CoverAnimator.SetBool("isPickup", false);
        isPickup = false;

        if (BookAnimator != null)
        {
            BookAnimator.enabled = true;
            CoverAnimator.enabled = true;
        }
    }
}
