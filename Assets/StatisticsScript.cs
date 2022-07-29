using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
public class StatisticsScript : MonoBehaviour
{
    [SerializeField] float refreshRate = 10f;//times per second
    [SerializeField] NeuralNetworkScript nn = null;

    TMP_Text text = null;
    [SerializeField]TMP_Text warning = null;
    StringBuilder stats = new StringBuilder();

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        if (1 / Time.deltaTime < 5f && warning.enabled == false)
        {
            warning.enabled = true;
        }
        else if( 1/Time.deltaTime >= 5f && warning.enabled == true)
            warning.enabled = false;
    }
    private void Start()
    {
        StartCoroutine(RefreshText());
    }
    IEnumerator RefreshText()
    {
        stats.Clear();
        //FPS
        stats.Append((1 / Time.deltaTime).ToString("0.0"));
        stats.Append(" FPS\n");
        //CurrentLayers
        stats.Append("<color=blue>");
        stats.Append(nn.GetLayersNumber().ToString());
        stats.Append("<color=white>");
        stats.Append(" total Layers\n");


        text.text = stats.ToString();
        yield return new WaitForSeconds(1f / refreshRate);
        StartCoroutine(RefreshText());
    }
}
