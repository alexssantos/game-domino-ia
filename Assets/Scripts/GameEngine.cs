using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class GameEngine
    {
        //public List<PieceModel> pieces = new List<PieceModel>();

        //[Header("Prefabs Pieces")]
        //public GameObject piece;
        //public GameObject hidePiece;

        //[Header("Components UI")]
        //public RectTransform playerHand;
        //public RectTransform opponentHand;
        //public RectTransform table;
        //public Button btnLeft;
        //public Button btnRight;

        //public Text txtPiecesLeftToBuy;
        //public List<Transform> viewPiecesInGame = new List<Transform>();
        //private PieceModel pieceSelected;

        //private List<PieceModel> piecesInGame = new List<PieceModel>();
        //private List<PieceModel> piecesInPlayerHand = new List<PieceModel>();
        //private List<PieceModel> piecesInOpponentHand = new List<PieceModel>();

        //private int leftPiece = -1;
        //private int rightPiece = -1;

        //private bool gameOver = false;
        //private bool startGame = false;


        //private Computer _opponentIA;


        //private void Update()
        //{
        //    if (startGame)
        //    {
        //        if (IsTerminal())
        //            SetGameOver();

        //        txtPiecesLeftToBuy.text = piecesInGame.Count.ToString();
        //    }
        //}


        //#region Botoes UI Methods

        ///// <summary>
        ///// Botão COMPRAR
        ///// </summary>
        //public void Buy()
        //{
        //    BuyPieces(piecesInPlayerHand, piece, playerHand, false);
        //    PlayerTurn(true);
        //}


        ///// <summary>
        /////  Botão de JOGAR
        ///// </summary>
        //public void StartGame()
        //{
        //    piecesInGame.AddRange(pieces);
        //    _opponentIA = new Computer("MINIMAX");

        //    DistributePieces();
        //    CheckFirstPlayer();
        //    startGame = true;
        //}

        //#endregion

        //#region Setup Game
        //public void DistributePieces()
        //{
        //    AddPieceToHand(piecesInGame, piece, piecesInPlayerHand, playerHand, false);
        //    AddPieceToHand(piecesInGame, hidePiece, piecesInOpponentHand, opponentHand, true);
        //}

        //private void AddPieceToHand(List<PieceModel> pieces, GameObject piecePrefab, List<PieceModel> hand, RectTransform handContent, bool isOpponentHand)
        //{
        //    for (int i = 0; i <= 6; i++)
        //    {
        //        int index = Random.Range(0, pieces.Count);
        //        hand.Add(pieces[index]);
        //        AddPieceToCanvas(pieces[index], piecePrefab, handContent, isOpponentHand);
        //        pieces.RemoveAt(index);
        //    }
        //}

        //private void AddPieceToCanvas(PieceModel piece, GameObject piecePrefab, RectTransform hand, bool IsOpponentHand)
        //{
        //    GameObject o = Instantiate(piecePrefab, hand);
        //    o.GetComponent<Image>().sprite = piece.sprite;


        //    if (IsOpponentHand)
        //    {
        //        o.GetComponent<Button>().interactable = false;
        //    }

        //    piece.pieceObject = o;
        //    o.GetComponent<PieceValue>().sideA = piece.sideA;
        //    o.GetComponent<PieceValue>().sideB = piece.sideB;
        //    o.GetComponent<Button>().onClick.AddListener(() => PieceClick(piece));
        //}

        //private void CheckFirstPlayer()
        //{
        //    PieceModel greatestDoubletPlayer = CheckGreatestDoubletes(piecesInPlayerHand);
        //    PieceModel greatestDoubletOpponent = CheckGreatestDoubletes(piecesInOpponentHand);

        //    if (greatestDoubletOpponent.sideA > greatestDoubletPlayer.sideA)
        //    {
        //        PlayerTurn(false);
        //        pieceSelected = greatestDoubletOpponent;
        //        OpponentCheck(true);
        //        return;
        //    }

        //    PlayerTurn(true);
        //}

        //#endregion

        //#region Opponent

        //private void OpponentCheck(bool isFirstPlay)
        //{
        //    //Thread.Sleep(2*1000); //Computador aguarda 2s para jogar

        //    if (isFirstPlay)
        //    {
        //        OppnentPlay(90, pieceSelected, false);
        //        return;
        //    }

        //    //TODO: OBTER MOVIMENTO IA
        //    var pieceToPlay = GetAllPossiblePiece(piecesInOpponentHand).FirstOrDefault();
        //    var possiblePieces = GetAllPossiblePiece(piecesInOpponentHand);
        //    //var pieceToPlay = _opponentIA.ObterMelhorJogada(possiblePieces);
        //    if (pieceToPlay != default)
        //    {
        //        var (rotaion, tableSide, pieaceMatch) = GetRotationAndTableSideForPieace(pieceToPlay);
        //        OppnentPlay(rotaion, pieceToPlay, tableSide);
        //        return;
        //    }

        //    if (!gameOver)
        //    {
        //        BuyPieces(piecesInOpponentHand, hidePiece, opponentHand, true);
        //        OpponentCheck(false);
        //    }
        //}

        //public PieceModel GetFirstPossiblePieceOppnent()
        //{
        //    //varre as peças e pega a primeira válida.
        //    for (int i = 0; i < piecesInOpponentHand.Count; i++)
        //    {
        //        var ixPiece = piecesInOpponentHand[i];
        //        if (CheckCanPlayPiece(ixPiece))
        //            return ixPiece;
        //    }
        //    return default;
        //}

        //public List<PieceModel> GetAllPossiblePiece(List<PieceModel> pieces)
        //{
        //    return pieces
        //        .Where(piece => CheckCanPlayPiece(piece))
        //        .ToList();
        //}

        //private (int, bool, bool) GetRotationAndTableSideForPieace(PieceModel pieceModel)
        //{
        //    var pieceMatch = CheckCanPlayPiece(pieceModel);

        //    if (pieceModel.sideA == leftPiece)
        //        return (-90, true, pieceMatch);
        //    else if (pieceModel.sideB == leftPiece)
        //        return (90, true, pieceMatch);

        //    //Jogar a Direita na mesa
        //    if (pieceModel.sideA == rightPiece)
        //        return (90, false, pieceMatch);
        //    else if (pieceModel.sideB == rightPiece)
        //        return (-90, false, pieceMatch);

        //    return default;
        //}

        //private bool CheckCanPlayPiece(PieceModel pieceModel)
        //{
        //    var pieceMatch =
        //        pieceModel.sideA == leftPiece
        //        || pieceModel.sideB == leftPiece
        //        || pieceModel.sideA == rightPiece
        //        || pieceModel.sideB == rightPiece;

        //    return pieceMatch;
        //}

        //private void OppnentPlay(float rotation, PieceModel pieceModel, bool isLeft)
        //{
        //    pieceSelected = pieceModel;


        //    pieceSelected.pieceObject.transform.SetParent(table);
        //    if (isLeft)
        //    {
        //        pieceSelected.pieceObject.transform.SetAsFirstSibling();
        //    }
        //    else
        //    {
        //        pieceSelected.pieceObject.transform.SetAsLastSibling();
        //    }

        //    pieceSelected.pieceObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, rotation);
        //    piecesInOpponentHand.Remove(pieceSelected);
        //    PlayerTurn(true);
        //}

        //#endregion

        //#region Player

        ///// <summary>
        ///// Configura a jogada do Humano para sua vez (interagivel) ou não.
        ///// </summary>
        ///// <param name="isTurn"></param>
        //private void PlayerTurn(bool isTurn)
        //{
        //    PieceCheck();

        //    for (int i = 0; i < piecesInPlayerHand.Count; i++)
        //    {
        //        if (isTurn && leftPiece != -1 && rightPiece != -1)
        //        {
        //            if (piecesInPlayerHand[i].sideA == leftPiece || piecesInPlayerHand[i].sideB == leftPiece || piecesInPlayerHand[i].sideA == rightPiece || piecesInPlayerHand[i].sideB == rightPiece)
        //            {
        //                piecesInPlayerHand[i].pieceObject.GetComponent<Button>().interactable = true;
        //            }
        //        }
        //        else
        //        {
        //            piecesInPlayerHand[i].pieceObject.GetComponent<Button>().interactable = isTurn;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Seleciona a peça possivel para a jogada.
        ///// Chamado por Botao
        ///// </summary>
        ///// <param name="piece"></param>
        //private void PieceClick(PieceModel piece)
        //{
        //    if (pieceSelected != null)
        //        pieceSelected.pieceObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        //    pieceSelected = piece;
        //    pieceSelected.pieceObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);

        //    btnLeft.onClick.RemoveAllListeners();
        //    btnRight.onClick.RemoveAllListeners();

        //    if (leftPiece == -1 && rightPiece == -1)
        //    {
        //        btnLeft.gameObject.SetActive(false);
        //        btnRight.gameObject.SetActive(true);
        //        btnRight.onClick.AddListener(() => Play(false, 90));
        //        return;
        //    }

        //    if (piece.sideA == leftPiece && piece.sideB == rightPiece)
        //    {
        //        btnLeft.gameObject.SetActive(true);
        //        btnRight.gameObject.SetActive(true);

        //        btnLeft.onClick.AddListener(() => Play(true, -90));
        //        btnRight.onClick.AddListener(() => Play(false, -90));
        //        return;
        //    }

        //    if (piece.sideA == rightPiece && piece.sideB == leftPiece)
        //    {
        //        btnLeft.gameObject.SetActive(true);
        //        btnRight.gameObject.SetActive(true);

        //        btnLeft.onClick.AddListener(() => Play(true, 90));
        //        btnRight.onClick.AddListener(() => Play(false, 90));
        //        return;
        //    }

        //    if (piece.sideA == leftPiece)
        //    {
        //        btnLeft.gameObject.SetActive(true);
        //        btnRight.gameObject.SetActive(false);
        //        btnLeft.onClick.AddListener(() => Play(true, -90));
        //        return;
        //    }
        //    else if (piece.sideB == leftPiece)
        //    {
        //        btnLeft.gameObject.SetActive(true);
        //        btnRight.gameObject.SetActive(false);
        //        btnLeft.onClick.AddListener(() => Play(true, 90));
        //        return;
        //    }

        //    if (piece.sideA == rightPiece)
        //    {
        //        btnLeft.gameObject.SetActive(false);
        //        btnRight.gameObject.SetActive(true);
        //        btnRight.onClick.AddListener(() => Play(false, 90));
        //        return;
        //    }
        //    else if (piece.sideB == rightPiece)
        //    {
        //        btnLeft.gameObject.SetActive(false);
        //        btnRight.gameObject.SetActive(true);
        //        btnRight.onClick.AddListener(() => Play(false, -90));
        //        return;
        //    }
        //}

        //private void PieceCheck()
        //{
        //    List<Transform> piecesInGame = new List<Transform>();
        //    int childs = table.childCount;

        //    for (int i = 0; i < childs; i++)
        //    {
        //        piecesInGame.Add(table.GetChild(i));
        //    }

        //    PieceValue GetPieceInGame(int index, bool invertOrder = false)
        //    {
        //        var ix = (invertOrder) ? (piecesInGame.Count - 1 - index) : index;
        //        return piecesInGame[ix].GetComponent<PieceValue>();
        //    }

        //    if (childs > 1)
        //    {
        //        var firstPeace = new PieceModel(GetPieceInGame(0));
        //        var secoundPiece = new PieceModel(GetPieceInGame(1));
        //        leftPiece = CheckPossiblePlay(firstPeace, secoundPiece);

        //        var invertOrder = true;
        //        var last = new PieceModel(GetPieceInGame(0, invertOrder));
        //        var penultimate = new PieceModel(GetPieceInGame(1, invertOrder));
        //        rightPiece = CheckPossiblePlay(last, penultimate);
        //    }

        //    if (childs == 1)
        //    {
        //        var firstPeace = new PieceModel(GetPieceInGame(0));
        //        leftPiece = firstPeace.sideA;
        //        rightPiece = firstPeace.sideB;
        //    }

        //    viewPiecesInGame = piecesInGame;
        //}

        ///// <summary>
        ///// Realiza a jogada ao selecionar a peça e o lado do tabuleiro disponivel.
        ///// Chamado por botão
        ///// </summary>
        ///// <param name="isLeft"></param>
        ///// <param name="valueRotation"></param>
        //public void Play(bool isLeft, float valueRotation)
        //{
        //    pieceSelected.pieceObject.transform.SetParent(table);
        //    if (isLeft)
        //    {
        //        pieceSelected.pieceObject.transform.SetAsFirstSibling();
        //    }
        //    else
        //    {
        //        pieceSelected.pieceObject.transform.SetAsLastSibling();
        //    }

        //    pieceSelected.pieceObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        //    pieceSelected.pieceObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, valueRotation);
        //    btnLeft.onClick.RemoveAllListeners();
        //    btnRight.onClick.RemoveAllListeners();
        //    btnLeft.gameObject.SetActive(false);
        //    btnRight.gameObject.SetActive(false);
        //    pieceSelected.pieceObject.GetComponent<Button>().onClick.RemoveAllListeners();
        //    piecesInPlayerHand.Remove(pieceSelected);
        //    PlayerTurn(false);
        //    OpponentCheck(false);
        //}

        //#endregion

        //#region Game Methods
        //private PieceModel CheckGreatestDoubletes(List<PieceModel> listPieces)
        //{
        //    PieceModel greatestDoublet = new PieceModel { sideA = -1, sideB = -1 };
        //    List<PieceModel> doublets = new List<PieceModel>();

        //    for (int i = 0; i < listPieces.Count; i++)
        //    {
        //        if (listPieces[i].sideA == listPieces[i].sideB)
        //        {
        //            doublets.Add(listPieces[i]);
        //        }
        //    }

        //    if (doublets.Count > 0)
        //    {
        //        if (doublets.Count == 1)
        //        {
        //            return doublets[0]; ;
        //        }

        //        for (int i = 0; i < doublets.Count; i++)
        //        {
        //            if (doublets[i].sideA > greatestDoublet.sideA)
        //            {
        //                greatestDoublet = doublets[i];
        //            }
        //        }
        //    }

        //    return greatestDoublet;
        //}

        //public bool IsTerminal()
        //{
        //    var playerWinns = piecesInPlayerHand.Count == 0;
        //    var computerWinns = piecesInOpponentHand.Count == 0;
        //    var drawGame = gameOver && piecesInOpponentHand.Count > 0 && piecesInPlayerHand.Count > 0;
        //    return playerWinns || computerWinns || drawGame;
        //}

        //private void SetGameOver()
        //{
        //    if (piecesInPlayerHand.Count == 0)
        //    {
        //        print("Player Ganhou!");
        //        gameOver = true;
        //        UnityEditor.EditorApplication.isPlaying = false;
        //    }

        //    if (piecesInOpponentHand.Count == 0)
        //    {
        //        print("IA Ganhou!");
        //        gameOver = true;
        //        UnityEditor.EditorApplication.isPlaying = false;
        //    }

        //    if (gameOver == true && piecesInOpponentHand.Count > 0 && piecesInPlayerHand.Count > 0)
        //    {
        //        print("Empate seus bucha");
        //        UnityEditor.EditorApplication.isPlaying = false;
        //    }
        //}

        //private int CheckPossiblePlay(PieceModel pieceA, PieceModel pieceB)
        //{
        //    int pieceValue = pieceA.sideA;

        //    if (pieceA.sideA == pieceB.sideA || pieceA.sideA == pieceB.sideB)
        //    {
        //        pieceValue = pieceA.sideB;
        //    }

        //    return pieceValue;
        //}

        //private void BuyPieces(List<PieceModel> piecesInHand, GameObject piecePrefab, RectTransform handContent, bool isOpponentHand)
        //{
        //    if (piecesInGame.Count > 0)
        //    {
        //        int index = Random.Range(0, piecesInGame.Count - 1);
        //        PieceModel newPiece = piecesInGame[index];
        //        piecesInHand.Add(newPiece);
        //        AddPieceToCanvas(newPiece, piecePrefab, handContent, isOpponentHand);
        //        piecesInGame.RemoveAt(index);
        //    }
        //    else
        //    {                
        //        gameOver = true;
        //    }
        //}

        //#endregion
    }
}
