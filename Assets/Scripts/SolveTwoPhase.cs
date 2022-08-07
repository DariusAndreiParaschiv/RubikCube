using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;
using UnityEngine.UI;

public class SolveTwoPhase : MonoBehaviour
{
    public ReadCube readCube;
    public CubeState cubeState;
    private bool doOnce = true;
    private Button solveButton;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
        solveButton = GameObject.Find("Solve").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CubeState.started && doOnce)
        {
            Solver();
            doOnce = false;
        } 
        if(CubeState.autoRotating)
        {
            solveButton.interactable = false;
        }
        else
        {
            solveButton.interactable = true;
        }
    }

    public void Solver()
    {
        solveButton.interactable = false;
        readCube.ReadState();

        // get the state of the cube as a string
        string moveString = cubeState.GetState();
        print(moveString);

        // solve the cube
        string info = "";
        string solution = "";

        if (doOnce)
        {
            // First time build the tables
            solution = SearchRunTime.solution(moveString, out info, buildTables: true);
        }
        else
        {
            //Every other time
            solution = Search.solution(moveString, out info);
        }

        // convert the solved moves from a string to a list
        List<string> solutionList = StringToList(solution);

        //Automate the list
        AutoRotation.moves = solutionList;

        print(info);
        //readCube.ReadState();
        solveButton.interactable = true;
    }

    List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }

}
