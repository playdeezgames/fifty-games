Imports System.Buffers

Friend Class World
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

    Public Property Encounter As IEncounter Implements IWorld.Encounter
        Get
            If _data.Encounter Is Nothing Then
                Return Nothing
            End If
            Return New Encounter(_data, _data.Encounter)
        End Get
        Set(value As IEncounter)
            If value Is Nothing Then
                _data.Encounter = Nothing
                Return
            End If
            _data.Encounter = DirectCast(value, Encounter)._data
        End Set
    End Property

    Public ReadOnly Property IsInAnEncounter As Boolean Implements IWorld.IsInAnEncounter
        Get
            Return _data.Encounter IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property Inn As IInn Implements IWorld.Inn
        Get
            If Not PlayerCharacter.IsInInn Then
                Return Nothing
            End If
            Dim cell = PlayerBoard.GetCell(_data.PlayerData.BoardColumn, _data.PlayerData.BoardRow)
            If cell.Triggers Is Nothing Then
                Return Nothing
            End If
            Return cell.Triggers(Player.TriggerIndex).Inn
        End Get
    End Property

    Public ReadOnly Property Shoppe As IShoppe Implements IWorld.Shoppe
        Get
            If Not PlayerCharacter.IsInShoppe Then
                Return Nothing
            End If
            Dim cell = PlayerBoard.GetCell(_data.PlayerData.BoardColumn, _data.PlayerData.BoardRow)
            If cell.Triggers Is Nothing Then
                Return Nothing
            End If
            Return cell.Triggers(Player.TriggerIndex).Shoppe
        End Get
    End Property

    Public ReadOnly Property Message As IMessage Implements IWorld.Message
        Get
            If Not PlayerCharacter.IsInMessage Then
                Return Nothing
            End If
            Dim cell = PlayerBoard.GetCell(_data.PlayerData.BoardColumn, _data.PlayerData.BoardRow)
            If cell.Triggers Is Nothing Then
                Return Nothing
            End If
            Return cell.Triggers(Player.TriggerIndex).Message
        End Get
    End Property

    Public ReadOnly Property HasMoreTriggers As Boolean Implements IWorld.HasMoreTriggers
        Get
            Dim cell = PlayerBoard.GetCell(_data.PlayerData.BoardColumn, _data.PlayerData.BoardRow)
            Return Player.TriggerIndex < cell.Triggers.Count
        End Get
    End Property

    Friend Sub StartGame() Implements IWorld.StartGame
        AbandonGame()
        InitializeBoards()
        InitializePlayer()
    End Sub

    Private Sub InitializePlayer()
        _data.PlayerData = New PlayerData With {.BoardIndex = 0, .BoardColumn = 7, .BoardRow = 3}
    End Sub
    Private Sub InitializeBoards()
        CreateBoard(Overworld.defaultTerrain, Overworld.map, Overworld.characters, Overworld.triggers, Overworld.encounterZones)
        CreateBoard(Town.defaultTerrain, Town.map, Town.characters, Town.triggers, Town.encounterZones)
    End Sub

    Private Function CreateBoard(defaultTerrain As TerrainType, map As IReadOnlyList(Of String), characters As IReadOnlyList(Of (CharacterType, Integer, Integer)), triggers As IReadOnlyList(Of (IReadOnlyList(Of TriggerData), Integer, Integer)), encounterZones As IReadOnlyList(Of EncounterZoneData)) As IBoard
        Dim columns = map(0).Length
        Dim rows = map.Count
        Dim random As New Random
        Dim boardData As New BoardData() With {.EncounterZones = encounterZones.ToList}
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
                    Case "?"c
                        boardCellData.Terrain = TerrainType.Sign
                    Case Else
                        Throw New NotImplementedException
                End Select
            End While
        End While
        For Each character In characters
            Dim characterData = New CharacterData() With
            {
                .CharacterType = character.Item1,
                .XP = 0,
                .IsInInn = False,
                .Jools = 0,
                .Wounds = 0,
                .Level = 1,
                .MaximumHitPoints = 0,
                .AttackStrength = 0,
                .DefendStrength = 0
            }
            boardData.BoardColumns(character.Item2).Cells(character.Item3).Character = characterData
            If characterData.CharacterType = CharacterType.Dude Then
                characterData.Equipment(EquipSlotType.Torso) = ItemType.Clothes
            End If
        Next
        For Each trigger In triggers
            boardData.BoardColumns(trigger.Item2).Cells(trigger.Item3).Triggers = trigger.Item1.ToList
        Next
        Return New Board(_data, boardIndex, boardData)
    End Function

    Friend Sub AbandonGame() Implements IWorld.AbandonGame
        _data.Boards.Clear()
        _data.PlayerData = Nothing
    End Sub

    Public Sub MoveNorth(random As Random) Implements IWorld.MoveNorth
        MovePlayer(random, 0, -1)
    End Sub

    Private Sub MovePlayer(random As Random, deltaX As Integer, deltaY As Integer)
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
        Dim character = currentCell.Character
        nextCell.Character = character
        currentCell.Character = Nothing
        PlayerBoard = nextBoard
        Player.X = nextX
        Player.Y = nextY
        Player.TriggerIndex = 0
        RunTrigger()
        Encounter = PlayerBoard.CheckForEncounter(random, Player.X, Player.Y)
    End Sub

    Public Sub MoveSouth(random As Random) Implements IWorld.MoveSouth
        MovePlayer(random, 0, 1)
    End Sub

    Public Sub MoveWest(random As Random) Implements IWorld.MoveWest
        MovePlayer(random, -1, 0)
    End Sub

    Public Sub MoveEast(random As Random) Implements IWorld.MoveEast
        MovePlayer(random, 1, 0)
    End Sub

    Public Sub FleeEncounter() Implements IWorld.FleeEncounter
        If Not IsInAnEncounter Then
            Return
        End If
        'TODO: roll some dice or something
        Encounter = Nothing
    End Sub

    Public Function Attack(enemy As IEnemy, random As Random) As IEnumerable(Of String) Implements IWorld.Attack
        Dim messages As New List(Of String)
        Dim character = PlayerCharacter
        messages.AddRange(character.Attack(enemy, random))
        EnemyResponse(random, messages, character)
        Return messages
    End Function

    Private Sub EnemyResponse(random As Random, messages As List(Of String), character As ICharacter)
        If Encounter Is Nothing Then
            Return
        End If
        Encounter.PurgeCorpses()
        If Encounter.Enemies.Any Then
            messages.AddRange(Encounter.CounterAttack(character, random))
        Else
            messages.Add($"{character.Name} has defeated all enemies!")
            _data.Encounter = Nothing
        End If
    End Sub

    Public Function UseItem(itemType As ItemType, random As Random) As IEnumerable(Of String) Implements IWorld.UseItem
        Dim messages As New List(Of String)
        Dim character = PlayerCharacter
        messages.AddRange(character.UseItem(itemType))
        EnemyResponse(random, messages, character)
        Return messages
    End Function

    Public Function EquipItem(itemType As ItemType) As IEnumerable(Of String) Implements IWorld.EquipItem
        Dim messages As New List(Of String)
        Dim character = PlayerCharacter
        messages.AddRange(character.EquipItem(itemType))
        Return messages
    End Function

    Public Sub ProceedToNextTrigger() Implements IWorld.ProceedToNextTrigger
        If Not HasMoreTriggers Then
            Return
        End If
        Player.TriggerIndex += 1
        RunTrigger()
    End Sub

    Private Sub RunTrigger()
        If Not HasMoreTriggers Then
            Return
        End If
        Dim cell = PlayerBoard.GetCell(Player.X, Player.Y)
        Dim trigger = cell.Triggers(Player.TriggerIndex)
        If Not trigger.IsActive Then
            ProceedToNextTrigger()
            Return
        End If
        Select Case trigger.TriggerType
            Case TriggerType.Shoppe
                PlayerCharacter.IsInShoppe = True
            Case TriggerType.Message
                PlayerCharacter.IsInMessage = True
            Case TriggerType.Teleport
                Dim character = PlayerBoard.GetCell(Player.X, Player.Y).Character
                PlayerBoard.GetCell(Player.X, Player.Y).Character = Nothing
                PlayerBoard = trigger.Teleport.DestinationBoard
                Player.X = trigger.Teleport.DestinationX
                Player.Y = trigger.Teleport.DestinationY
                PlayerBoard.GetCell(Player.X, Player.Y).Character = character
                Player.TriggerIndex = 0
                RunTrigger()
            Case TriggerType.Inn
                PlayerCharacter.IsInInn = True
            Case TriggerType.SetFlag
                _data.Flags.Add(trigger.Flag)
                ProceedToNextTrigger()
            Case TriggerType.ClearFlag
                _data.Flags.Remove(trigger.Flag)
                ProceedToNextTrigger()
            Case TriggerType.StopTriggers
                Player.TriggerIndex = Integer.MaxValue
            Case TriggerType.RemoveItems
                PlayerCharacter.RemoveItems(trigger.ItemRemoval.ItemType, trigger.ItemRemoval.ItemCount)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
End Class
