using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class MenuFolder : MonoBehaviour
{
    public GameObject credits;
    public GameObject menu;
    public GameObject QR;

    public void ShowCredits()
    {
        credits.SetActive(true);
        menu.SetActive(false);
        QR.SetActive(false);
    }
    
    public void HideCredits()
    {
        credits.SetActive(false);
        menu.SetActive(true);
        QR.SetActive(false);
    }
    
    public void ShowQR()
    {
        credits.SetActive(false);
        menu.SetActive(false);
        QR.SetActive(true);
    }
    
    public void HideQR()
    {
        credits.SetActive(false);
        menu.SetActive(true);
        QR.SetActive(false);
    }
}