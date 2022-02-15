/*
 * Copyright (c) 2018 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board board;

    public Canvas canvas;

    public GameObject whiteKing;
    public GameObject whiteQueen;
    public GameObject whiteBishop;
    public GameObject whiteKnight;
    public GameObject whiteRook;
    public GameObject whitePawn;

    public GameObject blackKing;
    public GameObject blackQueen;
    public GameObject blackBishop;
    public GameObject blackKnight;
    public GameObject blackRook;
    public GameObject blackPawn;

    private GameObject[,] pieces;
    private List<GameObject> movedPawns;

    private Player white;
    private Player black;
    public Player currentPlayer;
    public Player otherPlayer;

    int randomRook1;
    int randomRook2;
    int randomKing;
    int randomBishop1;
    int randomBishop2;
    int randomKnight1;
    int randomKnight2;
    int randomQueen;

    void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        pieces = new GameObject[8, 8];
        movedPawns = new List<GameObject>();

        white = new Player("white", true);
        black = new Player("black", false);

        currentPlayer = white;
        otherPlayer = black;

        //InitialSetup();
        //Initial960();

    }

    public void InitialSetup()
    {
        AddPiece(whiteRook, white, 0, 0);
        AddPiece(whiteKnight, white, 1, 0);
        AddPiece(whiteBishop, white, 2, 0);
        AddPiece(whiteQueen, white, 3, 0);
        AddPiece(whiteKing, white, 4, 0);
        AddPiece(whiteBishop, white, 5, 0);
        AddPiece(whiteKnight, white, 6, 0);
        AddPiece(whiteRook, white, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(whitePawn, white, i, 1);
        }

        AddPiece(blackRook, black, 0, 7);
        AddPiece(blackKnight, black, 1, 7);
        AddPiece(blackBishop, black, 2, 7);
        AddPiece(blackQueen, black, 3, 7);
        AddPiece(blackKing, black, 4, 7);
        AddPiece(blackBishop, black, 5, 7);
        AddPiece(blackKnight, black, 6, 7);
        AddPiece(blackRook, black, 7, 7);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(blackPawn, black, i, 6);
        }

        canvas.enabled = false;
    }

    public void Initial960()
    {
        PlaceRooks960();
        PlaceKing960();
        PlaceBishops960();
        PlaceRestOfPieces960();

        for (int i = 0; i < 8; i++)
        {
            AddPiece(whitePawn, white, i, 1);
            AddPiece(blackPawn, black, i, 6);
        }

        canvas.enabled = false;
        RunTest();
    }

    private void RunTest()
    {
        TestRookLocations();
        TestKingBetweenRooks();
        TestBishopLocations();
        TestKnghtsPositions();
        TestQueenPosition();
    }

    private void TestRookLocations()
    {
        print("Rook2 position minus the Rook1 position is >= 2 Leaving space between for the King " +  (randomRook2 - randomRook1 >= 2));   
    }

    private void TestKingBetweenRooks()
    {
        print("Rook1 Position is less than the Kings Position that is less than the Rook2 position: " + (randomRook1 < randomKing && randomKing < randomRook2) + " The King is inbetween both rooks");
    }

    private void TestBishopLocations()
    {
        print("Bishop 1 positon: " + randomBishop1 + "-- Bishop 2 position: " + randomBishop2 + "-- The bishops are on oposite squares");
    }

    private void TestKnghtsPositions()
    {
        print("Knight1 Position: " + randomKnight1 + " and Knight2 Position: " + randomKnight2 + " spawned in available spaces");
    }

    private void TestQueenPosition()
    {
        print("Queen spawned in the last available Position: " + randomQueen);
    }



    private void PlaceRooks960()
    {
        randomRook1 = Random.Range(0, 5);

        AddPiece(whiteRook, white, randomRook1, 0);
        AddPiece(blackRook, black, randomRook1, 7);

        randomRook2 = Random.Range(randomRook1 +2 , 7);

        AddPiece(whiteRook, white, randomRook2, 0);
        AddPiece(blackRook, black, randomRook2, 7);
    }

    private void PlaceKing960()
    {
        randomKing = Random.Range(randomRook1 + 1, randomRook2);
        AddPiece(whiteKing, white, randomKing, 0);
        AddPiece(blackKing, black, randomKing, 7);
    }
    private void PlaceBishops960()
    {
        bool pieceOneIsEven = true;
        bool piecePlaced = false;
        randomBishop1 = Random.Range(0, 7);
        randomBishop2 = Random.Range(0, 7);


        while(!piecePlaced)
        {
            if(!pieces[randomBishop1, 0])
            {
                pieceOneIsEven = randomBishop1 % 2 == 0;
                AddPiece(whiteBishop, white, randomBishop1, 0);
                AddPiece(blackBishop, black, randomBishop1, 7);
                piecePlaced = true;
            }
            else
            {
                randomBishop1 = Random.Range(0, 7);
            }
        }

        piecePlaced = false;

        while (!piecePlaced)
        {
            if((pieceOneIsEven && randomBishop2 % 2 != 0) || (!pieceOneIsEven && randomBishop2 % 2 == 0))
            {
                if (!pieces[randomBishop2, 0])
                {              
                    AddPiece(whiteBishop, white, randomBishop2, 0);
                    AddPiece(blackBishop, black, randomBishop2, 7);
                    piecePlaced = true;
                }
                else
                {
                    randomBishop2 = Random.Range(0, 7);
                }
            }
            else
            {
                randomBishop2 = Random.Range(0, 7);
            }
        }
    }

    private void PlaceRestOfPieces960()
    {
        bool knight1 = false;
        bool knight2 = false;

        for (int i = 0; i < pieces.GetLength(0); i++)
        {
            if(!pieces[i,0])
            {
                if(!knight1)
                {
                    randomKnight1 = i;
                    AddPiece(whiteKnight, white, i, 0);
                    AddPiece(blackKnight, black, i, 7);
                    knight1 = true;
                }
                else if(!knight2)
                {
                    randomKnight2 = i;
                    AddPiece(whiteKnight, white, i, 0);
                    AddPiece(blackKnight, black, i, 7);
                    knight2 = true;
                }
                else
                {
                    randomQueen = i;
                    AddPiece(whiteQueen, white, i, 0);
                    AddPiece(blackQueen, black, i, 7);
                    break;
                }
            }
        }
    }



    public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        player.pieces.Add(pieceObject);
        pieces[col, row] = pieceObject;
    }

    public void SelectPieceAtGrid(Vector2Int gridPoint)
    {
        GameObject selectedPiece = pieces[gridPoint.x, gridPoint.y];
        if (selectedPiece)
        {
            board.SelectPiece(selectedPiece);
        }
    }

    public List<Vector2Int> MovesForPiece(GameObject pieceObject)
    {
        Piece piece = pieceObject.GetComponent<Piece>();
        Vector2Int gridPoint = GridForPiece(pieceObject);
        List<Vector2Int> locations = piece.MoveLocations(gridPoint);

        // filter out offboard locations
        locations.RemoveAll(gp => gp.x < 0 || gp.x > 7 || gp.y < 0 || gp.y > 7);

        // filter out locations with friendly piece
        locations.RemoveAll(gp => FriendlyPieceAt(gp));

        return locations;
    }

    public void Move(GameObject piece, Vector2Int gridPoint)
    {
        Piece pieceComponent = piece.GetComponent<Piece>();
        if (pieceComponent.type == PieceType.Pawn && !HasPawnMoved(piece))
        {
            movedPawns.Add(piece);
        }

        Vector2Int startGridPoint = GridForPiece(piece);
        pieces[startGridPoint.x, startGridPoint.y] = null;
        pieces[gridPoint.x, gridPoint.y] = piece;
        board.MovePiece(piece, gridPoint);
    }

    public void PawnMoved(GameObject pawn)
    {
        movedPawns.Add(pawn);
    }

    public bool HasPawnMoved(GameObject pawn)
    {
        return movedPawns.Contains(pawn);
    }

    public void CapturePieceAt(Vector2Int gridPoint)
    {
        GameObject pieceToCapture = PieceAtGrid(gridPoint);
        if (pieceToCapture.GetComponent<Piece>().type == PieceType.King)
        {
            Debug.Log(currentPlayer.name + " wins!");
            Destroy(board.GetComponent<TileSelector>());
            Destroy(board.GetComponent<MoveSelector>());
        }
        currentPlayer.capturedPieces.Add(pieceToCapture);
        pieces[gridPoint.x, gridPoint.y] = null;
        Destroy(pieceToCapture);
    }

    public void SelectPiece(GameObject piece)
    {
        board.SelectPiece(piece);
    }

    public void DeselectPiece(GameObject piece)
    {
        board.DeselectPiece(piece);
    }

    public bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        return currentPlayer.pieces.Contains(piece);
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
        {
            return null;
        }
        return pieces[gridPoint.x, gridPoint.y];
    }

    public Vector2Int GridForPiece(GameObject piece)
    {
        for (int i = 0; i < 8; i++) 
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    public bool FriendlyPieceAt(Vector2Int gridPoint)
    {
        GameObject piece = PieceAtGrid(gridPoint);

        if (piece == null) {
            return false;
        }

        if (otherPlayer.pieces.Contains(piece))
        {
            return false;
        }

        return true;
    }

    public void NextPlayer()
    {
        Player tempPlayer = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = tempPlayer;
    }
}
