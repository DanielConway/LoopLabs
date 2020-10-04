using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<Recipe> recipesToDo;

    public float timeInterval;
    public float timeRemaining;

    [SerializeField]
    private GameObject player;
    private Vector3 spawnPoint;

    [HideInInspector]
    public UIManager uIManager;

    [SerializeField]
    GameObject goneEffect;

    AudioManager am;

    [SerializeField]
    AudioClip teleportClip;

    public float timeInLevel;

    public float fillTime1, fillTime2, fillTime3;

    public bool isInLevel = true;

    void Start()
    {
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        timeRemaining = timeInterval;
        spawnPoint = player.transform.position;
        uIManager = this.gameObject.GetComponent<UIManager>();

        uIManager.SetTasks(recipesToDo);

    }
    void Update()
    {
        if (isInLevel)
        {
            timeInLevel += Time.deltaTime;
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                ResetPlayer();
                timeRemaining = timeInterval;
            }
        }

        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void ResetPlayer()
    {
        GameObject tempGone = Instantiate(goneEffect, new Vector3(player.transform.position.x, 1, player.transform.position.z), Quaternion.identity);
        Destroy(tempGone, 2);
        am.PlayClip(teleportClip);
        player.transform.position = spawnPoint;
    }

    public void CheckForWin()
    {

        if (recipesToDo.Count == 0)
        {
            Debug.Log("Done");
            ShowFills();
        }
    }

    public void EndLevel()
    {


    }

    public void ShowFills()
    {
        int fills = 0;
        if (timeInLevel < fillTime1)
        {
            fills++;
        }
        if (timeInLevel < fillTime2)
        {
            fills++;
        }
        if (timeInLevel < fillTime3)
        {
            fills++;
        }

        Debug.Log(timeInLevel  + ",   " + fills);

        uIManager.FillIn(fills);
        isInLevel = false;
    }

    public void NextScene(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }


}
