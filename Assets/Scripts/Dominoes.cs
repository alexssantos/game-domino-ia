using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dominoes : MonoBehaviour
{
    public List<PieceModel> pieces = new List<PieceModel>();

    [Header("Prefabs Pieces")]
    public GameObject piece;
    public GameObject hidePiece;

    [Header("Components UI")]
    public RectTransform playerHand;
    public RectTransform opponentHand;
    public RectTransform table;
    public Button btnLeft;
    public Button btnRight;

    private List<PieceModel> piecesInGame = new List<PieceModel>();
    private List<PieceModel> piecesInPlayerHand = new List<PieceModel>();
    private List<PieceModel> piecesInOpponentHand = new List<PieceModel>();

    private PieceModel pieceSelected;
    private int leftPiece = -1;
    private int rightPiece = -1;

    public List<Transform> viewPiecesInGame = new List<Transform>();

    private bool gameOver = false;
    private bool startGame = false;

    public Text txtPiecesLeftToBuy;


    //TODO: O que esse metodo faz ? 
    private void Update()
    {
        if (startGame) 
        {
            if (piecesInPlayerHand.Count == 0)
            {
                print("Player Ganhou!");
                gameOver = true;
                UnityEditor.EditorApplication.isPlaying = false;
            }

            if (piecesInOpponentHand.Count == 0)
            {
                print("IA Ganhou!");
                gameOver = true;
                UnityEditor.EditorApplication.isPlaying = false;
            }

            if(gameOver == true && piecesInOpponentHand.Count > 0 && piecesInPlayerHand.Count > 0)
            {
                print("Empate seus bucha");
                UnityEditor.EditorApplication.isPlaying = false;
            }

            txtPiecesLeftToBuy.text = piecesInGame.Count.ToString();
        }
    }

    //É chamado lá no botão de jogar
    public void StartGame()
    {
        piecesInGame.AddRange(pieces);
        DistributePieces();
        CheckFirstPlayer();
        startGame = true;
    }

    public void DistributePieces()
    {
        AddPieceToHand(piecesInGame, piece, piecesInPlayerHand, playerHand, false);
        AddPieceToHand(piecesInGame, hidePiece, piecesInOpponentHand, opponentHand, true);
    }

    private void AddPieceToHand(List<PieceModel> pieces, GameObject piecePrefab, List<PieceModel> hand, RectTransform handContent, bool isOpponentHand) 
    {
        for (int i = 0; i <= 6; i++) 
        {
            int index = Random.Range(0, pieces.Count);
            hand.Add(pieces[index]);
            AddPieceToCanvas(pieces[index], piecePrefab, handContent, isOpponentHand);
            pieces.RemoveAt(index);
        }
    }

    private void AddPieceToCanvas(PieceModel piece, GameObject piecePrefab, RectTransform hand, bool IsOpponentHand)
    {
        GameObject o = Instantiate(piecePrefab, hand);
        o.GetComponent<Image>().sprite = piece.sprite;


        if (IsOpponentHand)
        {
            o.GetComponent<Button>().interactable = false;
        }

        piece.pieceObject = o;
        o.GetComponent<PieceValue>().sideA = piece.sideA;
        o.GetComponent<PieceValue>().sideB = piece.sideB;
        o.GetComponent<Button>().onClick.AddListener(() => PieceClick(piece));
    }

    private void CheckFirstPlayer()
    {
        PieceModel greatestDoubletPlayer = CheckGreatestDoubletes(piecesInPlayerHand);
        PieceModel greatestDoubletOpponent = CheckGreatestDoubletes(piecesInOpponentHand);

        if(greatestDoubletOpponent.sideA > greatestDoubletPlayer.sideA)
        {
            PlayerTurn(false);
            pieceSelected = greatestDoubletOpponent;
            OpponentCheck(true);
            return;
        }

        PlayerTurn(true);
    }

    private PieceModel CheckGreatestDoubletes(List<PieceModel> listPieces)
    {
        PieceModel greatestDoublet = new PieceModel {sideA = -1, sideB = -1 };
        List<PieceModel> doublets = new List<PieceModel>();

        for (int i = 0; i < listPieces.Count; i++)
        {
            if (listPieces[i].sideA == listPieces[i].sideB)
            {
                doublets.Add(listPieces[i]);
            }
        }

        if (doublets.Count > 0)
        {
            if(doublets.Count == 1)
            {
                return doublets[0]; ;
            }

            for (int i = 0; i < doublets.Count; i++)
            {
                if(doublets[i].sideA > greatestDoublet.sideA)
                {
                    greatestDoublet = doublets[i];
                }
            }
        }

        return greatestDoublet;
    }

    private void OpponentCheck(bool isFirstPlay)
    {
        if (isFirstPlay)
        {
            OppnentPlay(90, pieceSelected, false);
            return;
        }

        if (PlayFirstPossiblePeaceOppnent())
            return;

        if (!gameOver)
        {
            BuyPieces(piecesInOpponentHand, hidePiece, opponentHand, true);
            OpponentCheck(false);
        }

    }

    public bool PlayFirstPossiblePeaceOppnent()
    {
        //varre as peças e pega a primeira válida.
        for (int i = 0; i < piecesInOpponentHand.Count; i++)
        {
            //Esquerda
            if (piecesInOpponentHand[i].sideA == leftPiece)
            {
                OppnentPlay(-90, piecesInOpponentHand[i], true);
                return true;
            }
            else if (piecesInOpponentHand[i].sideB == leftPiece)
            {
                OppnentPlay(90, piecesInOpponentHand[i], true);
                return true;
            }

            //Direita
            if (piecesInOpponentHand[i].sideA == rightPiece)
            {
                OppnentPlay(90, piecesInOpponentHand[i], false);
                return true;
            }
            else if (piecesInOpponentHand[i].sideB == rightPiece)
            {
                OppnentPlay(-90, piecesInOpponentHand[i], false);
                return true;
            }
        }
        return false;
    }



    private void BuyPieces(List<PieceModel> piecesInHand, GameObject piecePrefab, RectTransform handContent, bool isOpponentHand)
    {
        if (piecesInGame.Count > 0)
        {
            int index = Random.Range(0, piecesInGame.Count - 1);
            PieceModel newPiece = piecesInGame[index];
            piecesInHand.Add(newPiece);
            AddPieceToCanvas(newPiece, piecePrefab, handContent, isOpponentHand);
            piecesInGame.RemoveAt(index);
        }
        else
        {
            print("Game Over, Acabou as peças!");
            gameOver = true;
        }
    }

    private void OppnentPlay(float rot, PieceModel pieceModel, bool isLeft)
    {
        pieceSelected = pieceModel;


        pieceSelected.pieceObject.transform.SetParent(table);
        if (isLeft)
        {
            pieceSelected.pieceObject.transform.SetAsFirstSibling();
        }
        else
        {
            pieceSelected.pieceObject.transform.SetAsLastSibling();
        }

        pieceSelected.pieceObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, rot);
        piecesInOpponentHand.Remove(pieceSelected);
        PlayerTurn(true);
    }

    private void PlayerTurn(bool isTurn)
    {
        PieceCheck();

        for (int i = 0; i < piecesInPlayerHand.Count; i++)
        {
            if (isTurn && leftPiece != -1 && rightPiece != -1)
            {
                if (piecesInPlayerHand[i].sideA == leftPiece || piecesInPlayerHand[i].sideB == leftPiece || piecesInPlayerHand[i].sideA == rightPiece || piecesInPlayerHand[i].sideB == rightPiece)
                {
                    piecesInPlayerHand[i].pieceObject.GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                piecesInPlayerHand[i].pieceObject.GetComponent<Button>().interactable = isTurn;
            }
        }
    }

    public void Buy()
    {
        BuyPieces(piecesInPlayerHand, piece, playerHand, false);
        PlayerTurn(true);
    }

    private void PieceClick(PieceModel piece)
    {
        if(pieceSelected != null)
            pieceSelected.pieceObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        pieceSelected = piece;
        pieceSelected.pieceObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);

        btnLeft.onClick.RemoveAllListeners();
        btnRight.onClick.RemoveAllListeners();

        if (leftPiece == -1 && rightPiece == -1)
        {
            btnLeft.gameObject.SetActive(false);
            btnRight.gameObject.SetActive(true);
            btnRight.onClick.AddListener(() => Play(false, 90));
            return;
        }

        if (piece.sideA == leftPiece && piece.sideB == rightPiece)
        {
            btnLeft.gameObject.SetActive(true);
            btnRight.gameObject.SetActive(true);

            btnLeft.onClick.AddListener(() => Play(true, -90));
            btnRight.onClick.AddListener(() => Play(false, -90));
            return;
        }

        if (piece.sideA == rightPiece && piece.sideB == leftPiece)
        {
            btnLeft.gameObject.SetActive(true);
            btnRight.gameObject.SetActive(true);

            btnLeft.onClick.AddListener(() => Play(true, 90));
            btnRight.onClick.AddListener(() => Play(false, 90));
            return;
        }

        if (piece.sideA == leftPiece)
        {
            btnLeft.gameObject.SetActive(true);
            btnRight.gameObject.SetActive(false);
            btnLeft.onClick.AddListener(() => Play(true, -90));
            return;
        }
        else if (piece.sideB == leftPiece)
        {
            btnLeft.gameObject.SetActive(true);
            btnRight.gameObject.SetActive(false);
            btnLeft.onClick.AddListener(() => Play(true, 90));
            return;
        }

        if (piece.sideA == rightPiece)
        {
            btnLeft.gameObject.SetActive(false);
            btnRight.gameObject.SetActive(true);
            btnRight.onClick.AddListener(() => Play(false, 90));
            return;
        }
        else if (piece.sideB == rightPiece)
        {
            btnLeft.gameObject.SetActive(false);
            btnRight.gameObject.SetActive(true);
            btnRight.onClick.AddListener(() => Play(false, -90));
            return;
        }
    }

    private void PieceCheck()
    {
        List<Transform> piecesInGame = new List<Transform>();
        int childs = table.childCount;

        for (int i = 0; i < childs; i++)
        {
            piecesInGame.Add(table.GetChild(i));
        }

        if (childs > 1)
        {
            leftPiece = CheckPossiblePlay(new PieceModel { sideA = piecesInGame[0].GetComponent<PieceValue>().sideA, sideB = piecesInGame[0].GetComponent<PieceValue>().sideB },
                                  new PieceModel { sideA = piecesInGame[1].GetComponent<PieceValue>().sideA, sideB = piecesInGame[1].GetComponent<PieceValue>().sideB });

            rightPiece = CheckPossiblePlay(new PieceModel { sideA = piecesInGame[piecesInGame.Count - 1].GetComponent<PieceValue>().sideA, sideB = piecesInGame[piecesInGame.Count - 1].GetComponent<PieceValue>().sideB },
                          new PieceModel { sideA = piecesInGame[piecesInGame.Count - 2].GetComponent<PieceValue>().sideA, sideB = piecesInGame[piecesInGame.Count - 2].GetComponent<PieceValue>().sideB });
        }

        if(childs == 1)
        {
            leftPiece = piecesInGame[0].GetComponent<PieceValue>().sideA;

            rightPiece = piecesInGame[0].GetComponent<PieceValue>().sideB;
        }

        viewPiecesInGame = piecesInGame;
    }

    private int CheckPossiblePlay(PieceModel pieceA, PieceModel pieceB)
    {
        int pieceValue = pieceA.sideA;

        if(pieceA.sideA == pieceB.sideA || pieceA.sideA == pieceB.sideB)
        {
            pieceValue = pieceA.sideB;
        }

        return pieceValue;
    }

    //É chamado no button na cena
    public void Play(bool isLeft, float valueRotation)
    {
        pieceSelected.pieceObject.transform.SetParent(table);
        if (isLeft)
        {
            pieceSelected.pieceObject.transform.SetAsFirstSibling();
        }
        else
        {
            pieceSelected.pieceObject.transform.SetAsLastSibling();
        }

        pieceSelected.pieceObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        pieceSelected.pieceObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, valueRotation);
        btnLeft.onClick.RemoveAllListeners();
        btnRight.onClick.RemoveAllListeners();
        btnLeft.gameObject.SetActive(false);
        btnRight.gameObject.SetActive(false);
        pieceSelected.pieceObject.GetComponent<Button>().onClick.RemoveAllListeners();
        piecesInPlayerHand.Remove(pieceSelected);
        PlayerTurn(false);
        OpponentCheck(false);
    }


}
