VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "Matrix"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (Destination As Any, Source As Any, ByVal length As Long)


Private m_matrix() As Double
Private m_rows As Long
Private m_columns As Long

Public Property Get Rows() As Long
    Rows = m_rows
End Property
Public Property Let Rows(value As Long)
    m_rows = value
End Property
Public Property Get Columns() As Long
    Columns = m_columns
End Property
Public Property Let Columns(value As Long)
    m_columns = value
End Property
Public Property Get Size() As Long
    Size = m_rows * m_columns
End Property
Public Property Get SquareMatrix() As Boolean
    If m_rows = m_columns Then
        SquareMatrix = True
    Else
        SquareMatrix = False
    End If
End Property

Public Sub Initialize(rws As Long, cols As Long)
    Rows = rws
    Columns = cols
    ReDim m_matrix(1 To cols, 1 To rws)
End Sub
Public Sub SetValue(rws As Long, cols As Long, value As Double)
    m_matrix(cols, rws) = value
End Sub
Public Function GetValue(ByVal rws As Long, ByVal cols As Long) As Double
    GetValue = m_matrix(cols, rws)
End Function
Public Sub SetBytes(data() As Byte, startindex As Long)
    CopyMemory m_matrix(1, 1), data(startindex), m_rows * m_columns * 8
End Sub
Public Sub GetBytes(data() As Byte, startindex As Long)
    If (startindex + m_rows * m_columns * 8) - 1 > UBound(data) Then
        ReDim Preserve data((startindex + m_rows * m_columns * 8) - 1)
    End If
    
    CopyMemory data(startindex), m_matrix(1, 1), m_rows * m_columns * 8
End Sub

