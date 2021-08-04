Imports System.IO
Imports System.Text
Imports ByteBank.Bibliotecas.Classes.Clientes
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
        Using FluxoDoArquivo As New FileStream(EnderecoDoArquivo, FileMode.Open)
            TextBox1.Text = ""
            TextBox2.Text = ""
            Dim Buffer = New Byte(1024) {} ' 1Kbyte
            Dim NumeroBytesLidos As Integer = -1
            While NumeroBytesLidos <> 0
                NumeroBytesLidos = FluxoDoArquivo.Read(Buffer, 0, 1024)
                TextBox1.AppendText(EscreverBuffer(Buffer))
                TextBox2.AppendText(EscreveBufferUTF(Buffer))
            End While
        End Using
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim EnderecoDoArquivo As String = "Tempo.txt"
        TextBox2.Text = ""
        Using FluxoDoArquivo As New FileStream(EnderecoDoArquivo, FileMode.Open)
            Using Leitor = New StreamReader(FluxoDoArquivo)
                While Not (Leitor.EndOfStream)
                    Dim vLinha As String = Leitor.ReadLine()
                    Dim CC As ContaCorrente = CriarContaCorrente(vLinha)
                    TextBox2.AppendText(CC.ToString + vbCrLf)
                End While
            End Using
        End Using

    End Sub

    Function CriarContaCorrente(Linha As String) As ContaCorrente

        Dim VetorLinha() As String = Linha.Split(";")
        Dim Cliente As New Cliente()
        Cliente.cpf = VetorLinha(3)
        Cliente.nome = VetorLinha(4)

        Dim CC As New ContaCorrente(VetorLinha(0), VetorLinha(1))
        CC.titular = Cliente
        Dim SaldoConvertido As Double = Double.Parse(VetorLinha(2))
        CC.Depositar(SaldoConvertido - 100)

        Return CC

    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim CaminhoNovoArquivo As String = "ContasExternas.csv"
        Using FluxoDoArquivo As New FileStream(CaminhoNovoArquivo, FileMode.Create)
            Dim ContaString As String = "456;78945;4785,9;221122323;Gustavo Santos"
            Dim UTF As New UTF8Encoding()
            Dim Bytes = UTF.GetBytes(ContaString)
            FluxoDoArquivo.Write(Bytes, 0, Bytes.Length)
        End Using
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim CaminhoNovoArquivo As String = "ContasExternas.csv"
        Using FluxoDoArquivo As New FileStream(CaminhoNovoArquivo, FileMode.Create)
            Using Escritor = New StreamWriter(FluxoDoArquivo, Encoding.UTF8)
                Dim ContaString As String = "456;78945;4785,9;221122323;José da Silva"
                Escritor.Write(ContaString)
            End Using
        End Using
    End Sub
End Class