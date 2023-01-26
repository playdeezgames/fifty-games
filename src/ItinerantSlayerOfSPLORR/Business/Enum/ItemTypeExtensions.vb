Imports System.Runtime.CompilerServices

Friend Module ItemTypeExtensions
    <Extension>
    Friend Function Name(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.Potion
                Return "Potion"
            Case ItemType.EmptyBottle
                Return "Empty Bottle"
            Case ItemType.Sword
                Return "Sword"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Friend Function CanEquip(itemType As ItemType) As Boolean
        Select Case itemType
            Case ItemType.Potion, ItemType.EmptyBottle
                Return False
            Case ItemType.Sword
                Return True
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Friend Function CanUse(itemType As ItemType) As Boolean
        Select Case itemType
            Case ItemType.Potion
                Return True
            Case ItemType.EmptyBottle
                Return False
            Case ItemType.Sword
                Return False
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Friend Function UseBy(itemType As ItemType, character As ICharacter) As IEnumerable(Of String)
        character.RemoveItems(itemType, 1)
        Select Case itemType
            Case ItemType.Potion
                Return UsePotion(character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Function UsePotion(character As ICharacter) As IEnumerable(Of String)
        Dim messages As New List(Of String)
        Const HealingAmount = 5
        Dim hpBefore = character.HitPoints
        character.Heal(HealingAmount)
        Dim actualHealing = character.HitPoints - hpBefore
        messages.Add($"{character.Name} drinks the potion.")
        messages.Add($"{character.Name} gains {actualHealing} HP.")
        character.AddItems(ItemType.EmptyBottle, 1)
        Return messages
    End Function
End Module
