Imports System.Runtime.CompilerServices

Friend Module ItemTypeExtensions
    <Extension>
    Friend Function Name(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.Potion
                Return "Potion"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
