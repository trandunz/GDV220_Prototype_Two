using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] Material[] materials;
    [SerializeField] Color[] materialColors;

    [SerializeField] Color flashColor = Color.white;

    public float flashTime = 0.2f;

    private void Start()
    {
        Renderer[] renderer = GetComponentsInChildren<Renderer>();
        materials = new Material[renderer.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = renderer[i].material;
        }
        materialColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            materialColors[i] = materials[i].color;
        }
    }

    public void FlashStart()
    {
        StartCoroutine(FlashAnimation());
    }

    IEnumerator FlashAnimation()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = flashColor;
        }

        yield return new WaitForSeconds(flashTime);
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = materialColors[i];
        }
    }
}
