using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCam : MonoBehaviour
{   
    public Transform target;
    private void Update() {
        this.transform.LookAt(2*this.transform.position - target.position);
    }
}
