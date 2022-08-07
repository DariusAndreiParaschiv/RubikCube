using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigDegree : MonoBehaviour
{
    public int degree;
    private string cubeConfig;
    private CubeState cubeState;
    private List<string> cubeConfigList = new List<string> ();
    public GameObject degreeValue;
    public Text degreeValueText;
    private int passes = 0;

    private void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        /*degreeValue = GameObject.Find("DegreeValue").GetComponent<GameObject>();
        degreeValueText = GameObject.Find("DegreeValue").GetComponent<Text>();*/
        degreeValue.SetActive(false);
    }

    private void Update()
    {
        if(cubeConfigList.Count > 0 && !CubeState.autoRotating && AutoRotation.moves.Count == 0)
        {
            for(int i = 0; i < cubeConfigList.Count; i++)
            {
                if(cubeConfigList[i] != CubeState.initialConfigList[i])
                {
                    AutoRotation.moves = cubeConfigList;
                    AutoRotation.moves.Clear();
                    degree++;
                    break;
                }
                passes++;
            }
            if(passes == cubeConfigList.Count)
            {
                degreeValueText.text = degree.ToString();
            }
        }
    }

    public void CalculateDegree()
    {
        degreeValue.SetActive(true);
        cubeConfig = cubeState.GetState();
        cubeConfigList = CubeState.StringToList(cubeConfig);
        //AutoRotation.moves = cubeConfigList; 
    }
}
