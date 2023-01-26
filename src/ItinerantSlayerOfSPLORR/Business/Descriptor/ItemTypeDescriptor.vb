Public Structure ItemTypeDescriptor
    Public Property Name As String
    Public Property CanUse As Boolean
    Public Property CanEquip As Boolean
    Public Property UseBy As Func(Of ICharacter, IEnumerable(Of String))
    Public Property EquipSlot As EquipSlotType?
    Public Property AttackStrength As Integer
End Structure
