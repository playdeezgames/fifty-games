﻿Friend Class World
    Implements IWorld
    Private _data As WorldData
    Sub New(data As WorldData)
        _data = data
    End Sub
    Public ReadOnly Property PlayerBoard As IBoard Implements IWorld.PlayerBoard
        Get
            Return New Board(_data.Boards(_data.PlayerData.BoardIndex))
        End Get
    End Property

    Public ReadOnly Property CanContinue As Boolean Implements IWorld.CanContinue
        Get
            Return _data.PlayerData IsNot Nothing
        End Get
    End Property
    Public ReadOnly Property CanAbandon As Boolean Implements IWorld.CanAbandon
        Get
            Return CanContinue
        End Get
    End Property
    Public ReadOnly Property CanStart As Boolean Implements IWorld.CanStart
        Get
            Return Not CanContinue
        End Get
    End Property

    Public ReadOnly Property Player As IPlayer Implements IWorld.Player
        Get
            Return New Player(_data.PlayerData)
        End Get
    End Property

    Friend Sub StartGame() Implements IWorld.StartGame
        AbandonGame()
        InitializeBoards()
        InitializePlayer()
    End Sub

    Private Sub InitializePlayer()
        _data.PlayerData = New PlayerData With {.BoardIndex = 0, .BoardColumn = 6, .BoardRow = 3}
    End Sub

    Private ReadOnly level As IReadOnlyList(Of String) = New List(Of String) From
        {
            "≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈",
            "≈≈≈≈≈≈......................................≈≈≈≈≈≈",
            "≈≈≈≈..........................................≈≈≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈..............................................≈≈",
            "≈≈≈≈..........................................≈≈≈≈",
            "≈≈≈≈≈≈......................................≈≈≈≈≈≈",
            "≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈"
        }

    Private Sub InitializeBoards()
        Dim board As IBoard = CreateBoard(level(0).Length, level.Count)

    End Sub

    Private Function CreateBoard(columns As Integer, rows As Integer) As IBoard
        Dim boardData As New BoardData()
        _data.Boards.Add(boardData)
        While boardData.BoardColumns.Count < columns
            Dim column = boardData.BoardColumns.Count
            Dim boardColumnData As New BoardColumnData
            boardData.BoardColumns.Add(boardColumnData)
            While boardColumnData.Cells.Count < rows
                Dim row = boardColumnData.Cells.Count
                Dim boardCellData = New BoardCellData
                boardColumnData.Cells.Add(boardCellData)
                Select Case level(row)(column)
                    Case "."c
                        boardCellData.Terrain = TerrainType.Grass
                    Case "≈"c
                        boardCellData.Terrain = TerrainType.Water
                End Select
            End While
        End While
        boardData.BoardColumns(6).Cells(3).Character = New CharacterData() With
        {
            .CharacterType = CharacterType.Dude
        }
        Return New Board(boardData)
    End Function

    Friend Sub AbandonGame() Implements IWorld.AbandonGame
        _data.Boards.Clear()
        _data.PlayerData = Nothing
    End Sub
End Class
