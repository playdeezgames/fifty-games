Friend Class BoardCell
    Implements IBoardCell
    Private _worldData As WorldData
    Private _data As BoardCellData
    Sub New(worldData As WorldData, data As BoardCellData)
        _worldData = worldData
        _data = data
    End Sub

    Public ReadOnly Property Terrain As TerrainType Implements IBoardCell.Terrain
        Get
            Return _data.Terrain
        End Get
    End Property

    Public Property Character As ICharacter Implements IBoardCell.Character
        Get
            If _data.Character Is Nothing Then
                Return Nothing
            End If
            Return New Character(_data.Character)
        End Get
        Set(value As ICharacter)
            If value Is Nothing Then
                _data.Character = Nothing
                Return
            End If
            Dim fromCharacter = TryCast(value, Character)
            If fromCharacter Is Nothing Then
                Throw New NotImplementedException
            End If
            _data.Character = fromCharacter._data
        End Set
    End Property

    Public ReadOnly Property Triggers As IEnumerable(Of ITrigger) Implements IBoardCell.Triggers
        Get
            Return _data.Triggers.Select(Function(x) New Trigger(_worldData, x))
        End Get
    End Property
End Class
