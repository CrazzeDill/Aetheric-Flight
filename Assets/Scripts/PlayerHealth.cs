using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image heartPrefab;
    public Transform heartContainer;
    public int currentLife = 1;
    private float spacing = -100f;

    private void Start()
    {
        // Initialize the life indicator based on the player's initial life value
        UpdateLifeIndicator();
    }

    private void UpdateLifeIndicator()
    {
        int lifePoints = currentLife;

        // Remove any existing heart images
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }

        // Generate heart images based on the player's life points
        for (int i = 0; i < lifePoints; i++)
        {
            // Instantiate a heart image and set its parent to the heart container
            Image heartImage = Instantiate(heartPrefab, heartContainer);
            // Set any desired properties of the heart image (e.g., size, position, sprite)
            float yPosition = i * spacing -50f;
            heartImage.rectTransform.anchoredPosition = new Vector2(0f, yPosition);
        }
    }
}
