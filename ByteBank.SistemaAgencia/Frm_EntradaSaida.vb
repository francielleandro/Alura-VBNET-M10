Imports System.IO
Imports System.Text
Public Class Frm_EntradaSaida
    Public Sub New()

        ' Esta chamada é requerida pelo designer.
        InitializeComponent()

        ' Adicione qualquer inicialização após a chamada InitializeComponent().

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox2.Text = ""

        Dim EnderecoDoArquivo As String = "Tempo.txt"

        Dim FluxoDoArquivo As New FileStream(EnderecoDoArquivo, FileMode.Open)

        Dim Buffer = New Byte(1024) {} ' 1Kbyte

        Dim NumeroBytesLidos As Integer = -1

        While NumeroBytesLidos <> 0
            NumeroBytesLidos = FluxoDoArquivo.Read(Buffer, 0, 1024)
            TextBox1.AppendText(EscreverBuffer(Buffer))
            TextBox2.AppendText(EscreveBufferUTF(Buffer))
        End While
    End Sub

    Function EscreverBuffer(Buffer As Byte()) As String

        Dim vRetorno As String = ""
        For Each MeuByte As Byte In Buffer
            vRetorno += MeuByte.ToString + " "
        Next
        Return vRetorno

    End Function

    Function EscreveBufferUTF(Buffer As Byte()) As String

        Dim vRetorno As String
        'Dim UTF As New UTF8Encoding()

        'Dim UTF As New UTF8Encoding()

        Dim UTF As New UTF8Encoding()

        vRetorno = UTF.GetString(Buffer)
        Return vRetorno

    End Function
End Class