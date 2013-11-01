VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   5805
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7755
   LinkTopic       =   "Form1"
   ScaleHeight     =   5805
   ScaleWidth      =   7755
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command2 
      Caption         =   "Command2"
      Height          =   375
      Left            =   2880
      TabIndex        =   2
      Top             =   4920
      Width           =   1455
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   375
      Left            =   840
      TabIndex        =   1
      Top             =   4800
      Width           =   1095
   End
   Begin Project1.MatrixView MatrixView1 
      Height          =   4335
      Left            =   240
      TabIndex        =   0
      Top             =   360
      Width           =   5775
      _ExtentX        =   11033
      _ExtentY        =   8281
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim b() As Byte
Dim m As New Matrix
Private Sub Command1_Click()
    m.GetBytes b, 0
End Sub

Private Sub Command2_Click()
    m.SetBytes b, 0
End Sub

Private Sub Form_Load()
    ReDim b(2) As Byte
    m.Initialize 2, 3
    m.SetValue 1, 1, 1
    m.SetValue 1, 2, 2
    m.SetValue 1, 2, 2
    'MsgBox m.GetValue(1, 4)
        
    MatrixView1.Matrix = m

End Sub

