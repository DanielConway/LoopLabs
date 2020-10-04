using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : MonoBehaviour
{
    LevelManager lm;

    [SerializeField]
    GameObject submitEffect;

    AudioManager am;

    [SerializeField]
    AudioClip sound;
    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void SubmitGameobject(GameObject g){
        
        for (int i = 0; i < lm.recipesToDo.Count; i++)
        {
            if(lm.recipesToDo[i].productGO.GetComponent<Pickup>().part == g.GetComponent<Pickup>().part){
                lm.recipesToDo.RemoveAt(i);
                lm.uIManager.RemoveTask(i);
                am.PlayClip(sound);
                GameObject sub = Instantiate(submitEffect,g.transform.position,Quaternion.identity);
                Destroy(sub,2);
                lm.CheckForWin();
                
            }
        }
    }

    public bool IsValidGO(GameObject g){

        for (int i = 0; i < lm.recipesToDo.Count; i++)
        {
            if(lm.recipesToDo[i].productGO.GetComponent<Pickup>().part == g.GetComponent<Pickup>().part){
                return true;
                
            }
        }
        return false;
    }
}
