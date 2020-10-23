Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf


Public Class LabelFormat
    ''' <summary>
    ''' Numerical Id of the label format
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Id As Integer
    ''' <summary>
    ''' Name of the label format (e.g. Avery L7163)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name As String
    ''' <summary>
    ''' Description of label format (e.g. A4 Sheet of 99.1 x 38.1mm address labels)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String
    ''' <summary>
    ''' Width of page in millimeters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PageWidth As Double
    ''' <summary>
    ''' Height of page in millimeters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PageHeight As Double
    ''' <summary>
    ''' Margin between top of page and top of first label in millimeters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TopMargin As Double
    ''' <summary>
    ''' Margin between left of page and left of first label in millimeters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LeftMargin As Double
    ''' <summary>
    ''' Width of individual label in millimeters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LabelWidth As Double
    ''' <summary>
    ''' Height of individual label in millimeters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LabelHeight As Double
    ''' <summary>
    ''' Padding on the left of an individual label, creates space between label edge and start of content
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LabelPaddingLeft As Double
    ''' <summary>
    ''' Padding on the Right of an individual label, creates space between label edge and end of content
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LabelPaddingRight As Double
    ''' <summary>
    ''' Padding on the top of an individual label, creates space between label edge and start of content
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LabelPaddingTop As Double
    ''' <summary>
    ''' Padding on the Bottom of an individual label, creates space between label edge and end of content
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LabelPaddingBottom As Double
    ''' <summary>
    ''' Distance between top of one label and top of label below it in millimeters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VerticalPitch As Double
    ''' <summary>
    ''' Distance between left of one label and left of label to the right of it in millimeters
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HorizontalPitch As Double
    ''' <summary>
    ''' Number of labels going across the page
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ColumnCount As Integer
    ''' <summary>
    ''' Number of labels going down the page
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RowCount As Integer

    ''' <summary>
    ''' Instantiate a new label sheet format definition
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Instantiate a new label sheet format definition
    ''' </summary>
    ''' <param name="Id">Numerical Id of the label format</param>
    ''' <param name="Name">Name of the label format (e.g. Avery L7163)</param>
    ''' <param name="Description">Description of label format (e.g. A4 Sheet of 99.1 x 38.1mm address labels)</param>
    ''' <param name="PageWidth">Width of page in millimeters</param>
    ''' <param name="PageHeight">Height of page in millimeters</param>
    ''' <param name="TopMargin">Margin between top of page and top of first label in millimeters</param>
    ''' <param name="LeftMargin">Margin between left of page and left of first label in millimeters</param>
    ''' <param name="LabelWidth">Width of individual label in millimeters</param>
    ''' <param name="LabelHeight">Height of individual label in millimeters</param>
    ''' <param name="VerticalPitch">Distance between top of one label and top of label below it in millimeters</param>
    ''' <param name="HorizontalPitch">Distance between left of one label and left of label to the right of it in millimeters</param>
    ''' <param name="ColumnCount">Number of labels going across the page</param>
    ''' <param name="RowCount">Number of labels going down the page</param>
    ''' <param name="LabelPaddingLeft">Padding on the left of an individual label, creates space between label edge and start of content</param>
    ''' <param name="LabelPaddingRight">Padding on the Right of an individual label, creates space between label edge and end of content</param>
    ''' <param name="LabelPaddingTop">Padding on the top of an individual label, creates space between label edge and start of content</param>
    ''' <param name="LabelPaddingBottom">Padding on the Bottom of an individual label, creates space between label edge and end of content</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Id As Integer,
                    ByVal Name As String,
                    ByVal Description As String,
                    ByVal PageWidth As Double,
                    ByVal PageHeight As Double,
                    ByVal TopMargin As Double,
                    ByVal LeftMargin As Double,
                    ByVal LabelWidth As Double,
                    ByVal LabelHeight As Double,
                    ByVal VerticalPitch As Double,
                    ByVal HorizontalPitch As Double,
                    ByVal ColumnCount As Integer,
                    ByVal RowCount As Integer,
                    Optional ByVal LabelPaddingLeft As Double = 0.0,
                    Optional ByVal LabelPaddingRight As Double = 0.0,
                    Optional ByVal LabelPaddingTop As Double = 0.0,
                    Optional ByVal LabelPaddingBottom As Double = 0.0)
        Me.Id = Id
        Me.Name = Name
        Me.Description = Description
        Me.PageWidth = PageWidth
        Me.PageHeight = PageHeight
        Me.TopMargin = TopMargin
        Me.LeftMargin = LeftMargin
        Me.LabelWidth = LabelWidth
        Me.LabelHeight = LabelHeight
        Me.VerticalPitch = VerticalPitch
        Me.HorizontalPitch = HorizontalPitch
        Me.ColumnCount = ColumnCount
        Me.RowCount = RowCount
        Me.LabelPaddingLeft = LabelPaddingLeft
        Me.LabelPaddingRight = LabelPaddingRight
        Me.LabelPaddingTop = LabelPaddingTop
        Me.LabelPaddingBottom = LabelPaddingBottom
    End Sub

