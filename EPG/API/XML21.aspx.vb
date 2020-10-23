Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml

Public Class XML21
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

       

        Dim strAPIkey As String = Request.QueryString("apikey")
        Dim strChannelid As String = Request.QueryString("channel")
        Dim strLanguageid As String = Request.QueryString("language")
        Dim strdate As String = Request.QueryString("date")
        Dim strDays As String = Request.QueryString("days")
        Dim strtype As String = Request.QueryString("type")

        If strAPIkey = "E5FA907176D4DCC637C884790EB92831" Then
            Dim obj As New clsExecute
            Dim dt As New DataTable(tableName:="MyTableName")

            If strtype = "1" Then
                dt = obj.executeSQL("select * from fn_viewepg ('" & strChannelid & "','" & strLanguageid & "','" & Convert.ToDateTime(strdate).ToString("yyyy-MM-dd") & "') order by sortby", False)
            Else
                dt = obj.executeSQL("sp_export_exportepg_toexcel", "ChannelId~fromDate~ToDate", "VarChar~DateTime~DateTime", strChannelid & "~" & Convert.ToDateTime(strdate).Date & "~" & Convert.ToDateTime(strdate).AddDays(strDays).Date, True, False)
            End If
            'gen_XMLTV_XML(dt, strChannelid, strChannelid)
            gen_DVB_XML(dt, strChannelid, strChannelid)
        Else
            Response.Write("Invalid Key")
        End If
    End Sub

    Private Sub gen_XMLTV_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String)
        Dim doc As XmlDocument = New XmlDocument()
        Dim docNode As XmlNode = doc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
        doc.AppendChild(docNode)

        Dim tvNode As XmlNode = doc.CreateElement("tv")
        doc.AppendChild(tvNode)

        Dim channelNode As XmlNode = doc.CreateElement("channel")
        Dim channelAttribute As XmlAttribute = doc.CreateAttribute("id")
        channelAttribute.Value = vChannel
        channelNode.Attributes.Append(channelAttribute)
        tvNode.AppendChild(channelNode)

        Dim nameNode As XmlNode = doc.CreateElement("display-name")
        nameNode.AppendChild(doc.CreateTextNode(vServiceID))
        channelNode.AppendChild(nameNode)

        Dim programmeNode As XmlNode
        Dim titleNode As XmlNode
        Dim descNode As XmlNode
        Dim startAttribute As XmlAttribute
        Dim stopAttribute As XmlAttribute
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                programmeNode = doc.CreateElement("programme")
                titleNode = doc.CreateElement("title")
                descNode = doc.CreateElement("desc")
                startAttribute = doc.CreateAttribute("start")
                startAttribute.Value = Convert.ToDateTime(dt.Rows(i)("progtime").ToString).ToString("yyyyMMddHHmmss") & " +0530"
                stopAttribute = doc.CreateAttribute("stop")
                stopAttribute.Value = Convert.ToDateTime(dt.Rows(i)("progtime").ToString).AddMinutes(dt.Rows(i)("duration").ToString).ToString("yyyyMMddHHmmss") & " +0530"
                channelAttribute = doc.CreateAttribute("channel")
                channelAttribute.Value = vChannel

                programmeNode.Attributes.Append(startAttribute)
                programmeNode.Attributes.Append(stopAttribute)
                programmeNode.Attributes.Append(channelAttribute)

                titleNode.AppendChild(doc.CreateTextNode(dt.Rows(i)("progname").ToString))
                descNode.AppendChild(doc.CreateTextNode(dt.Rows(i)("synopsis").ToString))

                programmeNode.AppendChild(titleNode)
                programmeNode.AppendChild(descNode)

                tvNode.AppendChild(programmeNode)
            Next
        End If

        '----------XMLTV format------------ full fields
        '<title lang="en">Mystery!</title>
        '<sub-title lang="en">Foyle's War, Series IV: Bleak Midwinter</sub-title>
        '<desc lang="en">Foyle investigates an explosion at a munitions factory, which he comes to believe may have been premeditated.</desc>
        '<date>20070701</date>
        '<category lang="en">Anthology</category>  ----------GENRE
        '<category lang="en">Mystery</category>
        '<category lang="en">Series</category>
        '<episode-num system="dd_progid">EP00003026.0665</episode-num>
        '<episode-num system="onscreen">2705</episode-num>
        '<audio>
        '  <stereo>stereo</stereo>
        '</audio>
        '<previously-shown start="20070701000000" />
        '<subtitles type="teletext" />

        Dim str As String
        Using stringWriter As New IO.StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                doc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Response.Clear()
        Response.Write(str)
    End Sub

    Private Sub gen_DVB_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String)
        Dim doc As XmlDocument = New XmlDocument()
        Dim docNode As XmlNode = doc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
        doc.AppendChild(docNode)

        Dim dvbNode As XmlNode = doc.CreateElement("DVB-EPG")
        doc.AppendChild(dvbNode)
        Dim xmlnsAttribute As XmlAttribute = doc.CreateAttribute("xmlns:xsi")
        xmlnsAttribute.Value = "http://www.w3.org/2001/XMLSchema-instance"
        Dim versionAttribute As XmlAttribute = doc.CreateAttribute("version")
        versionAttribute.Value = "1"
        dvbNode.Attributes.Append(xmlnsAttribute)
        dvbNode.Attributes.Append(versionAttribute)


        Dim serviceNode As XmlNode = doc.CreateElement("Service")
        Dim channelAttribute As XmlAttribute = doc.CreateAttribute("id")
        channelAttribute.Value = vChannel
        serviceNode.Attributes.Append(channelAttribute)
        dvbNode.AppendChild(serviceNode)


        Dim eventNode As XmlNode
        Dim shortEventDescriptorNode As XmlNode
        Dim extendedEventDescriptorNode As XmlNode
        Dim contentDescriptorNode As XmlNode

        Dim eventnameNode As XmlNode
        Dim textNode As XmlNode
        Dim textENode As XmlNode

        Dim idAttribute As XmlAttribute
        Dim nameAttribute As XmlAttribute
        Dim startAttribute As XmlAttribute
        Dim stopAttribute As XmlAttribute
        Dim epgstartAttribute As XmlAttribute
        Dim epgstopAttribute As XmlAttribute
        Dim scrambledAttribute As XmlAttribute

        Dim langAttribute As XmlAttribute
        Dim langEAttribute As XmlAttribute

        Dim nl1Attribute As XmlAttribute
        Dim nl2Attribute As XmlAttribute
        Dim un1Attribute As XmlAttribute
        Dim un2Attribute As XmlAttribute

        Dim serviceOffAirAttribute As XmlAttribute
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                eventNode = doc.CreateElement("Event")
                eventnameNode = doc.CreateElement("EventName")
                textNode = doc.CreateElement("Text")
                textENode = doc.CreateElement("Text")
                shortEventDescriptorNode = doc.CreateElement("ShortEventDescriptor")
                extendedEventDescriptorNode = doc.CreateElement("ExtendedEventDescriptor")
                contentDescriptorNode = doc.CreateElement("ContentDescriptor")

                idAttribute = doc.CreateAttribute("id")
                nameAttribute = doc.CreateAttribute("name")
                startAttribute = doc.CreateAttribute("start")
                stopAttribute = doc.CreateAttribute("stop")
                epgstartAttribute = doc.CreateAttribute("epgStart")
                epgstopAttribute = doc.CreateAttribute("epgStop")
                serviceOffAirAttribute = doc.CreateAttribute("serviceOffAir")
                scrambledAttribute = doc.CreateAttribute("scrambled")
                langAttribute = doc.CreateAttribute("languageCode")
                langEAttribute = doc.CreateAttribute("languageCode")
                nl1Attribute = doc.CreateAttribute("nibbleLevel1")
                nl2Attribute = doc.CreateAttribute("nibbleLevel2")
                un1Attribute = doc.CreateAttribute("userNibble1")
                un2Attribute = doc.CreateAttribute("userNibble2")



                idAttribute.Value = i + 1
                nameAttribute.Value = dt.Rows(i)("progname").ToString
                startAttribute.Value = Convert.ToDateTime(dt.Rows(i)("progtime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                stopAttribute.Value = Convert.ToDateTime(dt.Rows(i)("progtime").ToString).AddMinutes(dt.Rows(i)("duration").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                epgstartAttribute.Value = Convert.ToDateTime(dt.Rows(i)("progtime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                epgstopAttribute.Value = Convert.ToDateTime(dt.Rows(i)("progtime").ToString).AddMinutes(dt.Rows(i)("duration").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                serviceOffAirAttribute.Value = "false"
                scrambledAttribute.Value = "true"
                langAttribute.Value = "eng"
                langEAttribute.Value = "eng"
                nl1Attribute.Value = "0"
                nl2Attribute.Value = "0"
                un1Attribute.Value = "0"
                un2Attribute.Value = "0"

                eventNode.Attributes.Append(idAttribute)
                eventNode.Attributes.Append(nameAttribute)
                eventNode.Attributes.Append(startAttribute)
                eventNode.Attributes.Append(stopAttribute)
                eventNode.Attributes.Append(epgstartAttribute)
                eventNode.Attributes.Append(epgstopAttribute)
                eventNode.Attributes.Append(serviceOffAirAttribute)
                eventNode.Attributes.Append(scrambledAttribute)

                contentDescriptorNode.Attributes.Append(nl1Attribute)
                contentDescriptorNode.Attributes.Append(nl2Attribute)
                contentDescriptorNode.Attributes.Append(un1Attribute)
                contentDescriptorNode.Attributes.Append(un2Attribute)

                shortEventDescriptorNode.Attributes.Append(langAttribute)
                
                
                eventnameNode.AppendChild(doc.CreateTextNode(dt.Rows(i)("progname").ToString))
                textNode.AppendChild(doc.CreateTextNode(dt.Rows(i)("synopsis").ToString))
                textENode.AppendChild(doc.CreateTextNode(dt.Rows(i)("synopsis").ToString))

                shortEventDescriptorNode.AppendChild(eventnameNode)
                shortEventDescriptorNode.AppendChild(textNode)
                extendedEventDescriptorNode.Attributes.Append(langEAttribute)
                extendedEventDescriptorNode.AppendChild(textENode)

                

                eventNode.AppendChild(shortEventDescriptorNode)
                eventNode.AppendChild(extendedEventDescriptorNode)
                eventNode.AppendChild(contentDescriptorNode)

                serviceNode.AppendChild(eventNode)
            Next
        End If

        Dim str As String
        Using stringWriter As New IO.StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                doc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Response.Clear()
        Response.Write(str)
    End Sub

    Private Sub gen_AirtelType_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String)

    End Sub



End Class