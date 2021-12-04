using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PieceModel
{
    public PieceModel()
    {

    }    

    public PieceModel(int sideA, int sideB)
    {
        this.sideA = sideA;
        this.sideB = sideB;
    }

    public PieceModel(PieceValue pieceValue)
    {
        this.sideA = pieceValue.sideA;
        this.sideB = pieceValue.sideB;
    }

    public Sprite sprite;
    public int sideA;
    public int sideB;
    public GameObject pieceObject;
}

