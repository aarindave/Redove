using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarCustomization : MonoBehaviour
{
    [SerializeField]
    private Material materialToEdit;
    [SerializeField]
    private Color colorSelection;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            colorSelection = image.color;
        }
    }

    public void SetMaterialToEditColor()
    {
        materialToEdit.color = colorSelection;
    }
}
