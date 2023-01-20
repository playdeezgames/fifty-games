﻿Friend Class World
    Implements IWorld
    Private _data As WorldData
    Sub New(data As WorldData)
        _data = data
    End Sub
    Public Property PlayerBoard As IBoard Implements IWorld.PlayerBoard
        Get
            Return New Board(_data, _data.PlayerData.BoardIndex, _data.Boards(_data.PlayerData.BoardIndex))
        End Get
        Set(value As IBoard)
            _data.PlayerData.BoardIndex = value.BoardIndex
        End Set
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

    Public ReadOnly Property PlayerCharacter As ICharacter Implements IWorld.PlayerCharacter
        Get
            Return PlayerBoard.GetCell(Player.X, Player.Y).Character
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
    Private Sub InitializeBoards()
        CreateBoard(Overworld.defaultTerrain, Overworld.map, Overworld.characters, Overworld.triggers)
        CreateBoard(Town.defaultTerrain, Town.map, Town.characters, Town.triggers)

    End Sub

    Private Function CreateBoard(defaultTerrain As TerrainType, map As IReadOnlyList(Of String), characters As IReadOnlyList(Of (CharacterType, Integer, Integer)), triggers As IReadOnlyList(Of (TriggerData, Integer, Integer))) As IBoard
        Dim columns = map(0).Length
        Dim rows = map.Count
        Dim random As New Random
        Dim boardData As New BoardData()
        Dim boardIndex = _data.Boards.Count
        _data.Boards.Add(boardData)
        boardData.DefaultTerrain = defaultTerrain
        While boardData.BoardColumns.Count < columns
            Dim column = boardData.BoardColumns.Count
            Dim boardColumnData As New BoardColumnData
            boardData.BoardColumns.Add(boardColumnData)
            While boardColumnData.Cells.Count < rows
                Dim row = boardColumnData.Cells.Count
                Dim boardCellData = New BoardCellData
                boardColumnData.Cells.Add(boardCellData)
                Select Case map(row)(column)
                    Case "."c
                        boardCellData.Terrain = TerrainType.Grass
                    Case "~"c
                        boardCellData.Terrain = TerrainType.Water
                    Case "!"c
                        boardCellData.Terrain = TerrainType.Home
                    Case "#"c
                        boardCellData.Terrain = TerrainType.Wall
                    Case " "c
                        boardCellData.Terrain = TerrainType.Empty
                    Case ","c
                        boardCellData.Terrain = TerrainType.Road
                    Case Else
                        Throw New NotImplementedException
                End Select
            End While
        End While
        For Each character In characters
            boardData.BoardColumns(character.Item2).Cells(character.Item3).Character = New CharacterData() With
            {
                .CharacterType = character.Item1
            }
        Next
        For Each trigger In triggers
            boardData.BoardColumns(trigger.Item2).Cells(trigger.Item3).Trigger = trigger.Item1
        Next
        Return New Board(_data, boardIndex, boardData)
    End Function

    Friend Sub AbandonGame() Implements IWorld.AbandonGame
        _data.Boards.Clear()
        _data.PlayerData = Nothing
    End Sub

    Public Sub MoveNorth() Implements IWorld.MoveNorth
        MovePlayer(0, -1)
    End Sub

    Private Sub MovePlayer(deltaX As Integer, deltaY As Integer)
        Dim currentCell = PlayerBoard.GetCell(Player.X, Player.Y)
        Dim nextBoard = PlayerBoard
        Dim nextX = Player.X + deltaX
        Dim nextY = Player.Y + deltaY
        Dim nextCell = PlayerBoard.GetCell(Player.X + deltaX, Player.Y + deltaY)
        If nextCell Is Nothing Then
            Return
        End If
        If nextCell.Character IsNot Nothing Then
            Return
        End If
        Select Case nextCell.Terrain
            Case TerrainType.Water, TerrainType.Wall
                Return
        End Select
        If nextCell.Trigger IsNot Nothing Then
            Select Case nextCell.Trigger.TriggerType
                Case TriggerType.Teleport
                    nextBoard = nextCell.Trigger.Teleport.DestinationBoard
                    nextX = nextCell.Trigger.Teleport.DestinationX
                    nextY = nextCell.Trigger.Teleport.DestinationY
                    nextCell = nextBoard.GetCell(nextX, nextY)
                Case TriggerType.GiveItem
            End Select
        End If
        Dim character = currentCell.Character
        nextCell.Character = character
        currentCell.Character = Nothing
        PlayerBoard = nextBoard
        Player.X = nextX
        Player.Y = nextY
    End Sub

    Public Sub MoveSouth() Implements IWorld.MoveSouth
        MovePlayer(0, 1)
    End Sub

    Public Sub MoveWest() Implements IWorld.MoveWest
        MovePlayer(-1, 0)
    End Sub

    Public Sub MoveEast() Implements IWorld.MoveEast
        MovePlayer(1, 0)
    End Sub
End Class
