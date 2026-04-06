using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class FreeLook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    private TextMeshProUGUI buttonText;
    private Image buttonImage;
    private Camera mainCamera;
    private Quaternion initialRotation;
    private bool isFreeLookActive;

    private void Start()
    {
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = button.GetComponent<Image>();
        mainCamera = Camera.main;
        initialRotation = mainCamera.transform.rotation;

        button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            Color textColor = buttonText.color;
            textColor.a = 128 / 255f; // Set alpha to 128 / 255
            buttonText.color = textColor;
        }

        if (buttonImage != null)
        {
            Color imageColor = buttonImage.color;
            imageColor.a = 32 / 255f; // Set alpha to 32 / 255
            buttonImage.color = imageColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            Color textColor = buttonText.color;
            textColor.a = 0; // Set alpha to 1
            buttonText.color = textColor;
        }

        if (buttonImage != null)
        {
            Color imageColor = buttonImage.color;
            imageColor.a = 0; // Set alpha to 1
            buttonImage.color = imageColor;
        }
    }

    private void OnButtonClick()
    {
        isFreeLookActive = !isFreeLookActive;

        if (!isFreeLookActive)
        {
            mainCamera.transform.rotation = initialRotation;
        }
    }

    private void Update()
    {
        if (isFreeLookActive)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            mainCamera.transform.Rotate(Vector3.up, mouseX * 2f, Space.World);
            mainCamera.transform.Rotate(Vector3.right, -mouseY * 2f, Space.Self);
        }
    }
}
