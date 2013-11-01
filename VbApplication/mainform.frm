VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{1CAA19EA-8F51-47BC-A905-84B6983AD297}#1.0#0"; "DMatlabControl.ocx"
Begin VB.Form mainform 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Distributed Matlab Demonstration"
   ClientHeight    =   6375
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   6705
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   6375
   ScaleWidth      =   6705
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox port 
      Height          =   375
      Left            =   2280
      TabIndex        =   12
      Text            =   "6655"
      Top             =   3240
      Width           =   1095
   End
   Begin VB.TextBox ipaddress 
      Height          =   375
      Left            =   120
      TabIndex        =   11
      Text            =   "127.0.0.1"
      Top             =   3240
      Width           =   2055
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Random"
      Height          =   375
      Left            =   3480
      TabIndex        =   10
      Top             =   2760
      Width           =   855
   End
   Begin VB.CommandButton b_Refresh 
      Caption         =   "Refresh"
      Height          =   375
      Left            =   4440
      TabIndex        =   9
      Top             =   2760
      Width           =   855
   End
   Begin MSComctlLib.Slider Rows 
      Height          =   2775
      Left            =   5520
      TabIndex        =   5
      Top             =   360
      Width           =   435
      _ExtentX        =   767
      _ExtentY        =   4895
      _Version        =   393216
      Orientation     =   1
      Min             =   1
      Max             =   50
      SelStart        =   5
      Value           =   5
   End
   Begin VB.CommandButton b_Send 
      Caption         =   "SendMatrix"
      Height          =   375
      Left            =   5520
      TabIndex        =   4
      Top             =   3240
      Width           =   1095
   End
   Begin DMatlabDemonstration.MatrixView sMView 
      Height          =   2535
      Left            =   120
      TabIndex        =   3
      Top             =   120
      Width           =   5295
      _ExtentX        =   9340
      _ExtentY        =   4471
   End
   Begin DMatlabDemonstration.MatrixView rMview 
      Height          =   2535
      Left            =   120
      TabIndex        =   2
      Top             =   3720
      Width           =   6495
      _ExtentX        =   11456
      _ExtentY        =   4471
   End
   Begin VB.CommandButton b_Disconnect 
      Caption         =   "Disconnect"
      Height          =   375
      Left            =   4440
      TabIndex        =   1
      Top             =   3240
      Width           =   975
   End
   Begin VB.CommandButton b_Connect 
      Caption         =   "Connect"
      Height          =   375
      Left            =   3480
      TabIndex        =   0
      Top             =   3240
      Width           =   855
   End
   Begin DMatlabControl.DMatlab DMatlab1 
      Height          =   615
      Left            =   5880
      Top             =   6480
      Width           =   495
      _ExtentX        =   873
      _ExtentY        =   1085
   End
   Begin MSComctlLib.Slider Columns 
      Height          =   2775
      Left            =   6120
      TabIndex        =   6
      Top             =   360
      Width           =   435
      _ExtentX        =   767
      _ExtentY        =   4895
      _Version        =   393216
      Orientation     =   1
      Min             =   1
      Max             =   50
      SelStart        =   5
      Value           =   5
   End
   Begin VB.Label Label2 
      Caption         =   "Columns"
      Height          =   255
      Left            =   6000
      TabIndex        =   8
      Top             =   120
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Rows"
      Height          =   255
      Left            =   5520
      TabIndex        =   7
      Top             =   120
      Width           =   495
   End
End
Attribute VB_Name = "mainform"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub b_Connect_Click()
    DMatlab1.ipaddress = ipaddress.Text
    DMatlab1.port = port.Text
    DMatlab1.Unplug
    DMatlab1.Plug
End Sub

Private Sub b_Disconnect_Click()
    DMatlab1.Unplug
End Sub

Private Sub b_Refresh_Click()
    sMView.RefreshView
End Sub

Private Sub b_Send_Click()
    Dim mdata As MatrixData
    ReDim mdata.data(2) As Byte
    mdata.Rows = sMView.matrix.Rows
    mdata.cols = sMView.matrix.Columns
    sMView.matrix.GetBytes mdata.data, 0
    DMatlab1.SendMatrix mdata
End Sub

Private Sub Columns_Change()
    Rows_Change
End Sub


Private Sub Command1_Click()
    Randomize
    Dim rw As Long, cl As Long
    For rw = 1 To sMView.matrix.Rows
        For cl = 1 To sMView.matrix.Columns
            sMView.matrix.SetValue rw, cl, Rnd() * 10000
        Next cl
    Next rw
    sMView.RefreshView
End Sub

Private Sub DMatlab1_Error(ByVal Number As Integer, Description As String)
    MsgBox Description
End Sub

Private Sub DMatlab1_Plugged()
    MsgBox "Connected"
End Sub

Private Sub DMatlab1_RecieveMatrix(matrix As DMatlabControl.MatrixData)
    Dim m As New matrix
    m.Initialize matrix.Rows, matrix.cols
    m.SetBytes matrix.data, 0
    
    rMview.matrix = m
End Sub

Private Sub DMatlab1_Unplugged()
    MsgBox "Disconnected"
End Sub

Private Sub Form_Load()
    Dim m As New matrix
    m.Initialize Rows.value, Columns.value
    
    sMView.matrix = m
    
End Sub

Private Sub Rows_Change()
    Dim m As New matrix
    m.Initialize Rows.value, Columns.value
    
    sMView.matrix = m
End Sub


