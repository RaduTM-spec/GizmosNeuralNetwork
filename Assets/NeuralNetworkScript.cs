using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
public class NeuralNetworkScript : MonoBehaviour
{
    int oldLayersNumber = 1;

    [SerializeField] Slider layersNumberSlider;
    int layersNumber { get; set; } = 3;
    [SerializeField] float distanceBetweenLayers = 10f;
    [SerializeField] public float distanceBetweenNeurons = 1f;
    [SerializeField] Color weightsColor = new Color(0, 0, 1f);
    List<GameObject> layers = new List<GameObject>();/////////-----------------------the list of layers
    List<LayerScript> layersScripts = new List<LayerScript>();

   int startingNeuronsNumberForNewLayer = 3;
    private void Awake()
    {
        AddFirstLayer();
    }
    void AddFirstLayer()
    {
        GameObject newLayer = new GameObject();
        LayerScript nl = newLayer.AddComponent<LayerScript>();
        nl.Initialize(transform.position);
        newLayer.name = GetALayerName();
        newLayer.transform.parent = this.transform;
        layers.Add(newLayer);
        layersScripts.Add(nl);
        
    }
    private void Update()
    {
        layersNumber = (int)layersNumberSlider.value;
        UpdateLayers();
    }
    private void UpdateLayers()
    {
        
        if(layersNumber != oldLayersNumber)
        {
            if(layersNumber < oldLayersNumber)//IF the current NN the layers dropped we delete the last layers
            {
                int counter = oldLayersNumber - layersNumber;
                for (int i = 0; i < counter; i++)
                {
                    Destroy(layers[layers.Count - 1]);
                    layers.RemoveAt(layers.Count - 1);
                    layersScripts.RemoveAt(layersScripts.Count - 1);

                }
            }
            else//Add Layers in this case
            {
                int counter = layersNumber - oldLayersNumber;
                for (int i = 0; i < counter; i++)
                {
                    GameObject newLayer = new GameObject();
                    LayerScript nl = newLayer.AddComponent<LayerScript>();
                    nl.Initialize(layers[layers.Count - 1].GetComponent<LayerScript>().center + new Vector3(distanceBetweenLayers, 0, 0));
                    newLayer.name = GetALayerName();
                    newLayer.transform.parent = this.transform;
                    layers.Add(newLayer);
                    layersScripts.Add(nl);
                    
                }
            }
        }

        oldLayersNumber = layersNumber;
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < layersScripts.Count-1; i++)//----------------------------------------------------------------------------Parse each layer
        {
            //---------------------------------------------------------------------------------------------Parse layer matrix
            int neuronsRooted = layersScripts[i].neuronsNumber;
            for (int j = 0; j < neuronsRooted; j++)
            {
                for (int k = 0; k < neuronsRooted; k++)
                {
                    //------------------------------------------------------------Parse next layer matrix
                    int neuronsRootedOnNextLayer = layersScripts[i + 1].neuronsNumber;
                    for (int a = 0; a < neuronsRootedOnNextLayer; a++)
                    {
                        for (int b = 0; b < neuronsRootedOnNextLayer; b++)
                        {
                            float data = layersScripts[i].neuronsMatrix[j, k].data;
                            Gizmos.color = weightsColor + new Color(data, data, 0f);
                            Gizmos.DrawLine(layersScripts[i].neuronsMatrix[j, k].position, layersScripts[i + 1].neuronsMatrix[a, b].position);
                        }
                    }
                     //------------------------------------------------------------Parse next layer matrix
                }
            }
            //-----------------------------------------------------------------------------------------------Parse layer matrix
        }
        //----------------------------------------------------------------------------------------------------------------------------Prase each layer
    }


    //-----------------------------------------------BUTTON FUNCTIONS------------------------------------------//
    public void SetAllNeuronsTo0()
    {
        for (int i = 0; i < layersScripts.Count; i++)
        {
            int neuronsRooted = layersScripts[i].neuronsNumber;
            for (int j = 0; j < neuronsRooted; j++)
            {
                for (int k = 0; k < neuronsRooted; k++)
                {
                    layersScripts[i].neuronsMatrix[j, k].data = 0f;
                }
            }
        }
    }
    public void AssignRandomValues()
    {
        for (int i = 0; i < layersScripts.Count; i++)
        {
            int neuronsRooted = layersScripts[i].neuronsNumber;
            for (int j = 0; j < neuronsRooted; j++)
            {
                for (int k = 0; k < neuronsRooted; k++)
                {
                    layersScripts[i].neuronsMatrix[j, k].data = Random.value;
                }
            }
        }
    }
    public void IncreaseAllInternalLayersTo25Neurons()
    {
        for (int i = 1; i < layersScripts.Count - 1; i++)
        {
            layersScripts[i].neuronsNumber = 5;
        }
    }
    public void IncreaseAllInternalLayersTo100Neurons()
    {
        for (int i = 1 ; i < layersScripts.Count-1; i++)
        {
            layersScripts[i].neuronsNumber = 10;
        }
    }
    public void IncreaseAllInternalLayersTo225Neurons()
    {
        for (int i = 1; i < layersScripts.Count - 1; i++)
        {
            layersScripts[i].neuronsNumber = 15;
        }
    }
    public void IncreaseAllInternalLayersTo400Neurons()
    {
        for (int i = 1; i < layersScripts.Count - 1; i++)
        {
            layersScripts[i].neuronsNumber = 20;
        }
    }

    //-------------------------------------------------GETTERS---------------------------------------------------//
    string GetALayerName()
    {
        StringBuilder name = new StringBuilder();
        name.Append("Layer ");
        name.Append(layers.Count.ToString());
        return name.ToString();
    }
    public int GetLayersNumber()
    {
        return layersNumber;
    }
    public float GetDistanceBetweenLayers()
    {
        return distanceBetweenLayers;
    }
}
