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

    Public ReadOnly Property Character As ICharacter Implements IBoardCell.Character
        Get
            If _data.Character Is Nothing Then
                Return Nothing
            End If
            Return New Character(_data.Character)
        End Get
    End Property
End Class
