Friend Class World
    Private _data As WorldData
    Sub New(data As WorldData)
        _data = data
    End Sub
    Public ReadOnly Property PlayerBoard As Board
        Get
            Return New Board(_data.Boards(_data.PlayerData.BoardIndex))
        End Get
    End Property

    Public ReadOnly Property CanContinue As Boolean
        Get
            Return _data.PlayerData IsNot Nothing
        End Get
    End Property
    Public ReadOnly Property CanAbandon As Boolean
        Get
            Return CanContinue
        End Get
    End Property
    Public ReadOnly Property CanStart As Boolean
        Get
            Return Not CanContinue
        End Get
    End Property

    Friend Sub StartGame()
        AbandonGame()
        InitializeBoards()
        InitializePlayer()
    End Sub

    Private Sub InitializePlayer()
        _data.PlayerData = New PlayerData With {.BoardIndex = 0, .BoardColumn = 0, .BoardRow = 0}
    End Sub

    Private Sub InitializeBoards()
        _data.Boards.Add(New BoardData())
        _data.Boards(0).BoardColumns.Add(New BoardColumnData())
        _data.Boards(0).BoardColumns(0).Cells.Add(New BoardCellData())
        _data.Boards(0).BoardColumns(0).Cells(0).Terrain = TerrainType.None
        _data.Boards(0).BoardColumns(0).Cells(0).Character = New CharacterData With {
            .CharacterType = CharacterType.Dude
        }
    End Sub

    Friend Sub AbandonGame()
        _data.Boards.Clear()
        _data.PlayerData = Nothing
    End Sub
End Class
