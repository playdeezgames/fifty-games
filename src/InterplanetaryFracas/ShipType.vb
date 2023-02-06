Imports System.Runtime.CompilerServices

Public Enum ShipType
    MarkI
    MarkII
    MarkIII
    MarkIV
End Enum
Public Module ShipTypeExtensions
    <Extension>
    Function GetStartingTorpedos(shipType As ShipType) As Integer
        Select Case shipType
            Case ShipType.MarkI
                Return 1
            Case ShipType.MarkII
                Return 3
            Case ShipType.MarkIII
                Return 6
            Case ShipType.MarkIV
                Return 10
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
