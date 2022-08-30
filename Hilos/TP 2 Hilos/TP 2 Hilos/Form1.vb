Imports System.Threading
Imports System.IO.StreamWriter
Imports System.IO
Imports System.Text
Imports System.dll

Public Class Form1
    Inherits System.Windows.Forms.Form

    <STAThread()>
    Public Shared Sub Main()
        'crear 2 hilos diferentes, cada hilo se enlaza con un método para iniciar los formularios
        Dim hilo1 As New Thread(AddressOf Proceso1)
        Dim hilo2 As New Thread(AddressOf Proceso2)
        'después se arrancan los 2 hilos
        hilo1.Start()
        hilo2.Start()
    End Sub

    Public Shared Sub Proceso1()
        Dim Texto As String = Form1.TextBox2.Text
        Proceso2(Texto)
    End Sub
    'procedimiento que muestra el segundo formulario
    Public Shared Sub Proceso2(Aux As String)



        'Form1.TextBox2.Text

        Dim Palabras As String() = Form1.TextBox2.Text.Split(" ")
        Dim Palabra As String = ""
        Dim Contador As Integer = 0

        Dim Linea As String = ""
        Contador = 0
        For Each Palabra In Palabras
            Linea = Linea & Palabra & " "
            Contador += 1
            'Contador += Palabra.Length

        Next
        'MsgBox(Palabra)

        Dim lineas As String() = Form1.TextBox2.Lines()
        Contador -= 1
        For Each lin As String In lineas
            Dim values As Object() = {lin}
            'MsgBox(lin)
            Contador += 1
        Next

        'Form1.ProgressBar1.Value = Contador
        'Form1.Label2.Text = (Contador / Max) * 100 & "%"
        'Form1.Label2.Refresh()
        'Threading.Thread.Sleep(100)

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Proceso1()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Val(TextBox1.Text) > 0 Then
            Button2.Visible = False
            Button1.Visible = True
            TextBox2.Enabled = True
            TextBox1.Enabled = False
        Else
            MsgBox("Inserte cantidad maxima de lineas de texto")
        End If

        BackgroundWorker1.RunWorkerAsync()

        Dim Max As Integer = Val(TextBox1.Text)
        ProgressBar1.Maximum = Max

        Dim path As String = "C:\Users\PC1\Desktop\Hilos.txt"

        ' Create or overwrite the file.
        Dim fs As FileStream = File.Create(path)

        ' Add text to the file.
        Dim info As Byte()

        For a = 1 To Max
            info = New UTF8Encoding(True).GetBytes("Linea " + Str(a) + vbCrLf)
            fs.Write(info, 0, info.Length)
        Next

        fs.Close()

    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        TextBox1.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Dim i As Integer

        For i = 1 To 1000
            BackgroundWorker1.WorkerReportsProgress = True
            BackgroundWorker1.ReportProgress(i / 10)

            Threading.Thread.Sleep(25)

        Next

    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

        If e.ProgressPercentage <= Val(TextBox1.Text) Then
            ProgressBar1.Value = e.ProgressPercentage
        End if

    End Sub



    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        MsgBox("Terminado")
        me.hide()

    End Sub

End Class
