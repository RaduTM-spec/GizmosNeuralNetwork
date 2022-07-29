using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject Target;
    NeuralNetworkScript TargetScript;

    [SerializeField] float distance = 1f;

    private void Awake()
    {
        if(Target!= null)
            TargetScript = Target.GetComponent<NeuralNetworkScript>();
    }
    void Update()
    {
        transform.position = Target.transform.position
                           + new Vector3((TargetScript.GetLayersNumber()-1) * TargetScript.GetDistanceBetweenLayers()/2, 0f,- TargetScript.GetLayersNumber() * TargetScript.GetDistanceBetweenLayers() / 2 * distance);
    }
}
