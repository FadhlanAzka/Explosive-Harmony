using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleWires : MonoBehaviour
{
    public GameObject Wire1;
    public GameObject Wire2;
    public GameObject Wire3;
    public GameObject Wire4;
    public GameObject Wire5;
    public GameObject LED;

    public bool WireSuccess = false;

    private List<GameObject> activeWires = new List<GameObject>();

    void Start()
    {
        GameObject[] wires = { Wire1, Wire2, Wire3, Wire4, Wire5 };

        foreach (GameObject wire in wires)
        {
            wire.SetActive(true);
            SetRandomColor(wire);
            activeWires.Add(wire);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (activeWires.Contains(hit.collider.gameObject))
                {
                    hit.collider.gameObject.SetActive(false);
                    CheckWireSuccess();
                }
            }
        }

        if (WireSuccess)
        {
            LED.GetComponent<Renderer>().material.color = Color.green;
            DisableAllWireColliders();
        }
    }

    void CheckWireSuccess()
    {
        List<GameObject> remainingWires = new List<GameObject>();
        foreach (GameObject wire in activeWires)
        {
            if (wire.activeSelf)
            {
                remainingWires.Add(wire);
            }
        }

        int redWireCount = remainingWires.FindAll(w => w.GetComponent<Renderer>().material.color == Color.red).Count;
        int blueWireCount = remainingWires.FindAll(w => w.GetComponent<Renderer>().material.color == Color.blue).Count;
        int yellowWireCount = remainingWires.FindAll(w => w.GetComponent<Renderer>().material.color == Color.yellow).Count;
        int whiteWireCount = remainingWires.FindAll(w => w.GetComponent<Renderer>().material.color == Color.white).Count;
        int blackWireCount = remainingWires.FindAll(w => w.GetComponent<Renderer>().material.color == Color.black).Count;

        // Define 10 conditions
        if (redWireCount == 2)
        {
            CutWire(remainingWires, 3); // Cut the 4th wire
        }
        else if (blueWireCount == 1)
        {
            CutWire(remainingWires, 0); // Cut the 1st wire
        }
        else if (yellowWireCount > 2)
        {
            CutWire(remainingWires, 4); // Cut the 5th wire
        }
        else if (whiteWireCount == 0)
        {
            CutWire(remainingWires, 2); // Cut the 3rd wire
        }
        else if (blackWireCount == 3)
        {
            CutWire(remainingWires, 1); // Cut the 2nd wire
        }
        else if (redWireCount == 1 && yellowWireCount == 1)
        {
            CutWire(remainingWires, 0); // Cut the 1st wire
        }
        else if (blueWireCount == 2 && blackWireCount == 2)
        {
            CutWire(remainingWires, 4); // Cut the 5th wire
        }
        else if (whiteWireCount == 1 && redWireCount == 1)
        {
            CutWire(remainingWires, 3); // Cut the 4th wire
        }
        else if (yellowWireCount == 2)
        {
            CutWire(remainingWires, 1); // Cut the 2nd wire
        }
        else if (blackWireCount == 1 && whiteWireCount == 2)
        {
            CutWire(remainingWires, 2); // Cut the 3rd wire
        }
        else
        {
            CutWire(remainingWires, remainingWires.Count - 1); // Cut the last remaining wire
        }
    }

    void CutWire(List<GameObject> wires, int indexToCut)
    {
        if (indexToCut >= 0 && indexToCut < wires.Count)
        {
            wires[indexToCut].SetActive(false);
            WireSuccess = true;
        }
    }

    void DisableAllWireColliders()
    {
        foreach (GameObject wire in activeWires)
        {
            Collider collider = wire.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    void ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            T temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }
    }

    void SetRandomColor(GameObject wire)
    {
        Color[] possibleColors = { Color.red, Color.blue, Color.yellow, Color.white, Color.black };
        int randomIndex = Random.Range(0, possibleColors.Length);

        Renderer renderer = wire.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = possibleColors[randomIndex];
        }
        else
        {
            SpriteRenderer spriteRenderer = wire.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = possibleColors[randomIndex];
            }
            else
            {
                Debug.LogWarning("No Renderer or SpriteRenderer found on wire GameObject.");
            }
        }
    }
}