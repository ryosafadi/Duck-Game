using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecoratePond : MonoBehaviour
{
    public GameObject[] assetPrefabs; // Array of asset prefabs
    private GameObject selectedAsset; // Currently selected asset

    void Start()
    {
        selectedAsset = null;
    }

    // Called when an asset button is clicked
    public void SelectAsset(int assetIndex)
    {
        selectedAsset = assetPrefabs[assetIndex];
    }

    void Update()
    {
        if (selectedAsset != null && Input.GetMouseButtonDown(0))
        {
            PlaceAsset();
        }
    }

    void PlaceAsset()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(selectedAsset, worldPosition, Quaternion.identity);
        selectedAsset = null; // Deselect asset after placing
    }
}
