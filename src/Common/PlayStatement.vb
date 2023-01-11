Public Module PlayStatement
    Private ReadOnly noteTable As New Dictionary(Of String, Integer) From
        {
            {"D1", 37},
            {"D#1", 39},
            {"E1", 41},
            {"F1", 44},
            {"F#1", 46},
            {"G1", 49},
            {"G#1", 52},
            {"A1", 55},
            {"A#1", 58},
            {"B1", 62},
            {"C2", 65},
            {"C#2", 69},
            {"D2", 73},
            {"D#2", 78},
            {"E2", 82},
            {"F2", 87},
            {"F#2", 93},
            {"G2", 98},
            {"G#2", 104},
            {"A2", 110},
            {"A#2", 117},
            {"B2", 123},
            {"C3", 131},
            {"C#3", 139},
            {"D3", 147},
            {"D#3", 156},
            {"E3", 165},
            {"F3", 175},
            {"F#3", 185},
            {"G3", 196},
            {"G#3", 208},
            {"A3", 220},
            {"A#3", 233},
            {"B3", 247},
            {"C4", 262},
            {"C#4", 277},
            {"D4", 294},
            {"D#4", 311},
            {"E4", 330},
            {"F4", 349},
            {"F#4", 370},
            {"G4", 392},
            {"G#4", 415},
            {"A4", 440},
            {"A#4", 466},
            {"B4", 494},
            {"C5", 523},
            {"C#5", 554},
            {"D5", 587},
            {"D#5", 622},
            {"E5", 659},
            {"F5", 698},
            {"F#5", 740},
            {"G5", 784},
            {"G#5", 831},
            {"A5", 880},
            {"A#5", 932},
            {"B5", 988}
        }
    Public Sub Play(song As String)
        Dim notes = song.Split(";")
        Dim duration As Integer = 200
        For Each note In notes
            If note.StartsWith("L") Then
                duration = CInt(note.Substring(1))
            ElseIf note.StartsWith("R") Then
                Threading.Thread.Sleep(CInt(note.Substring(1)))
            Else
#Disable Warning CA1416 ' Validate platform compatibility
                Console.Beep(noteTable(note), duration)
#Enable Warning CA1416 ' Validate platform compatibility
            End If
        Next
    End Sub
End Module