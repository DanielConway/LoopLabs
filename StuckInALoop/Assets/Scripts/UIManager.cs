using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject taskLayout;

    [SerializeField]
    GameObject taskPrefab;

    public List<GameObject> tasksList;
    LevelManager lm;

    [SerializeField]
    Slider timeSlider;

    [SerializeField]
    GameObject[] fillAnims;

    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    GameObject blocker;

    [SerializeField]
    GameObject gameui;

    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        StartCoroutine(UpdateSlider());
    }

    public void SetTasks(List<Recipe> inList)
    {
        foreach (Recipe r in inList)
        {
            GameObject task = Instantiate(taskPrefab, Vector3.zero, Quaternion.identity, taskLayout.transform);
            tasksList.Add(task);
            TaskEntry taskEntry = task.GetComponent<TaskEntry>();
            taskEntry.recipeName.text = r.recipeName;
            taskEntry.compoundImg.sprite = r.icon;
            List<Part> tempList = new List<Part>();

            foreach (Part p in r.ingredients)
            {
                tempList.Add(p);
            }
            taskEntry.ingrediants = tempList;

            taskEntry.ApplyValues();

        }
    }

    //problem is when 2 benches are going at once
    public void RemoveTask(int i)
    {

        GameObject temp = tasksList[i];
        tasksList.RemoveAt(i);
        Destroy(temp);
    }

    IEnumerator UpdateSlider()
    {
        while (true)
        {
            timeSlider.value = (lm.timeInterval - lm.timeRemaining)/lm.timeInterval;
            yield return new WaitForSeconds(0.005f);
        }
    }

    public void FillIn(int numToFill){
        gameui.SetActive(false);
        blocker.SetActive(true);
        endScreen.SetActive(true);
        blocker.GetComponent<Animation>().Play();
        endScreen.GetComponent<Animation>().Play();
        StartCoroutine(fillingWait(numToFill));
    }

    IEnumerator fillingWait(int fillLevel){
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < fillLevel; i++)
        {
            fillAnims[i].GetComponent<Animation>().Play();
            yield return new WaitForSeconds(1f);
        }
    }

}
