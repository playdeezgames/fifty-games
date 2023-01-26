Public Structure ItemTypeDescriptor
    Public Property Name As String
    Public Property CanUse As Boolean
    Public Property CanEquip As Boolean
    Public Property UseBy As Func(Of ICharacter, IEnumerable(Of String))
End Structure