End Class

Public Class clsLabelFormatBLL
    Private Shared _mLabelFormats As List(Of LabelFormat)

    ''' <summary>
    ''' Return a list of all label formats in the database.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLabelFormats() As List(Of LabelFormat)
        If IsNothing(_mLabelFormats) Then
            ' We are not using a database so manually create the labels here.
            _mLabelFormats = New List(Of LabelFormat)
            _mLabelFormats.Add(New LabelFormat(Id:=1,
                                               Name:="L7163",
                                               Description:="A4 Sheet of 99.1 x 38.1mm address labels",
                                               PageWidth:=210,
                                               PageHeight:=297,
                                               TopMargin:=10.1,
                                               LeftMargin:=4.7,
                                               LabelWidth:=99.1,
                                               LabelHeight:=34.0,
                                               VerticalPitch:=34.0,
                                               HorizontalPitch:=101.6,
                                               ColumnCount:=2,
                                               RowCount:=6,
                                               LabelPaddingTop:=5.0,
                                               LabelPaddingLeft:=8.0))
            _mLabelFormats.Add(New LabelFormat(Id:=3,
                                                           Name:="L7164",
                                                           Description:="A4 Sheet of 99.1 x 38.1mm address labels",
                                                           PageWidth:=210,
                                                           PageHeight:=297,
                                                           TopMargin:=15.1,
                                                           LeftMargin:=4.7,
                                                           LabelWidth:=99.1,
                                                           LabelHeight:=50,
                                                           VerticalPitch:=50,
                                                           HorizontalPitch:=101.6,
                                                           ColumnCount:=2,
                                                           RowCount:=7,
                                                           LabelPaddingTop:=5.0,
                                                           LabelPaddingLeft:=8.0))

            _mLabelFormats.Add(New LabelFormat(Id:=2,
                                               Name:="L7169",
                                               Description:="A4 Sheet of 99.1 x 139mm BlockOut (tm) address labels",
                                               PageWidth:=210,
                                               PageHeight:=297,
                                               TopMargin:=9.5,
                                               LeftMargin:=4.6,
                                               LabelWidth:=99.1,
                                               LabelHeight:=139,
                                               VerticalPitch:=139,
                                               HorizontalPitch:=101.6,
                                               ColumnCount:=2,
                                               RowCount:=2,
                                               LabelPaddingTop:=5.0,
                                               LabelPaddingLeft:=8.0))

        End If
        Return _mLabelFormats
    End Function


    Public Shared Function GetLabelFormat(ByVal Id As Integer) As LabelFormat
        GetLabelFormat = Nothing
        For Each lf As LabelFormat In GetLabelFormats()
            If lf.Id = Id Then
                ' Label format found. Return it.
                Return lf
            End If
        Next
    End Function
End Class

Public Class AddressesBLL
    ''' <summary>
    ''' Return a list of demo addresses for debugging and demonstration.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAddresses() As list(Of String)
        GetAddresses = New List(Of String)
        Dim sb As StringBuilder
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select name,company,addressline1,addressline2,city,statename,district,pincode,contact from mst_operatorAddress where active=1 order by isnull(operatorid,0),id", False)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                sb = New StringBuilder
                'If i = 0 Then
                '    sb.Append("From")
                '    sb.Append(vbCrLf)
                'Else
                '    sb.Append("To")
                '    sb.Append(vbCrLf)
                'End If
                sb.Append(dt.Rows(i)("name").ToString)
                sb.Append(vbCrLf)
                sb.Append(dt.Rows(i)("company").ToString)
                sb.Append(vbCrLf)
                sb.Append(dt.Rows(i)("addressline1").ToString & " " & dt.Rows(i)("addressline2").ToString)
                'sb.Append(vbCrLf)
                'sb.Append(dt.Rows(i)("addressline2").ToString)
                sb.Append(vbCrLf)
                sb.Append(dt.Rows(i)("city").ToString & " (" & dt.Rows(i)("statename").ToString & ")-" & dt.Rows(i)("pincode").ToString)
                sb.Append(vbCrLf)
                'sb.Append("District: " & dt.Rows(i)("district").ToString & ". " &
                'sb.Append(vbCrLf)
                sb.Append("Contact: " & dt.Rows(i)("contact").ToString)
                GetAddresses.Add(sb.ToString)
            Next
        End If
    End Function
