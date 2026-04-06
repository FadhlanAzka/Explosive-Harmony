using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleSymbol : MonoBehaviour
{
    public GameObject[] SymbolGroups;
    public bool SymbolSuccess = false;

    private void Start()
    {
        if (SymbolGroups.Length == 0)
        {
            Debug.LogWarning("No SymbolGroups assigned!");
            return;
        }

        int selectedIndex = Random.Range(0, SymbolGroups.Length);

        for (int i = 0; i < SymbolGroups.Length; i++)
        {
            SymbolGroups[i].SetActive(i == selectedIndex);
        }
    }
}