Friend Class BoardCell
    Implements IBoardCell
    Private _data As BoardCellData
    Sub New(data As BoardCellData)
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

    Public Property Item As IItem Implements IBoardCell.Item
        Get
            If _data.Item Is Nothing Then
                Return Nothing
            End If
            Return New Item(_data.Item)
        End Get
        Set(value As IItem)
            Throw New NotImplementedException()
        End Set
    End Property
End Class