End Class


Public Class PdfLabelUtil
    Public Shared Function GeneratePdfLabels(ByVal Addresses As List(Of String),
                                      ByVal lf As LabelFormat,
                                      Optional ByVal QtyEachLabel As Integer = 1) As MemoryStream
        GeneratePdfLabels = New MemoryStream

        ' The label sheet is basically a table and each cell is a single label

        ' Format related
        Dim CellsPerPage As Integer = lf.RowCount * lf.ColumnCount
        Dim CellsThisPage As Integer = 0
        Dim ContentRectangle As XRect       ' A single cell content rectangle. This is the rectangle that can be used for contents and accounts for margins and padding.
        Dim ContentSize As XSize            ' Size of content area inside a cell.
        Dim ContentLeftPos As Double        ' left edge of current content area.
        Dim ContentTopPos As Double         ' Top edge of current content area

        ' Layout related
        Dim StrokeColor As XColor = XColors.Black
        Dim FillColor As XColor = XColors.Black
        Dim Pen As XPen = New XPen(StrokeColor, 0.1)
        Dim Brush As XBrush = New XSolidBrush(FillColor)
        Dim Gfx As XGraphics
        Dim Path As XGraphicsPath

        Dim LoopTemp As Integer = 0         ' Counts each itteration. Used with QtyEachLabel
        Dim CurrentColumn As Integer = 1
        Dim CurrentRow As Integer = 1
        Dim Doc As New PdfDocument
        Dim page As PdfPage = Nothing
        AddPage(Doc, page, lf)
        Gfx = XGraphics.FromPdfPage(page)

        ' Ensure that at least 1 of each label is printed.
        If QtyEachLabel < 1 Then QtyEachLabel = 1

        ' Define the content area size
        ContentSize = New XSize(XUnit.FromMillimeter(lf.LabelWidth - lf.LabelPaddingLeft - lf.LabelPaddingRight).Point,
                             XUnit.FromMillimeter(lf.LabelHeight - lf.LabelPaddingTop - lf.LabelPaddingBottom).Point)

        If Not IsNothing(Addresses) Then
            If Addresses.Count > 0 Then
                ' We actually have addresses to output.
                For Each Address As String In Addresses
                    ' Once for each address
                    For LoopTemp = 1 To QtyEachLabel
                        ' Once for each copy of this address.
                        If CellsThisPage = CellsPerPage Then
                            ' This pages worth of cells are filled up. Create a new page
                            AddPage(Doc, page, lf)
                            Gfx = XGraphics.FromPdfPage(page)
                            CellsThisPage = 0
                        End If

                        ' Calculate which row and column we are working on.
                        CurrentColumn = (CellsThisPage + 1) Mod lf.ColumnCount
                        CurrentRow = Fix((CellsThisPage + 1) / lf.ColumnCount)

                        If CurrentColumn = 0 Then
                            ' This occurs when you are working on the last column of the row. 
                            ' This affects the count for column and row
                            CurrentColumn = lf.ColumnCount
                        Else
                            ' We are not viewing the last column so this number will be decremented by one.
                            CurrentRow = CurrentRow + 1
                        End If

                        ' Calculate the left position of the current cell.
                        ContentLeftPos = ((CurrentColumn - 1) * lf.HorizontalPitch) + lf.LeftMargin + lf.LabelPaddingLeft

                        ' Calculate the top position of the current cell.
                        ContentTopPos = ((CurrentRow - 1) * lf.VerticalPitch) + lf.TopMargin + lf.LabelPaddingTop

                        ' Define the content rectangle.
                        ContentRectangle = New XRect(New XPoint(XUnit.FromMillimeter(ContentLeftPos).Point, XUnit.FromMillimeter(ContentTopPos).Point),
                                                     ContentSize)

                        Path = New XGraphicsPath

                        ' Add the address string to the page.
                        Path.AddString(Address,
                                        New XFontFamily("Times New Roman"),
                                        XFontStyle.Regular,
                                        12,
                                        ContentRectangle,
                                        XStringFormats.TopLeft)

                        Gfx.DrawPath(Pen, Brush, Path)

                        ' Increment the cell count
                        CellsThisPage = CellsThisPage + 1
                    Next LoopTemp
                Next
                ' Output the document
                Doc.Save(GeneratePdfLabels, False)
            End If
        End If
    End Function

    Private Shared Sub AddPage(ByRef Doc As PdfDocument,
                        ByRef Page As PdfPage,
                        ByVal lf As LabelFormat)
        Page = Doc.AddPage
        Page.Width = XUnit.FromMillimeter(lf.PageWidth)
        Page.Height = XUnit.FromMillimeter(lf.PageHeight)
    End Sub
End Class
