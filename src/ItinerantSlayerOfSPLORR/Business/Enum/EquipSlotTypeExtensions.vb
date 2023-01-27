Imports System.Runtime.CompilerServices

Friend Module EquipSlotTypeExtensions
    <Extension>
    Friend Function Name(equipSlot As EquipSlotType) As String
        Select Case equipSlot
            Case EquipSlotType.Weapon
                Return "Weapon"
            Case EquipSlotType.Torso
                Return "Torso"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
