Imports System
Class UserInterface
    Public Property Value As String
End Class

Module Program
    Sub Main(args As String())
        Console.WriteLine("Please enter a value")
        Dim userInterface = New UserInterface()
        userInterface.Value = Console.ReadLine()
        Console.WriteLine($"You enter {userInterface.Value}")
    End Sub
End Module
