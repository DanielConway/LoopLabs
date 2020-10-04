using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiTable : MonoBehaviour
{
    [SerializeField]
    private int size;
    public List<GameObject> curObjs;

    public Transform[] objSpots;

    public bool buildable = false;

    public bool inProgress = false;
    public int recipeIndex = 99;

    private float recipeTime;
    public float timeRemaining;

    private LevelManager lm;

    [SerializeField]
    GameObject productPoint;

    [SerializeField]
    Slider progressSlider;

    [SerializeField]
    GameObject completedEffect;
    public ParticleSystem workingParticle;

    private AudioManager am;
    [SerializeField]
    AudioClip doneMaking;
    [SerializeField]
    AudioClip placeObj;
    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        progressSlider.gameObject.SetActive(false);
    }

    public void ArrangeObjects()
    {

        for (int i = 0; i < curObjs.Count; i++)
        {
            curObjs[i].transform.position = objSpots[i].position;
        }

        if (CanAssemble())
        {
            buildable = true;
        }
    }

    public void AddGameObject(GameObject g)
    {

        if (curObjs.Count < size)
        {
            curObjs.Add(g);

            am.PlayClip(placeObj);

            g.GetComponent<SphereCollider>().enabled = false;

            //Debug.Log(g.name);
            g.transform.parent = this.transform;

            ArrangeObjects();
        }
    }

    public bool CanPlace()
    {
        if (curObjs.Count < size)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanAssemble()
    {

        foreach (Recipe r in lm.recipesToDo)
        {

            if (CanCreateRecipe(r))
            {
                return true;
            }

        }
        return false;
    }

    public int RecipeCreatedID(Recipe r)
    {

        for (int i = 0; i < lm.recipesToDo.Count; i++)
        {
            if (lm.recipesToDo[i] == r)
            {
                return i;
            }
        }

        return 99;

    }

    public bool CanCreateRecipe(Recipe r)
    {

        int itemsNeeded = r.ingredients.Length;
        int itemsHave = 0;

        foreach (Part p in r.ingredients)
        {
            foreach (GameObject g in curObjs)
            {

                if (g.GetComponent<Pickup>().part == p)
                {
                    itemsHave++;
                }
            }
        }

        if (itemsHave == itemsNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartMakingRecipe()
    {

        inProgress = true;

        foreach (Recipe r in lm.recipesToDo)
        {

            if (CanCreateRecipe(r))
            {
                recipeIndex = RecipeCreatedID(r);
            }

        }

        recipeTime = lm.recipesToDo[recipeIndex].timeToMake;
        timeRemaining = recipeTime;
        if(!workingParticle.isPlaying){
            workingParticle.Play();
        }
        
        progressSlider.gameObject.SetActive(true);

    }

    public void DoneRecipe()
    {


        GameObject productGO = Instantiate(lm.recipesToDo[recipeIndex].productGO, productPoint.transform.position, Quaternion.identity, null);
        GameObject effectTemp = Instantiate(completedEffect,productGO.transform.position,Quaternion.identity);
        Destroy(effectTemp,2);
        //lm.recipesToDo.RemoveAt(recipeIndex);
        //lm.uIManager.RemoveTask(recipeIndex);
        ResetTable();
        if(workingParticle.isPlaying){
            workingParticle.Stop();
        }
        am.PlayClip(doneMaking);
        progressSlider.gameObject.SetActive(false);

    }

    public void ResetTable()
    {
        buildable = false;
        inProgress = false;
        foreach (GameObject go in curObjs)
        {
            Destroy(go);
        }
        curObjs.Clear();
        recipeTime = 99;
        timeRemaining = 0;
        recipeIndex = 99;
    }

    public void UpdateProgress()
    {

        progressSlider.value = (recipeTime - timeRemaining) / recipeTime;
    }


}
