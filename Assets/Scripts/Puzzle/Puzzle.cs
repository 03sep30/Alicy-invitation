using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public int puzzleId;

    private CubePuzzle cubePuzzle;

    void Start()
    {
        cubePuzzle = FindObjectOfType<CubePuzzle>();
        cubePuzzle.RegisterCube(gameObject);
    }
}
