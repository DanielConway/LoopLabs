using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskEntry : MonoBehaviour
{
    [SerializeField]
    private GameObject imagePlaceholder;
    [SerializeField]
    private GameObject partsLayout;
    public Text recipeName;
    public List<GameObject> partsImg;
    public Image compoundImg;
    public List<Part> ingrediants;

    public void ApplyValues(){

        //make the needed number of blank images
        for (int i = 0; i < ingrediants.Count; i++)
        {
            GameObject blankImg = Instantiate(imagePlaceholder,Vector3.zero, Quaternion.identity, partsLayout.transform);
            blankImg.GetComponent<RectTransform>().sizeDelta = new Vector2(65,65);
            partsImg.Add(blankImg);
        }

        //assign the correct sprites 
        for (int i = 0; i < ingrediants.Count; i++)
        {
            partsImg[i].GetComponent<Image>().sprite = ingrediants[i].icon;
        }

    }
}
