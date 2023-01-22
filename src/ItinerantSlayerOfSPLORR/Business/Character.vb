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

    Public ReadOnly Property Name As String Implements ICharacter.Name
        Get
            Return _data.CharacterType.ToDescriptor().Name
        End Get
    End Property

    Public ReadOnly Property XP As Integer Implements ICharacter.XP
        Get
            Return _data.XP
        End Get
    End Property

    Public Sub AddXP(amount As Integer) Implements ICharacter.AddXP
        _data.XP += amount
    End Sub

    Public Function RollAttack(random As Random) As Integer Implements ICharacter.RollAttack
        Return random.Next(_data.CharacterType.ToDescriptor().Attack)
    End Function
End Class
