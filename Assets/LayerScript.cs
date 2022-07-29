using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Neuron
{
    public float data;
    public Vector3 position;

    public Neuron(Vector3 pos, float d)
    {
        this.data = d;
        this.position = pos;
    }
}
public class LayerScript : MonoBehaviour
{
    [Range(0.1f,10f)]public static float distBetweenNeur = 1f;
    [SerializeField] public static float wavingSpeed = .05f;
    [SerializeField] public static float wavingRange = .2f;

    public Vector3 center;
    Vector3 position;
    int oldNeuronsNumber = 3;
    [SerializeField,Range(1,10)]public int neuronsNumber = 3; //This is Squared, every layers is a Square
    public Neuron[,] neuronsMatrix;

   
    NeuralNetworkScript nnScript = null;
   
    public void Initialize(Vector3 center)
    {
      
        this.center = center;
        neuronsMatrix = new Neuron[neuronsNumber, neuronsNumber];
        UpdateNeuronsPosition();

        return;
        StartCoroutine(WaveNeurons());
    }

    private void Update()
    {
        UpdateNeurons();
    }
    void UpdateNeurons()
    {
        if(neuronsNumber > oldNeuronsNumber)
        {
            Neuron[,] newMatrix = new Neuron[neuronsNumber, neuronsNumber];

            //Copy Previous data
            for (int i = 0; i < oldNeuronsNumber; i++)
            {
                for (int j = 0; j < oldNeuronsNumber; j++)
                {
                    newMatrix[i, j] = neuronsMatrix[i, j];
                }
            }
            neuronsMatrix = null;
            neuronsMatrix = (Neuron[,])newMatrix.Clone();
            UpdateNeuronsPosition();

        }
        else if(neuronsNumber < oldNeuronsNumber)
        {
            Neuron[,] newMatrix = new Neuron[neuronsNumber, neuronsNumber];

            //Copy Previous data
            for (int i = 0; i < neuronsNumber; i++)
            {
                for (int j = 0; j < neuronsNumber; j++)
                {
                    newMatrix[i, j] = neuronsMatrix[i, j];
                }
            }
            neuronsMatrix = null;
            neuronsMatrix = (Neuron[,])newMatrix.Clone();
            UpdateNeuronsPosition();
        }

        oldNeuronsNumber = neuronsNumber;
    }
    void UpdateNeuronsPosition()
    {
        position = center - new Vector3(0f, neuronsNumber / 2, neuronsNumber / 2);
        for (int i = 0; i < neuronsNumber; i++)
        {
            for (int j = 0; j < neuronsNumber; j++)
            {

                neuronsMatrix[i, j].position = position + new Vector3(0, i, j) * distBetweenNeur;
                //neuronsMatrix[i, j] = new Neuron(position + new Vector3(0, i, j), 0f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        
        for (int i = 0; i < neuronsNumber; i++)
        {
            for (int j = 0; j < neuronsNumber; j++)
            {
                float data = neuronsMatrix[i, j].data;
                Gizmos.color = new Color(data, data, data);
                Gizmos.DrawCube(neuronsMatrix[i, j].position, new Vector3(0.1f,0.1f,0.1f));
            }
        }
    }

    IEnumerator WaveNeurons()
    {
        for (int i = 0; i < neuronsNumber; i++)
        {
            for (int j = 0; j < neuronsNumber; j++)
            {
                neuronsMatrix[i, j].position = Vector3.Lerp(neuronsMatrix[i, j].position, 
                                                            new Vector3(position.x, neuronsMatrix[i,j].position.y, neuronsMatrix[i, j].position.z) + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * wavingRange,
                                                            wavingSpeed);
            }
        }
        yield return null;
        StartCoroutine(WaveNeurons());
    }

    Vector3 GetPosition()
    {
        return position;
    }

    
}
