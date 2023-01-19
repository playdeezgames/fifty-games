Friend Class Character
    Implements ICharacter
    Private _data As CharacterData
    Sub New(data As CharacterData)
        _data = data
    End Sub

    Public ReadOnly Property CharacterType As CharacterType Implements ICharacter.CharacterType
        Get
            Return _data.CharacterType
        End Get
    End Property
End Class
