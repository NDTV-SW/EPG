'Copyright (c) 2014 Craig Moore - Deadline Automation Limited

'Permission is hereby granted, free of charge, to any person obtaining a copy
'of this software and associated documentation files (the "Software"), to deal
'in the Software without restriction, including without limitation the rights
'to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
'copies of the Software, and to permit persons to whom the Software is
'furnished to do so, subject to the following conditions:

'The above copyright notice and this permission notice shall be included in
'all copies or substantial portions of the Software.

'THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
'IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
'FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
'AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
'LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
'OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
'THE SOFTWARE.

Imports System.Web
Imports System.Web.Services
Imports System.IO


Public Class LabelHandler
    Implements System.Web.IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If IsNumeric(context.Request.QueryString("Id")) Then

            Using MemStream As MemoryStream = PdfLabelUtil.GeneratePdfLabels(AddressesBLL.GetAddresses,
                                                                             clsLabelFormatBLL.GetLabelFormat(CInt(context.Request.QueryString("Id"))),
                                                                             1)
                If Not IsNothing(MemStream) Then
                    With context.Response
                        .Clear()
                        .ContentType = "application/pdf"
                        .AddHeader("content-length", MemStream.Length.ToString())
                        .AppendHeader("Content-Disposition", "inline; filename=AddressLabels.pdf")
                        .BinaryWrite(MemStream.ToArray())
                        .Flush()
                        .Close()
                        .End()
                    End With
                End If
            End Using
        Else
            context.Response.ContentType = "text/plain"
            context.Response.Write("ID Missing")
        End If
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class