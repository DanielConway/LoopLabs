using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    Transform holderSpot;

    public GameObject heldObj = null;

    public bool inRangeOfTable = false;
    public bool inRangeOfPickup = false;
    public GameObject curTable = null;
    public GameObject curPickup = null;

    public bool inRangeOfDropoff = false;

    [SerializeField]
    GameObject placeEffect;

    AudioManager am;
    [SerializeField]
    AudioClip pickupSound;

    [SerializeField]
    AudioClip dropSound;

    [SerializeField]
    GameObject prompt;
    [SerializeField]
    Text promptText;
    DropOff d;


    void Start()
    {
        d = GameObject.FindGameObjectWithTag("DropOff").GetComponent<DropOff>();
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        prompt.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Pickup"))
        {
            if (heldObj == null)
            {
                prompt.SetActive(true);
                promptText.text = "E";
                curPickup = other.gameObject;
                inRangeOfPickup = true;
            }
        }

        if (other.gameObject.tag.Equals("Table"))
        {
            inRangeOfTable = true;
            curTable = other.gameObject;
        }

        if (other.gameObject.tag.Equals("DropOff"))
        {
            inRangeOfDropoff = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Table"))
        {

            if (inRangeOfTable)
            {

                inRangeOfTable = false;
                prompt.SetActive(false);

                if (curTable.GetComponent<MultiTable>().workingParticle.isPlaying)
                {
                    curTable.GetComponent<MultiTable>().workingParticle.Stop();
                }
                curTable = null;
            }

        }

        if (other.gameObject.tag.Equals("Pickup"))
        {

            if (inRangeOfPickup == true)
            {
                inRangeOfPickup = false;
                prompt.SetActive(false);
                curPickup = null;
            }
        }

        if (other.gameObject.tag.Equals("DropOff"))
        {

            inRangeOfDropoff = false;
            if (inRangeOfPickup == false)
            {
                prompt.SetActive(false);
            }

        }

    }

    void Update()
    {

        if (inRangeOfTable)
        {
            MultiTable mt = curTable.GetComponent<MultiTable>();

            if (mt.inProgress == false && mt.CanAssemble())
            {
                if (prompt.activeSelf == false)
                {
                    prompt.SetActive(true);
                    promptText.text = "E";
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    prompt.SetActive(false);
                    mt.StartMakingRecipe();
                }

            }

            if (mt.inProgress == false && heldObj != null)
            {

                if (prompt.activeSelf == false)
                {
                    prompt.SetActive(true);
                    promptText.text = "F";
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    MultiTable tableScript = curTable.gameObject.GetComponent<MultiTable>();
                    prompt.SetActive(false);

                    //buggy please fix
                    tableScript.AddGameObject(heldObj);

                    GameObject tempEffect = Instantiate(placeEffect, heldObj.transform.position, Quaternion.identity);
                    Destroy(tempEffect, 2);
                    heldObj = null;

                }

            }

            if (mt.inProgress == true)
            {

                mt.timeRemaining -= Time.deltaTime;
                mt.UpdateProgress();

                if (curTable.GetComponent<MultiTable>().workingParticle.isPlaying == false)
                {
                    curTable.GetComponent<MultiTable>().workingParticle.Play();
                }

                if (mt.timeRemaining <= 0)
                {
                    mt.DoneRecipe();
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                mt.ResetTable();
            }
        }

        if (inRangeOfPickup && curPickup != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpObject(curPickup);
                prompt.SetActive(false);
                am.PlayClip(pickupSound);
            }
        }

        if (heldObj != null && inRangeOfTable == false && inRangeOfDropoff == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DropObject();
                am.PlayClip(dropSound);
            }
        }

        if (inRangeOfDropoff)
        {
            if (prompt.activeSelf == false)
            {

                if (heldObj != null && d.IsValidGO(heldObj))
                {
                    prompt.SetActive(true);
                    promptText.text = "F";
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (d.IsValidGO(heldObj))
                {
                    d.SubmitGameobject(heldObj);
                    Destroy(heldObj);
                    heldObj = null;
                }


            }
        }
    }

    private void PickUpObject(GameObject g)
    {
        g.transform.position = holderSpot.transform.position;
        g.transform.parent = holderSpot;
        heldObj = g;
        curPickup = null;
        inRangeOfPickup = false;

    }

    private void DropObject()
    {
        heldObj.transform.position = new Vector3(heldObj.transform.position.x, 0, heldObj.transform.position.z);
        heldObj.transform.parent = null;
        heldObj = null;
    }
}
