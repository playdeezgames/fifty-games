Public Class CharacterData
    Public Property CharacterType As CharacterType
    Public Property XP As Integer
    Public Property Wounds As Integer
    Public Property IsInInn As Boolean
    Public Property Jools As Integer
    Public Property Level As Integer
    Public Property MaximumHitPoints As Integer
    Public Property DefendStrength As Integer
    Public Property AttackStrength As Integer
    Public Property IsInShoppe As Boolean
    Public Property Inventory As New Dictionary(Of ItemType, Integer)
    Public Property Equipment As New Dictionary(Of EquipSlotType, ItemType)
    Public Property IsInMessage As Boolean
End Class
