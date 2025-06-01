using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzle : MonoBehaviour
{
    [SerializeField] private List<int> correctCubeList;
    private HashSet<int> correctCubes = new HashSet<int>();
    private HashSet<int> activatedCubes = new HashSet<int>();
    private List<GameObject> allCubes = new List<GameObject>();

    public GameObject[] activeObjs;
    public bool textActive = false;

    public TextController textController; 

    void Start()
    {
        correctCubes = new HashSet<int>(correctCubeList);
    }

    public void RegisterCube(GameObject cube)
    {
        allCubes.Add(cube);
        cube.GetComponent<Renderer>().material.color = Color.gray;
    }

    public void ToggleCube(GameObject cube)
    {
        int cubeID = cube.GetComponent<Puzzle>().puzzleId;

        if (activatedCubes.Contains(cubeID))
        {
            activatedCubes.Remove(cubeID);
            cube.GetComponent<Renderer>().material.color = Color.gray;
        }
        else
        {
            activatedCubes.Add(cubeID);
            cube.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    public void CheckPuzzle()
    {
        if (!activatedCubes.SetEquals(correctCubes))
        {
            ResetPuzzle();
        }
        else if (activatedCubes.SetEquals(correctCubes))
        {
            foreach (var cube in allCubes)
            {
                cube.GetComponent<Renderer>().material.color = Color.green;
            }
            Debug.Log("퍼즐 완성!");
            if (activeObjs != null)
            {
                foreach (var activeObj in activeObjs)
                {
                    activeObj.SetActive(true);
                }
            }
                
            if (textActive)
                textController.StartTextDisplay();
        }
    }

    private void ResetPuzzle()
    {
        foreach (var cube in allCubes)
        {
            cube.GetComponent<Renderer>().material.color = Color.gray;
        }
        activatedCubes.Clear();
        Debug.Log("틀림");
    }
}
