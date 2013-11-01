VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Begin VB.UserControl MatrixView 
   ClientHeight    =   2940
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   3900
   ScaleHeight     =   2940
   ScaleWidth      =   3900
   Begin VB.TextBox cellvalue 
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   2880
      Visible         =   0   'False
      Width           =   1695
   End
   Begin MSFlexGridLib.MSFlexGrid mgrd 
      Height          =   2775
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   3855
      _ExtentX        =   6800
      _ExtentY        =   4895
      _Version        =   393216
      FixedRows       =   0
      FixedCols       =   0
      WordWrap        =   -1  'True
      AllowUserResizing=   1
      Appearance      =   0
   End
End
Attribute VB_Name = "MatrixView"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Private m_matrix As matrix

Public Property Let matrix(value As matrix)
    Set m_matrix = value
    RefreshView
End Property
Public Property Get matrix() As matrix
    Set matrix = m_matrix
End Property

Public Sub RefreshView()
    mgrd.Clear
    mgrd.Rows = m_matrix.Rows
    mgrd.cols = m_matrix.Columns
    
    SetWidths
    
    For rw = 1 To m_matrix.Rows
        For cl = 1 To m_matrix.Columns
            mgrd.TextMatrix(rw - 1, cl - 1) = m_matrix.GetValue(ByVal rw, ByVal cl)
        Next cl
    Next rw
    
    mgrd.Row = 0
    mgrd.Col = 0
    
    cellvalue.Text = mgrd.Text
    
End Sub

Private Sub b_RefreshView_Click()
    RefreshView
End Sub

Private Sub cellvalue_Change()
    'On Error Resume Next
    'm_matrix.SetValue mgrd.Row + 1, mgrd.Col + 1, CDbl(cellvalue.Text)
    'mgrd.Text = cellvalue.Text
End Sub

Private Sub mgrd_EnterCell()
   MoveTextBox
   cellvalue.Visible = True
   cellvalue.Text = mgrd.Text
   cellvalue.SetFocus
End Sub
Private Sub MoveTextBox()
    cellvalue.Left = mgrd.CellLeft + mgrd.Left
    cellvalue.Top = mgrd.CellTop
    cellvalue.Height = mgrd.CellHeight
    cellvalue.Width = mgrd.CellWidth
End Sub


Private Sub mgrd_LeaveCell()
    On Error Resume Next
    m_matrix.SetValue mgrd.Row + 1, mgrd.Col + 1, CDbl(cellvalue.Text)
    mgrd.Text = cellvalue.Text
End Sub

Private Sub mgrd_SelChange()
    'cellvalue.Text = mgrd.Text
End Sub

Private Sub UserControl_Initialize()
    'mgrd.CellWidth = 10
    'mgrd.CellHeight = 10
End Sub
Private Sub SetWidths()
    For ic = 0 To mgrd.cols - 1
        mgrd.ColWidth(ic) = 600
    Next ic
End Sub
Private Sub UserControl_Resize()
    mgrd.Width = Width
    mgrd.Height = Height
    
    'b_RefreshView.Top = Height - (b_RefreshView.Height + 65)
    'cellvalue.Top = b_RefreshView.Top
    'b_RefreshView.Left = Width - (b_RefreshView.Width + 50)
End Sub
