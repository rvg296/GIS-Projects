Imports System.Drawing.Image
Public Class Final_Button
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

        Dim x As FindTheBustopToGo = New FindTheBustopToGo
        x.BackgroundImage = FromFile("C:\Users\rxm160030\Desktop\logo_dart.png")
        x.Show()

    End Sub

    Protected Overrides Sub OnUpdate()
        Enabled = My.ArcMap.Application IsNot Nothing
    End Sub
End Class
