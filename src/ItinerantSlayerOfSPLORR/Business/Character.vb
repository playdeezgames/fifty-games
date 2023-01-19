Friend Class Character
    Implements ICharacter
    Friend _data As CharacterData
    Sub New(data As CharacterData)
        _data = data
    End Sub

    Public ReadOnly Property CharacterType As CharacterType Implements ICharacter.CharacterType
        Get
            Return _data.CharacterType
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements ICharacter.Items
        Get
            Return _data.Items.Select(Function(x) New Item(x))
        End Get
    End Property

    Public Sub Take(item As IItem) Implements ICharacter.Take
        Dim actualItem = DirectCast(item, Item)
        If actualItem Is Nothing Then
            Throw New NotImplementedException
        End If
        _data.Items.Add(actualItem._data)
    End Sub

    Public Function CanTake(item As IItem) As Boolean Implements ICharacter.CanTake
        Return True
    End Function
End Class
