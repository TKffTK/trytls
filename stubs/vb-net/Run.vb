Imports System
Imports System.Net
Imports System.IO

Module Run
  Sub Main()  ' or args as String
    Dim arguments As String() = Environment.GetCommandLineArgs()
    Dim host, port as String

    if arguments.Length = 3 Then
      host = arguments(1)
      port = arguments(2)
    Else
      Console.WriteLine ("UNSUPPORTED") ' for now
      Environment.Exit(0)
    End if

    Dim sURL As String
    sURL = "https://" & host & ":" & port

    Dim wrGETURL As WebRequest
    wrGETURL = WebRequest.Create(sURL)

    Try
      wrGETURL.GetResponse()
      Console.WriteLine ("VERIFY SUCCESS")
    Catch ex As System.Net.WebException
      if ex.ToString.Contains("NameResolutionFailure") Then
        Throw ex
        Environment.Exit(1)
      Else
        Console.WriteLine ("VERIFY FAILURE")
      End if
    Catch ex As Exception
      Throw ex
      Environment.Exit(1)
    End Try
    Environment.Exit(0)
  End Sub
End Module
