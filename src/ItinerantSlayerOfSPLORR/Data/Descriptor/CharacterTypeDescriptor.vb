Friend Class CharacterTypeDescriptor
    Property Name As String
    Property Attack As Integer
    Property HitPoints As Integer
    Property Defend As Integer
    Property XPGoal As Func(Of Integer, Integer)
    Public Property HitPointIncrease As Integer
    Public Property AttackStrengthIncrease As Integer
    Public Property DefendStrengthIncrease As Integer
End Class
