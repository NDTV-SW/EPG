Imports System.Xml
Imports System.IO
Imports System.Linq
'Imports System.IO.Compression


Public Class clsEPGService

    Public Sub clsEPGService()
        'GZipStream()
    End Sub

#Region "Rich"

    Public Function gen_rich_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, ByVal vOperatorChannel As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vTZ As String) As String
        Dim doc As XmlDocument = New XmlDocument()
        Dim documentElementNode As XmlNode = doc.CreateElement("DocumentElement")
        If vTZ = "" Then
            vTZ = "ist"
        End If

        If dt.Rows.Count > 0 Then
            Dim epgRootNode As XmlNode, channelnoNode As XmlNode, channeldescNode As XmlNode, serviceidNode As XmlNode, channelidNode As XmlNode, channelgenreNode As XmlNode
            Dim progstartdateNode As XmlNode, progenddateNode As XmlNode, progstarttimeNode As XmlNode, progendtimeNode As XmlNode, durationNode As XmlNode
            Dim prognameNode As XmlNode, synopsisNode As XmlNode, episodictitleNode As XmlNode, episodicsynopsisNode As XmlNode, extendedSynopsisNode As XmlNode, channellogoNode As XmlNode
            Dim programlogoNode As XmlNode, programlogo_portraitNode As XmlNode, languageNode As XmlNode, showtypeNode As XmlNode, genreNode As XmlNode
            Dim subgenreNode As XmlNode, episodenoNode As XmlNode, starcastNode As XmlNode, directorNode As XmlNode, releaseyearNode As XmlNode, writerNode As XmlNode
            Dim isliveNode As XmlNode, episodetypeNode As XmlNode, team1Node As XmlNode, team2Node As XmlNode, leagueNode As XmlNode, timezoneNode As XmlNode, fillerNode As XmlNode
            Dim channelRootNode As XmlNode, hdChannelNode As XmlNode, eventidNode As XmlNode, parentalRatingNode As XmlNode, awardsNode As XmlNode, seasonnoNode As XmlNode

            channelRootNode = doc.CreateElement("CHANNEL")
            channelnoNode = doc.CreateElement("channelno")
            channeldescNode = doc.CreateElement("channeldesc")
            serviceidNode = doc.CreateElement("serviceid")
            channelidNode = doc.CreateElement("channelid")
            channelgenreNode = doc.CreateElement("channelgenre")
            channellogoNode = doc.CreateElement("channellogo")
            hdChannelNode = doc.CreateElement("hdchannel")
            timezoneNode = doc.CreateElement("timezone")
            languageNode = doc.CreateElement("language")


            channelnoNode.InnerText = vChannelno
            channeldescNode.InnerText = dt.Rows(0)("channeldesc").ToString
            serviceidNode.InnerText = vServiceID
            channelidNode.InnerText = vOperatorChannel
            channelgenreNode.InnerText = dt.Rows(0)("channelgenre").ToString
            channellogoNode.InnerText = dt.Rows(0)("channellogo").ToString
            hdChannelNode.InnerText = dt.Rows(0)("hdchannel").ToString
            languageNode.InnerText = dt.Rows(0)("language").ToString
            timezoneNode.InnerText = vTZ

            channelRootNode.AppendChild(channelnoNode)
            channelRootNode.AppendChild(serviceidNode)
            channelRootNode.AppendChild(channelidNode)
            channelRootNode.AppendChild(channeldescNode)
            channelRootNode.AppendChild(channelgenreNode)
            channelRootNode.AppendChild(languageNode)
            channelRootNode.AppendChild(channellogoNode)
            channelRootNode.AppendChild(hdChannelNode)
            channelRootNode.AppendChild(timezoneNode)


            documentElementNode.AppendChild(channelRootNode)

            Dim j As Integer = 1

            For i As Integer = 0 To dt.Rows.Count - 1

                Dim dCurrDate As DateTime
                If vTZ = "utc" Then
                    dCurrDate = Convert.ToDateTime(dt.Rows(i)("utcstartdatetime").ToString).Date
                Else
                    dCurrDate = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).Date
                End If

                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                    epgRootNode = doc.CreateElement("EPG")
                    eventidNode = doc.CreateElement("eventid")
                    progstartdateNode = doc.CreateElement("progstartdate")
                    progenddateNode = doc.CreateElement("progenddate")
                    progstarttimeNode = doc.CreateElement("progstarttime")
                    progendtimeNode = doc.CreateElement("progendtime")
                    durationNode = doc.CreateElement("duration")
                    prognameNode = doc.CreateElement("progname")
                    synopsisNode = doc.CreateElement("synopsis")
                    extendedSynopsisNode = doc.CreateElement("extendedsynopsis")
                    episodictitleNode = doc.CreateElement("episodictitle")
                    episodicsynopsisNode = doc.CreateElement("episodicsynopsis")

                    programlogoNode = doc.CreateElement("programlogo")
                    programlogo_portraitNode = doc.CreateElement("programlogo_portrait")

                    showtypeNode = doc.CreateElement("showtype")
                    genreNode = doc.CreateElement("genre")
                    subgenreNode = doc.CreateElement("subgenre")
                    episodenoNode = doc.CreateElement("episodeno")
                    starcastNode = doc.CreateElement("starcast")
                    awardsNode = doc.CreateElement("awards")
                    directorNode = doc.CreateElement("director")
                    releaseyearNode = doc.CreateElement("releaseyear")
                    writerNode = doc.CreateElement("writer")
                    isliveNode = doc.CreateElement("islive")
                    episodetypeNode = doc.CreateElement("episodetype")
                    team1Node = doc.CreateElement("team1")
                    team2Node = doc.CreateElement("team2")
                    leagueNode = doc.CreateElement("league")
                    parentalRatingNode = doc.CreateElement("parentalrating")
                    seasonnoNode = doc.CreateElement("seasonno")


                    fillerNode = doc.CreateElement("filler")

                    eventidNode.InnerText = j
                    j = j + 1
                    If vTZ = "utc" Then
                        progstartdateNode.InnerText = Convert.ToDateTime(dt.Rows(i)("utcstartdatetime").ToString).ToString("dd MMM yyyy")
                        progenddateNode.InnerText = Convert.ToDateTime(dt.Rows(i)("utcenddatetime").ToString).ToString("dd MMM yyyy")
                        progstarttimeNode.InnerText = Convert.ToDateTime(dt.Rows(i)("utcstartdatetime").ToString).ToString("HH:mm:ss")
                        progendtimeNode.InnerText = Convert.ToDateTime(dt.Rows(i)("utcenddatetime").ToString).ToString("HH:mm:ss")
                    Else
                        progstartdateNode.InnerText = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).ToString("dd MMM yyyy")
                        progenddateNode.InnerText = Convert.ToDateTime(dt.Rows(i)("enddatetime").ToString).ToString("dd MMM yyyy")
                        progstarttimeNode.InnerText = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).ToString("HH:mm:ss")
                        progendtimeNode.InnerText = Convert.ToDateTime(dt.Rows(i)("enddatetime").ToString).ToString("HH:mm:ss")
                    End If

                    durationNode.InnerText = dt.Rows(i)("duration").ToString
                    prognameNode.InnerText = dt.Rows(i)("progname").ToString
                    synopsisNode.InnerText = dt.Rows(i)("synopsis").ToString
                    extendedSynopsisNode.InnerText = dt.Rows(i)("extendedsynopsis").ToString
                    episodictitleNode.InnerText = dt.Rows(i)("episodic_title").ToString
                    episodicsynopsisNode.InnerText = dt.Rows(i)("episodic_synopsis").ToString
                    programlogoNode.InnerText = dt.Rows(i)("programlogo").ToString
                    programlogo_portraitNode.InnerText = dt.Rows(i)("proglogo_portrait").ToString

                    showtypeNode.InnerText = dt.Rows(i)("showtype").ToString
                    genreNode.InnerText = dt.Rows(i)("genre").ToString
                    subgenreNode.InnerText = dt.Rows(i)("subgenre").ToString
                    episodenoNode.InnerText = dt.Rows(i)("episodeno").ToString
                    starcastNode.InnerText = dt.Rows(i)("starcast").ToString
                    awardsNode.InnerText = dt.Rows(i)("awards").ToString
                    directorNode.InnerText = dt.Rows(i)("director").ToString
                    releaseyearNode.InnerText = dt.Rows(i)("release_year").ToString
                    writerNode.InnerText = dt.Rows(i)("writer").ToString
                    isliveNode.InnerText = dt.Rows(i)("is_live").ToString
                    episodetypeNode.InnerText = dt.Rows(i)("episodetype").ToString
                    team1Node.InnerText = dt.Rows(i)("team1").ToString
                    team2Node.InnerText = dt.Rows(i)("team2").ToString
                    leagueNode.InnerText = dt.Rows(i)("league").ToString
                    parentalRatingNode.InnerText = dt.Rows(i)("parentalrating").ToString
                    seasonnoNode.InnerText = dt.Rows(i)("seasonno").ToString


                    fillerNode.InnerText = dt.Rows(i)("filler").ToString

                    epgRootNode.AppendChild(eventidNode)
                    epgRootNode.AppendChild(progstartdateNode)
                    epgRootNode.AppendChild(progenddateNode)
                    epgRootNode.AppendChild(progstarttimeNode)
                    epgRootNode.AppendChild(progendtimeNode)
                    epgRootNode.AppendChild(durationNode)
                    epgRootNode.AppendChild(seasonnoNode)
                    epgRootNode.AppendChild(prognameNode)
                    epgRootNode.AppendChild(synopsisNode)
                    epgRootNode.AppendChild(episodictitleNode)
                    epgRootNode.AppendChild(episodicsynopsisNode)
                    epgRootNode.AppendChild(extendedSynopsisNode)
                    epgRootNode.AppendChild(programlogoNode)
                    epgRootNode.AppendChild(programlogo_portraitNode)
                    epgRootNode.AppendChild(showtypeNode)
                    epgRootNode.AppendChild(genreNode)
                    epgRootNode.AppendChild(subgenreNode)
                    epgRootNode.AppendChild(parentalRatingNode)

                    epgRootNode.AppendChild(episodenoNode)
                    epgRootNode.AppendChild(starcastNode)
                    epgRootNode.AppendChild(awardsNode)
                    epgRootNode.AppendChild(directorNode)
                    epgRootNode.AppendChild(releaseyearNode)
                    epgRootNode.AppendChild(writerNode)
                    epgRootNode.AppendChild(isliveNode)
                    epgRootNode.AppendChild(episodetypeNode)
                    epgRootNode.AppendChild(team1Node)
                    epgRootNode.AppendChild(team2Node)
                    epgRootNode.AppendChild(leagueNode)

                    epgRootNode.AppendChild(fillerNode)
                    documentElementNode.AppendChild(epgRootNode)
                End If
            Next
            doc.AppendChild(documentElementNode)
        End If

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                doc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function

    Public Function gen_richACT_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, ByVal vOperatorChannel As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vTZ As String) As String


        If vTZ = "" Then
            vTZ = "ist"
        End If

        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = ""



        Dim BiNode As XElement = New XElement(
            xNamespace + "DocumentElement"
        )
        Dim siChannelNode As XElement = New XElement(xNamespace + "CHANNEL",
                                                        New XElement(xNamespace + "channelno", vChannelno),
                                                        New XElement(xNamespace + "channeldesc", dt.Rows(0)("channeldesc").ToString),
                                                        New XElement(xNamespace + "serviceid", vServiceID),
                                                        New XElement(xNamespace + "channelid", vServiceID),
                                                        New XElement(xNamespace + "channelgenre", dt.Rows(0)("channelgenre").ToString),
                                                        New XElement(xNamespace + "channellogo",
                                                                     New XAttribute("resolution", "90x90"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/90/90/" & Logger.convertStringToHex(vChannel.Replace(" ", "_")) & "/channel"),
                                                        New XElement(xNamespace + "channellogo",
                                                                     New XAttribute("resolution", "120x120"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/120/120/" & Logger.convertStringToHex(vChannel.Replace(" ", "_")) & "/channel"),
                                                        New XElement(xNamespace + "channellogo",
                                                                     New XAttribute("resolution", "160x160"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/160/160/" & Logger.convertStringToHex(vChannel.Replace(" ", "_")) & "/channel"),
                                                        New XElement(xNamespace + "channellogo",
                                                                     New XAttribute("resolution", "250x250"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/250/250/" & Logger.convertStringToHex(vChannel.Replace(" ", "_")) & "/channel"),
                                                        New XElement(xNamespace + "channellogo",
                                                                     New XAttribute("resolution", "160x90"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/160/90/" & Logger.convertStringToHex(vChannel.Replace(" ", "_")) & "/channel"),
                                                                     New XElement(xNamespace + "language", dt.Rows(0)("language").ToString),
                                                                     New XElement(xNamespace + "hdchannel", dt.Rows(0)("hdchannel").ToString),
                                                                     New XElement(xNamespace + "timezone", vTZ)
            )
        BiNode.Add(siChannelNode)
        Dim j As Integer = 1
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                    'eventid()
                    'seasonno()
                    'extendedsynopsis()
                    'parentalrating()
                    'awards()
                    Dim siEventNode As XElement = New XElement(xNamespace + "EPG",
                                                        New XElement(xNamespace + "eventid", j),
                                                        New XElement(xNamespace + "progstartdate", IIf(vTZ = "ist", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("dd MMM yyyy"), Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("dd MMM yyyy"))),
                                                        New XElement(xNamespace + "progenddate", IIf(vTZ = "ist", Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("dd MMM yyyy"), Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("dd MMM yyyy"))),
                                                        New XElement(xNamespace + "progstarttime", IIf(vTZ = "ist", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("HH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("HH:mm:ss"))),
                                                        New XElement(xNamespace + "progendtime", IIf(vTZ = "ist", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("HH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("HH:mm:ss"))),
                                                        New XElement(xNamespace + "duration", dt.Rows(i)("duration").ToString),
                                                        New XElement(xNamespace + "seasonno", dt.Rows(i)("seasonno").ToString),
                                                        New XElement(xNamespace + "progname", dt.Rows(i)("progname").ToString),
                                                        New XElement(xNamespace + "synopsis", dt.Rows(i)("synopsis").ToString),
                                                        New XElement(xNamespace + "episodictitle", dt.Rows(i)("episodic_title").ToString),
                                                        New XElement(xNamespace + "episodicsynopsis", dt.Rows(i)("episodic_synopsis").ToString),
                                                        New XElement(xNamespace + "extendedsynopsis", dt.Rows(i)("extendedsynopsis").ToString),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "300x178"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/300/178/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "400x237"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/400/237/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "540x304"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/540/304/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "600x355"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/600/355/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "720x405"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/720/405/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "800x473"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/800/473/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "1080x608"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/1080/608/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "1440x810"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/1440/810/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "143x179"),
                                                                     New XAttribute("orientation", "portrait"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/143/179/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "190x238"),
                                                                     New XAttribute("orientation", "portrait"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/190/238/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "285x356"),
                                                                     New XAttribute("orientation", "portrait"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/285/376/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "380x475"),
                                                                     New XAttribute("orientation", "portrait"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/380/475/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "79x79"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/79/79/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "105x105"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/105/105/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "158x158"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/158/158/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "211x211"),
                                                                     New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/png/211/211/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "programlogo",
                                                                     New XAttribute("resolution", "340x192"),
                                                                     New XAttribute("orientation", "landscape"),
                                                                     New XAttribute("format", "jpeg"), "http://epgops.ndtv.com/serveimage/jpeg/340/192/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                        New XElement(xNamespace + "showtype", dt.Rows(i)("showtype").ToString),
                                                        New XElement(xNamespace + "genre", dt.Rows(i)("genre").ToString),
                                                        New XElement(xNamespace + "subgenre", dt.Rows(i)("subgenre").ToString),
                                                        New XElement(xNamespace + "parentalrating", dt.Rows(i)("parentalrating").ToString),
                                                        New XElement(xNamespace + "episodeno", dt.Rows(i)("episodeno").ToString),
                                                        New XElement(xNamespace + "starcast", dt.Rows(i)("starcast").ToString),
                                                        New XElement(xNamespace + "awards", dt.Rows(i)("awards").ToString),
                                                        New XElement(xNamespace + "director", dt.Rows(i)("director").ToString),
                                                        New XElement(xNamespace + "releaseyear", dt.Rows(i)("release_year").ToString),
                                                        New XElement(xNamespace + "writer", dt.Rows(i)("writer").ToString),
                                                        New XElement(xNamespace + "islive", dt.Rows(i)("is_live").ToString),
                                                        New XElement(xNamespace + "episodetype", dt.Rows(i)("episodetype").ToString),
                                                        New XElement(xNamespace + "team1", dt.Rows(i)("team1").ToString),
                                                        New XElement(xNamespace + "team2", dt.Rows(i)("team2").ToString),
                                                        New XElement(xNamespace + "league", dt.Rows(i)("league").ToString),
                                                        New XElement(xNamespace + "filler", dt.Rows(i)("filler").ToString))
                    '143X179	190X238	285X356	380X475
                    '79X79	105X105	158X158	211X211
                    'New XElement(xNamespace + "programlogo_portrait", dt.Rows(i)("proglogo_portrait").ToString),



                    BiNode.Add(siEventNode)
                    j = j + 1
                End If
            Next
        End If
        'BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function

    'Old Working with image url method
    'Public Function gen_richACT_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, ByVal vOperatorChannel As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vTZ As String) As String


    '    If vTZ = "" Then
    '        vTZ = "ist"
    '    End If

    '    Dim xDoc As XDocument = New XDocument()

    '    Dim xNamespace As XNamespace = ""



    '    Dim BiNode As XElement = New XElement(
    '        xNamespace + "DocumentElement"
    '    )
    '    Dim siChannelNode As XElement = New XElement(xNamespace + "CHANNEL",
    '                                                    New XElement(xNamespace + "channelno", vChannelno),
    '                                                    New XElement(xNamespace + "channeldesc", dt.Rows(0)("channeldesc").ToString),
    '                                                    New XElement(xNamespace + "serviceid", vServiceID),
    '                                                    New XElement(xNamespace + "channelid", vServiceID),
    '                                                    New XElement(xNamespace + "channelgenre", dt.Rows(0)("channelgenre").ToString),
    '                                                    New XElement(xNamespace + "channellogo",
    '                                                                 New XAttribute("resolution", "90x90"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?type=channel&format=png&width=90&height=90&id=" & Logger.convertStringToHex(vChannel.Replace(" ", "_") & ".png")),
    '                                                    New XElement(xNamespace + "channellogo",
    '                                                                 New XAttribute("resolution", "120x120"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?type=channel&format=png&width=120&height=120&id=" & Logger.convertStringToHex(vChannel.Replace(" ", "_") & ".png")),
    '                                                    New XElement(xNamespace + "channellogo",
    '                                                                 New XAttribute("resolution", "160x160"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?type=channel&format=png&width=160&height=160&id=" & Logger.convertStringToHex(vChannel.Replace(" ", "_") & ".png")),
    '                                                    New XElement(xNamespace + "channellogo",
    '                                                                 New XAttribute("resolution", "250x250"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?type=channel&format=png&width=250&height=250&id=" & Logger.convertStringToHex(vChannel.Replace(" ", "_") & ".png")),
    '                                                    New XElement(xNamespace + "channellogo",
    '                                                                 New XAttribute("resolution", "160x90"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?type=channel&format=png&width=160&height=90&id=" & Logger.convertStringToHex(vChannel.Replace(" ", "_") & ".png")),
    '                                                                 New XElement(xNamespace + "language", dt.Rows(0)("language").ToString),
    '                                                                 New XElement(xNamespace + "hdchannel", dt.Rows(0)("hdchannel").ToString),
    '                                                                 New XElement(xNamespace + "timezone", vTZ)
    '        )
    '    BiNode.Add(siChannelNode)
    '    Dim j As Integer = 1
    '    If dt.Rows.Count > 0 Then
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
    '            If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

    '                Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
    '                'eventid()
    '                'seasonno()
    '                'extendedsynopsis()
    '                'parentalrating()
    '                'awards()
    '                Dim siEventNode As XElement = New XElement(xNamespace + "EPG",
    '                                                    New XElement(xNamespace + "eventid", j),
    '                                                    New XElement(xNamespace + "progstartdate", IIf(vTZ = "ist", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("dd MMM yyyy"), Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("dd MMM yyyy"))),
    '                                                    New XElement(xNamespace + "progenddate", IIf(vTZ = "ist", Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("dd MMM yyyy"), Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("dd MMM yyyy"))),
    '                                                    New XElement(xNamespace + "progstarttime", IIf(vTZ = "ist", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("HH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("HH:mm:ss"))),
    '                                                    New XElement(xNamespace + "progendtime", IIf(vTZ = "ist", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("HH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("HH:mm:ss"))),
    '                                                    New XElement(xNamespace + "duration", dt.Rows(i)("duration").ToString),
    '                                                    New XElement(xNamespace + "seasonno", dt.Rows(i)("seasonno").ToString),
    '                                                    New XElement(xNamespace + "progname", dt.Rows(i)("progname").ToString),
    '                                                    New XElement(xNamespace + "synopsis", dt.Rows(i)("synopsis").ToString),
    '                                                    New XElement(xNamespace + "episodictitle", dt.Rows(i)("episodic_title").ToString),
    '                                                    New XElement(xNamespace + "episodicsynopsis", dt.Rows(i)("episodic_synopsis").ToString),
    '                                                    New XElement(xNamespace + "extendedsynopsis", dt.Rows(i)("extendedsynopsis").ToString),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "300x178"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=300&height=178&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "400x237"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=400&height=237&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "540x304"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=540&height=304&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "600x355"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=600&height=355&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "720x405"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=720&height=405&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "800x473"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=800&height=473&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "1080x608"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=1080&height=608&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "1440x810"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=1440&height=810&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "143x179"),
    '                                                                 New XAttribute("orientation", "portrait"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?orientation=portrait&format=png&width=143&height=179&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "190x238"),
    '                                                                 New XAttribute("orientation", "portrait"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?orientation=portrait&format=png&width=190&height=238&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "285x356"),
    '                                                                 New XAttribute("orientation", "portrait"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?orientation=portrait&format=png&width=285&height=376&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "380x475"),
    '                                                                 New XAttribute("orientation", "portrait"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?orientation=portrait&format=png&width=380&height=475&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "79x79"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?orientation=portrait&format=png&width=79&height=79&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "105x105"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=105&height=105&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "158x158"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=158&height=158&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "211x211"),
    '                                                                 New XAttribute("format", "png"), "http://epgops.ndtv.com/serveimage/?format=png&width=211&height=211&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo",
    '                                                                 New XAttribute("resolution", "340x192"),
    '                                                                 New XAttribute("orientation", "landscape"),
    '                                                                 New XAttribute("format", "jpeg"), "http://epgops.ndtv.com/serveimage/?format=jpeg&width=340&height=192&id=" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
    '                                                    New XElement(xNamespace + "programlogo_portrait", dt.Rows(i)("proglogo_portrait").ToString),
    '                                                    New XElement(xNamespace + "showtype", dt.Rows(i)("showtype").ToString),
    '                                                    New XElement(xNamespace + "genre", dt.Rows(i)("genre").ToString),
    '                                                    New XElement(xNamespace + "subgenre", dt.Rows(i)("subgenre").ToString),
    '                                                    New XElement(xNamespace + "parentalrating", dt.Rows(i)("parentalrating").ToString),
    '                                                    New XElement(xNamespace + "episodeno", dt.Rows(i)("episodeno").ToString),
    '                                                    New XElement(xNamespace + "starcast", dt.Rows(i)("starcast").ToString),
    '                                                    New XElement(xNamespace + "awards", dt.Rows(i)("awards").ToString),
    '                                                    New XElement(xNamespace + "director", dt.Rows(i)("director").ToString),
    '                                                    New XElement(xNamespace + "releaseyear", dt.Rows(i)("release_year").ToString),
    '                                                    New XElement(xNamespace + "writer", dt.Rows(i)("writer").ToString),
    '                                                    New XElement(xNamespace + "islive", dt.Rows(i)("is_live").ToString),
    '                                                    New XElement(xNamespace + "episodetype", dt.Rows(i)("episodetype").ToString),
    '                                                    New XElement(xNamespace + "team1", dt.Rows(i)("team1").ToString),
    '                                                    New XElement(xNamespace + "team2", dt.Rows(i)("team2").ToString),
    '                                                    New XElement(xNamespace + "league", dt.Rows(i)("league").ToString),
    '                                                    New XElement(xNamespace + "filler", dt.Rows(i)("filler").ToString))
    '                '143X179	190X238	285X356	380X475
    '                '79X79	105X105	158X158	211X211


    '                BiNode.Add(siEventNode)
    '                j = j + 1
    '            End If
    '        Next
    '    End If
    '    'BiNode.Add(siEventSNode)
    '    xDoc.Add(BiNode)

    '    Dim str As String
    '    Using stringWriter As New Utf8StringWriter()
    '        Using xmlTextWriter = XmlWriter.Create(stringWriter)
    '            xDoc.WriteTo(xmlTextWriter)
    '            xmlTextWriter.Flush()
    '            str = stringWriter.GetStringBuilder().ToString()
    '        End Using
    '    End Using
    '    Return str
    'End Function

    Public Function gen_BarrowaRich_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, ByVal vOperatorChannel As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vTZ As String) As String

        If vTZ = "" Then
            vTZ = "ist"
        End If

        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = "http://barrowa.com/common/epg/format/barrowa/v6"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"


        Dim BiNode As XElement = New XElement(
            xNamespace + "epg_import",
            New XAttribute("xmlns", xNamespace),
            New XAttribute("version", "6")
        )

        Dim siEventSNode As XElement = New XElement(xNamespace + "schedule",
                                                    New XAttribute("id", vServiceID)
                                                    )


        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))

                    Dim dStartDateTime As DateTime, dEndDateTime As DateTime

                    If vTZ = "utc" Then
                        dStartDateTime = Convert.ToDateTime(dt.Rows(i)("utcstartdatetime").ToString)
                        dEndDateTime = Convert.ToDateTime(dt.Rows(i)("utcenddatetime").ToString)
                    Else
                        dStartDateTime = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString)
                        dEndDateTime = Convert.ToDateTime(dt.Rows(i)("enddatetime").ToString)
                    End If

                    Dim siEventNode As XElement = New XElement(xNamespace + "event",
                                                           New XAttribute("start_time", dStartDateTime.ToString("yyyyMMdd HH:mm:ss")),
                                                           New XAttribute("end_time", dEndDateTime.ToString("yyyyMMdd HH:mm:ss")),
                                                           New XElement(xNamespace + "description",
                                                                        New XAttribute("language", "eng"),
                                                                        New XAttribute("title", dt.Rows(i)("progname").ToString),
                                                                        New XAttribute("short_synopsis", dt.Rows(i)("synopsis").ToString),
                                                                        New XAttribute("extended_synopsis", dt.Rows(i)("synopsis").ToString)
                                                                        ),
                                                           New XElement(xNamespace + "content",
                                                                        New XAttribute("nibble1", dt.Rows(i)("nibble1").ToString),
                                                                        New XAttribute("nibble2", dt.Rows(i)("nibble2").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "eventid"), New XAttribute("type", ""), New XAttribute("value", Convert.ToDecimal(dt.Rows(i)("progid")).ToString("500000000") & Convert.ToDecimal(IIf(dt.Rows(i)("episodeno") = "", "0", dt.Rows(i)("episodeno"))).ToString("0000"))),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "channellogo"), New XAttribute("type", "130"), New XAttribute("value", dt.Rows(i)("channellogo").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "channelno"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("channel_no").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "serviceid"), New XAttribute("type", ""), New XAttribute("value", vServiceID)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "channelgenre"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("channelgenre").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "channellanguage"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("language").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "channeldesc"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("channeldesc").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "hdchannel"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("hdchannel").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "timezone"), New XAttribute("type", ""), New XAttribute("value", vTZ)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "genre"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("genre").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "subgenre"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("subgenre").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "duration"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("duration").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "episodictitle"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("episodic_title").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "episodicsynopsis"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("synopsis").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "programlogo"), New XAttribute("type", "131"), New XAttribute("value", dt.Rows(i)("programlogo").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "programlogo_portrait"), New XAttribute("type", "132"), New XAttribute("value", dt.Rows(i)("proglogo_portrait").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "showtype"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("showtype").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "parentalrating"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("parentalrating").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "seasonno"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("seasonno").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "episodeno"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("episodeno").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "starcast"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("starcast").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "awards"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("awards").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "director"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("director").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "releaseyear"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("release_year").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "writer"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("writer").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "islive"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("is_live").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "episodetype"), New XAttribute("type", ""), New XAttribute("value", IIf(dt.Rows(i)("episodetype").ToString = "Repeat", "R", "O"))),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "team1"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("team1").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "team2"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("team2").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "league"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("league").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "filler"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("filler").ToString)),
                                                                        New XElement(xNamespace + "parameter", New XAttribute("name", "eventchanged"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("event_changed").ToString))
                                                                        )
                    siEventSNode.Add(siEventNode)
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)
        'New XElement(xNamespace + "parameter", New XAttribute("name", "countryorigin"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("countryorigin").ToString)),
        'New XElement(xNamespace + "parameter", New XAttribute("name", "eventtime"), New XAttribute("type", ""), New XAttribute("value", IIf(vTZ = "ist", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("HH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("HH:mm:ss")))),
        'New XElement(xNamespace + "parameter", New XAttribute("name", "channelname"), New XAttribute("type", ""), New XAttribute("value", vOperatorChannel)),
        'New XElement(xNamespace + "parameter", New XAttribute("name", "eventname"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("progname").ToString)),
        'New XElement(xNamespace + "parameter", New XAttribute("name", "shortdescription"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("synopsis").ToString)),
        'New XElement(xNamespace + "parameter", New XAttribute("name", "extendeddescription"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("synopsis").ToString)),
        'New XElement(xNamespace + "parameter", New XAttribute("name", "movierating"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("tmdbrating").ToString)),
        'New XElement(xNamespace + "parameter", New XAttribute("name", "specialepisode"), New XAttribute("type", ""), New XAttribute("value", dt.Rows(i)("specialepisode").ToString)),


        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function

    Public Function gen_SynergyRich_XML(ByVal dtNew1 As DataTable, ByVal vChannel As String, ByVal vServiceID As String, ByVal vOperatorChannel As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vTZ As String) As String

        'If vTZ = "" Then
        '    vTZ = "ist"
        'End If
        Dim intTZ As Integer
        Try
            intTZ = vTZ
        Catch ex As Exception
            intTZ = 0
        End Try
        Dim dtNew As DataTable = dtNew1.Select().CopyToDataTable
        For i As Integer = 0 To dtNew1.Rows.Count - 1
            Dim vStartDatetime As DateTime = dtNew1.Rows(i)("startdatetime")
            Dim vEndDatetime As DateTime = dtNew1.Rows(i)("enddatetime")
            dtNew.Rows(i)("startdatetime") = Convert.ToDateTime(vStartDatetime).AddMinutes(intTZ).ToString("MM/dd/yyyy HH:mm:ss")
            dtNew.Rows(i)("enddatetime") = Convert.ToDateTime(vEndDatetime).AddMinutes(intTZ).ToString("MM/dd/yyyy HH:mm:ss")
            dtNew.Rows(i)("startdate") = Convert.ToDateTime(vStartDatetime).AddMinutes(intTZ).Date.ToString("MM/dd/yyyy")
            dtNew.Rows(i)("enddate") = Convert.ToDateTime(vEndDatetime).AddMinutes(intTZ).Date.ToString("MM/dd/yyyy")
            dtNew.Rows(i)("starttime") = Convert.ToDateTime(vStartDatetime).AddMinutes(intTZ).ToString("HH:mm:ss")
            dtNew.Rows(i)("endtime") = Convert.ToDateTime(vEndDatetime).AddMinutes(intTZ).ToString("HH:mm:ss")
        Next


        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = "http://SourceSytem.Hostname.Interfaces/folder/1.0"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"
        Dim xsd As XNamespace = "http://www.w3.org/2001/XMLSchema"



        Dim BiNode As XElement = New XElement(
            xNamespace + "synergytoepg",
            New XAttribute("xmlns", xNamespace),
            New XAttribute(xNamespace.Xmlns + "xsi", xsi),
            New XAttribute(xNamespace.Xmlns + "xsd", xsd)
        )

        Dim dtMin As DateTime, dtMax As DateTime, intLastDur As Integer
        Dim dt As DataTable = dtNew.Select("startdate >= '" + vStartDate.Date.ToString("MM'/'dd'/'yyyy") + "' AND startdate <= '" + vEndDate.Date.ToString("MM'/'dd'/'yyyy") + "'").CopyToDataTable
        dtMin = dt.AsEnumerable().Select(Function(r) Convert.ToDateTime(r.Field(Of String)("startdatetime"))).First
        dtMax = dt.AsEnumerable().Select(Function(r) Convert.ToDateTime(r.Field(Of String)("startdatetime"))).Last
        intLastDur = dt.AsEnumerable().Select(Function(r) r.Field(Of Integer)("duration")).Last
        dtMax = dtMax.AddMinutes(intLastDur)

        'Dim siChannelNode As XElement = New XElement(xNamespace + "channelname", vServiceID)
        'Dim siDateFromNode As XElement = New XElement(xNamespace + "datefrom", vStartDate.ToString("ddMMyyyy"))
        'Dim siDateToNode As XElement = New XElement(xNamespace + "dateto", vEndDate.ToString("ddMMyyyy"))
        'Dim siStartTimeNode As XElement = New XElement(xNamespace + "channelstarttime", vStartDate.ToString("HH:mm"))

        Dim siChannelNode As XElement = New XElement(xNamespace + "channelname", vServiceID)
        Dim siDateFromNode As XElement = New XElement(xNamespace + "datefrom", dtMin.ToString("ddMMyyyy"))
        Dim siDateToNode As XElement = New XElement(xNamespace + "dateto", dtMax.ToString("ddMMyyyy"))
        Dim siStartTimeNode As XElement = New XElement(xNamespace + "channelstarttime", dtMin.ToString("HH:mm"))

        BiNode.Add(siDateFromNode)
        BiNode.Add(siDateToNode)
        BiNode.Add(siChannelNode)
        BiNode.Add(siStartTimeNode)



        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))

                    'Dim dStartDateTime As DateTime, dEndDateTime As DateTime

                    'If vTZ = "utc" Then
                    '    dStartDateTime = Convert.ToDateTime(dt.Rows(i)("utcstartdatetime").ToString).AddMinutes(intTZ)
                    '    dEndDateTime = Convert.ToDateTime(dt.Rows(i)("utcenddatetime").ToString).AddMinutes(intTZ)
                    'Else
                    'dStartDateTime = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).AddMinutes(intTZ)
                    'dEndDateTime = Convert.ToDateTime(dt.Rows(i)("enddatetime").ToString).AddMinutes(intTZ)
                    'End If

                    Dim siEventNode As XElement = New XElement(xNamespace + "schedule",
                                                           New XAttribute("id", dt.Rows(i)("rowid").ToString),
                                                           New XElement(xNamespace + "programmetitle", dt.Rows(i)("progname").ToString),
                                                           New XElement(xNamespace + "programmenumber", Convert.ToDecimal(dt.Rows(i)("progid")).ToString("500000000") & Convert.ToDecimal(IIf(dt.Rows(i)("episodeno") = "", "0", dt.Rows(i)("episodeno"))).ToString("0000")),
                                                           New XElement(xNamespace + "episodetitle", IIf(dt.Rows(i)("episodic_title").ToString = "", Nothing, dt.Rows(i)("episodic_title").ToString)),
                                                           New XElement(xNamespace + "episodenumber", IIf(dt.Rows(i)("episodeno").ToString = "", Nothing, dt.Rows(i)("episodeno").ToString)),
                                                           New XElement(xNamespace + "seriesnumber", IIf(dt.Rows(i)("seasonno").ToString = "", Nothing, dt.Rows(i)("seasonno").ToString)),
                                                           New XElement(xNamespace + "yearofrelease", IIf(dt.Rows(i)("release_year").ToString = "", Nothing, dt.Rows(i)("release_year").ToString)),
                                                           New XElement(xNamespace + "directorname", IIf(dt.Rows(i)("director").ToString = "", Nothing, dt.Rows(i)("director").ToString)),
                                                           New XElement(xNamespace + "castname", IIf(dt.Rows(i)("starcast").ToString = "", Nothing, dt.Rows(i)("starcast").ToString)),
                                                           New XElement(xNamespace + "scheduledate", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("ddMMyyyy")),
                                                           New XElement(xNamespace + "schedulestarttime", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("HH:mm")),
                                                           New XElement(xNamespace + "scheduleendtime", Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("HH:mm")),
                                                           New XElement(xNamespace + "classification", "PG13"),
                                                           New XElement(xNamespace + "synopsis1", dt.Rows(i)("synopsis").ToString),
                                                           New XElement(xNamespace + "synopsis2", dt.Rows(i)("episodic_synopsis").ToString),
                                                           New XElement(xNamespace + "synopsis3", Nothing),
                                                           New XElement(xNamespace + "genre", dt.Rows(i)("genre").ToString),
                                                           New XElement(xNamespace + "colour", Nothing),
                                                           New XElement(xNamespace + "country", IIf(dt.Rows(i)("countryorigin").ToString = "", Nothing, dt.Rows(i)("countryorigin").ToString)),
                                                           New XElement(xNamespace + "language", dt.Rows(i)("language").ToString)
                                                                        )
                    BiNode.Add(siEventNode)
                End If
            Next
        End If

        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function

    Public Function gen_Generic_XML(ByVal dtNew As DataTable, ByVal vChannel As String, ByVal vServiceID As String, ByVal vOperatorChannel As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vTZ As String) As String

        If vTZ = "" Then
            vTZ = "ist"
        End If

        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = "http://SourceSytem.Hostname.Interfaces/folder/1.0"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"
        Dim xsd As XNamespace = "http://www.w3.org/2001/XMLSchema"



        Dim BiNode As XElement = New XElement(
            xNamespace + "synergytoepg",
            New XAttribute("xmlns", xNamespace),
            New XAttribute(xNamespace.Xmlns + "xsi", xsi),
            New XAttribute(xNamespace.Xmlns + "xsd", xsd)
        )

        Dim dtMin As DateTime, dtMax As DateTime, intLastDur As Integer
        Dim dt As DataTable = dtNew.Select("startdate >= '" + vStartDate.Date.ToString("MM'/'dd'/'yyyy") + "' AND startdate <= '" + vEndDate.Date.ToString("MM'/'dd'/'yyyy") + "'").CopyToDataTable
        dtMin = dt.AsEnumerable().Select(Function(r) Convert.ToDateTime(r.Field(Of String)("startdatetime"))).First
        dtMax = dt.AsEnumerable().Select(Function(r) Convert.ToDateTime(r.Field(Of String)("startdatetime"))).Last
        intLastDur = dt.AsEnumerable().Select(Function(r) r.Field(Of Integer)("duration")).Last
        dtMax = dtMax.AddMinutes(intLastDur)

        Dim siChannelNode As XElement = New XElement(xNamespace + "channelname", vServiceID)
        Dim siDateFromNode As XElement = New XElement(xNamespace + "datefrom", dtMin.ToString("ddMMyyyy"))
        Dim siDateToNode As XElement = New XElement(xNamespace + "dateto", dtMax.ToString("ddMMyyyy"))
        Dim siStartTimeNode As XElement = New XElement(xNamespace + "channelstarttime", dtMin.ToString("HH:mm"))

        BiNode.Add(siDateFromNode)
        BiNode.Add(siDateToNode)
        BiNode.Add(siChannelNode)
        BiNode.Add(siStartTimeNode)



        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))

                    Dim dStartDateTime As DateTime, dEndDateTime As DateTime

                    If vTZ = "utc" Then
                        dStartDateTime = Convert.ToDateTime(dt.Rows(i)("utcstartdatetime").ToString)
                        dEndDateTime = Convert.ToDateTime(dt.Rows(i)("utcenddatetime").ToString)
                    Else
                        dStartDateTime = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString)
                        dEndDateTime = Convert.ToDateTime(dt.Rows(i)("enddatetime").ToString)
                    End If

                    Dim siEventNode As XElement = New XElement(xNamespace + "schedule",
                                                           New XAttribute("id", dt.Rows(i)("rowid").ToString),
                                                           New XElement(xNamespace + "programmetitle", dt.Rows(i)("progname").ToString),
                                                           New XElement(xNamespace + "programmenumber", Convert.ToDecimal(dt.Rows(i)("progid")).ToString("500000000") & Convert.ToDecimal(IIf(dt.Rows(i)("episodeno") = "", "0", dt.Rows(i)("episodeno"))).ToString("0000")),
                                                           New XElement(xNamespace + "episodetitle", IIf(dt.Rows(i)("episodic_title").ToString = "", Nothing, dt.Rows(i)("episodic_title").ToString)),
                                                           New XElement(xNamespace + "episodenumber", IIf(dt.Rows(i)("episodeno").ToString = "", Nothing, dt.Rows(i)("episodeno").ToString)),
                                                           New XElement(xNamespace + "seriesnumber", IIf(dt.Rows(i)("seasonno").ToString = "", Nothing, dt.Rows(i)("seasonno").ToString)),
                                                           New XElement(xNamespace + "yearofrelease", IIf(dt.Rows(i)("release_year").ToString = "", Nothing, dt.Rows(i)("release_year").ToString)),
                                                           New XElement(xNamespace + "directorname", IIf(dt.Rows(i)("director").ToString = "", Nothing, dt.Rows(i)("director").ToString)),
                                                           New XElement(xNamespace + "castname", IIf(dt.Rows(i)("starcast").ToString = "", Nothing, dt.Rows(i)("starcast").ToString)),
                                                           New XElement(xNamespace + "scheduledate", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("ddMMyyyy")),
                                                           New XElement(xNamespace + "schedulestarttime", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("HH:mm")),
                                                           New XElement(xNamespace + "scheduleendtime", Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("HH:mm")),
                                                           New XElement(xNamespace + "classification", Nothing),
                                                           New XElement(xNamespace + "synopsis1", dt.Rows(i)("synopsis").ToString),
                                                           New XElement(xNamespace + "synopsis2", dt.Rows(i)("episodic_synopsis").ToString),
                                                           New XElement(xNamespace + "synopsis3", Nothing),
                                                           New XElement(xNamespace + "genre", dt.Rows(i)("genre").ToString),
                                                           New XElement(xNamespace + "colour", Nothing),
                                                           New XElement(xNamespace + "country", IIf(dt.Rows(i)("countryorigin").ToString = "", Nothing, dt.Rows(i)("countryorigin").ToString)),
                                                           New XElement(xNamespace + "language", dt.Rows(i)("language").ToString)
                                                                        )
                    BiNode.Add(siEventNode)
                End If
            Next
        End If

        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function

    Public Function gen_richXMLTV_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, ByVal vOperatorChannel As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vTZ As String) As String

        If dt.Rows.Count > 0 Then
            If vChannelno <> "" Then
                vChannelno = Convert.ToInt16(vChannelno).ToString("0000")
            End If
            Dim xDoc As XDocument = New XDocument()

            Dim tvNode As XElement = New XElement("tv")
            Dim channelNode As XElement = New XElement("channel",
                                            New XAttribute("id", vChannelno & "." & vOperatorChannel.Replace(" ", "") & ".in"),
                                            New XElement("serviceid", vServiceID),
                                            New XElement("display-name", New XAttribute("lang", "en"), vOperatorChannel),
                                            New XElement("ChannelLogo", "http://epgops.ndtv.com/serveimage/jpg/160/90/" & Logger.convertStringToHex(vChannel.Replace(" ", "_")) & "/channel"))

            tvNode.Add(channelNode)


            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim siEventSNode As XElement = New XElement("programme",
                                                            New XAttribute("start", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyyMMddHHmmss") & " +0530"),
                                                            New XAttribute("stop", Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("yyyyMMddHHmmss") & " +0530"),
                                                            New XAttribute("channel", vChannelno & "." & vOperatorChannel.Replace(" ", "") & ".in"),
                                                            New XAttribute("duration", dt.Rows(i)("duration").ToString),
                                                            New XAttribute("clumpidx", "0/1"),
                                                            New XElement("programmeid", Convert.ToInt64(dt.Rows(i)("progid").ToString).ToString("5000000000")),
                                                            New XElement("title", New XAttribute("lang", "en"), dt.Rows(i)("progname").ToString),
                                                            New XElement("broadcastlanguage", dt.Rows(i)("language").ToString),
                                                            IIf(dt.Rows(i)("episodeno").ToString > "0", New XElement("episodenumber", dt.Rows(i)("episodeno").ToString), ""),
                                                            New XElement("desc", New XAttribute("lang", "en"), dt.Rows(i)("synopsis").ToString),
                                                            New XElement("category", New XAttribute("lang", "en"), dt.Rows(i)("genre").ToString),
                                                            New XElement("sub-category", New XAttribute("lang", "en"), dt.Rows(i)("subgenre").ToString),
                                                            New XElement("ImageUrl", New XAttribute("size", "648x486"), "http://epgops.ndtv.com/serveimage/jpg/648/486/" & Logger.convertStringToHex(dt.Rows(i)("progid").ToString)),
                                                            New XElement("is-repeat", IIf(dt.Rows(i)("episodetype").ToString = "repeat", "true", "false"))
                                                            )
                    tvNode.Add(siEventSNode)
                End If
            Next
            xDoc.Add(tvNode)
            Dim str As String
            Using stringWriter As New Utf8StringWriter()
                Using xmlTextWriter = XmlWriter.Create(stringWriter)

                    xDoc.WriteTo(xmlTextWriter)
                    xmlTextWriter.Flush()
                    str = stringWriter.GetStringBuilder().ToString()
                End Using
            End Using
            Return str
        Else
            Return ""
        End If


    End Function

    Public Function GetDistinctRecords(dt As DataTable, Columns As String()) As DataTable
        Dim dtUniqRecords As DataTable = New DataTable()
        dtUniqRecords = dt.DefaultView.ToTable(True, Columns)
        Return dtUniqRecords
    End Function

    Public Function gen_Generic_Single_Rich_XML(ByVal dtNew As DataTable, vStartDate As DateTime, vEndDate As DateTime, vType As String, vTZ As String) As String
        Dim intAddMins As Integer
        If vTZ = "" Then
            vTZ = "ist"
            intAddMins = 0
        ElseIf vTZ = "ist" Then
            intAddMins = 0
        ElseIf vTZ = "utc" Then
            intAddMins = -330
        ElseIf vTZ = "gmt" Then
            intAddMins = -330
        End If
        Dim xDoc As XDocument = New XDocument()

        Dim BiNode As XElement = New XElement("generic",
                                              New XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                                                New XAttribute("schemaVersion", "1.0"))



        Dim j As Integer = 1
        Dim dt As DataTable = dtNew.Clone
        For Each sourceRow As DataRow In dtNew.Rows
            If Convert.ToDateTime(sourceRow("startdatetime")).Date >= vStartDate And Convert.ToDateTime(sourceRow("enddatetime")).Date <= vEndDate Then
                dt.ImportRow(sourceRow)
            End If

        Next

        If vType = "channels" Then
            Dim BiHeaderNode As XElement = New XElement("header",
                                                   New XElement("content", "Signal List"),
                                                   New XElement("created", DateTime.Now.ToString("yyyy-MM-dd")),
                                                   New XElement("copyright", "Copyright NDTV. All rights reserved")
                                                   )
            Dim BiChannelNode As XElement = New XElement("channels")
            'Dim TobeDistinct As String() = {"serviceid", "channellogo", "channel_no"}
            Dim TobeDistinct As String() = {"serviceid", "channelid", "channel_no"}
            Dim dtDistinct As DataTable = GetDistinctRecords(dt, TobeDistinct)
            If dtDistinct.Rows.Count > 0 Then

                For i As Integer = 0 To dtDistinct.Rows.Count - 1


                    Dim siChannelNode As XElement = New XElement("channel",
                                                           New XAttribute("channelId", dtDistinct.Rows(i)("channel_no").ToString),
                                                           New XElement("name", dtDistinct.Rows(i)("serviceid").ToString),
                                                           New XElement("address",
                                                                        New XElement("country", "IN")),
                                                            New XElement("callSign", dtDistinct.Rows(i)("serviceid").ToString),
                                                           New XElement("images",
                                                                New XElement("image",
                                                                    New XAttribute("type", "image/png"),
                                                                    New XAttribute("width", "720"),
                                                                    New XAttribute("height", "540"),
                                                                    New XAttribute("primary", "true"),
                                                                    New XAttribute("category", "Logo"),
                                                                    New XElement("URI", "http://d2joc2ww338o5x.cloudfront.net/png/720/540/" & Logger.convertStringToHex(dtDistinct.Rows(i)("channelid").ToString.Replace(" ", "_")) & "/channel"))))

                    'New XElement("URI", dtDistinct.Rows(i)("channellogo").ToString))))


                    BiChannelNode.Add(siChannelNode)
                Next
            End If
            BiNode.Add(BiHeaderNode)
            BiNode.Add(BiChannelNode)
            xDoc.Add(BiNode)
        ElseIf vType = "programs" Then
            Dim BiHeaderNode As XElement = New XElement("header",
                                                   New XElement("content", "Program List"),
                                                   New XElement("created", DateTime.Now.ToString("yyyy-MM-dd")),
                                                   New XElement("copyright", "Copyright NDTV. All rights reserved")
                                                   )

            Dim BiProgramsNode As XElement = New XElement("programs")
            Dim TobeDistinct As String() = {"channelid", "progname", "synopsis", "progid", "genre", "parentalrating", "programlogo_raw", "programlogoportrait_raw", "showtype", "seasonno", "episodeno", "episodic_title", "seriesid", "starcast", "director", "writer", "imdb_rating"}
            Dim dtDistinct As DataTable = GetDistinctRecords(dt, TobeDistinct)

            Dim lstProgId As New List(Of String)
            For i As Integer = 0 To dtDistinct.Rows.Count - 1
                lstProgId.Add(dtDistinct.Rows(i)("progid").ToString & "," & dtDistinct.Rows(i)("episodeno").ToString)
            Next


            lstProgId = lstProgId.GroupBy(Function(m) m) _
                 .Where(Function(g) g.Count() > 1) _
                 .Select(Function(g) g.Key).ToList
            If lstProgId.Count > 0 Then
                Dim strString As String = ""
                For i As Integer = 0 To lstProgId.Count
                    strString = strString & lstProgId.Item(i).ToString & "<br/>"
                Next
                Logger.mailMessage("epgtech@ndtv.com", "Dhiraagu-Duplicate Values found in Result set", "Duplicate Values found in Result set<br/><b>Progid,Episode</b>" & strString, "", "")

            End If


            If dtDistinct.Rows.Count > 0 Then

                For i As Integer = 0 To dtDistinct.Rows.Count - 1

                    Dim strTier As String = "", strCategory As String
                    Dim strImgCoverURL As String = "http://d2joc2ww338o5x.cloudfront.net/png/480/720/" & Logger.convertStringToHex(dtDistinct.Rows(i)("progid").ToString)
                    Dim strImgDetailURL As String = "http://d2joc2ww338o5x.cloudfront.net/png/542/813/" & Logger.convertStringToHex(dtDistinct.Rows(i)("progid").ToString)
                    Dim strImgSceneURL As String = "http://d2joc2ww338o5x.cloudfront.net/png/1300/731/" & Logger.convertStringToHex(dtDistinct.Rows(i)("progid").ToString)

                    If dtDistinct.Rows(i)("programlogoportrait_raw").ToString = "" Then
                        strImgCoverURL = "http://d2joc2ww338o5x.cloudfront.net/png/480/720/" & Logger.convertStringToHex(dtDistinct.Rows(i)("channelid").ToString.Replace(" ", "_")) & "/channel"
                        strImgDetailURL = "http://d2joc2ww338o5x.cloudfront.net/png/542/813/" & Logger.convertStringToHex(dtDistinct.Rows(i)("channelid").ToString.Replace(" ", "_")) & "/channel"
                    End If
                    If dtDistinct.Rows(i)("programlogo_raw").ToString = "" Then
                        strImgSceneURL = "http://d2joc2ww338o5x.cloudfront.net/png/1300/731/" & Logger.convertStringToHex(dtDistinct.Rows(i)("channelid").ToString.Replace(" ", "_")) & "/channel"
                    End If

                    'Dim strProgPrefix As String
                    Dim strShowType As String = dtDistinct.Rows(i)("showtype").ToString
                    Dim strGenre As String = dtDistinct.Rows(i)("genre").ToString.ToLower
                    Dim strSeasonId As String = Convert.ToInt64(dtDistinct.Rows(i)("progid")).ToString("10000000") & Convert.ToInt32(IIf(dtDistinct.Rows(i)("seasonno") = "", "0", dtDistinct.Rows(i)("seasonno"))).ToString("00")

                    Dim strProgPrefix As String
                    If strShowType = "Feature Film" Or strShowType = "Movie" Then
                        strTier = "Movie"
                        strCategory = "Poster Art"
                        strProgPrefix = "MV"
                        strShowType = "Movies"
                    ElseIf strShowType = "Event" And (strGenre = "basketball" Or strGenre = "cricket" Or strGenre = "football" Or strGenre = "sports" Or strGenre = "wrestling") Then
                        strTier = "Series"
                        strCategory = "Banner"
                        strProgPrefix = "SP"
                        strShowType = "Other"
                    ElseIf strShowType = "Series" Then
                        strTier = "Series"
                        strCategory = "Banner"
                        If dtDistinct.Rows(i)("seriesid").ToString = "0" Then
                            strProgPrefix = "SH"
                            strShowType = "Other"
                        Else
                            strProgPrefix = "EP"
                            strShowType = "Series"
                        End If

                    Else
                        strTier = "Episode"
                        strCategory = "Banner"
                        strProgPrefix = "SH"
                        strShowType = "Other"

                    End If
                    Dim intProgid As Integer = dtDistinct.Rows(i)("progid").ToString
                    Dim intEpisodeNo As Integer = dtDistinct.Rows(i)("episodeno").ToString
                    Dim strProgId As String = intProgid.ToString(strProgPrefix & "0000000000") & intEpisodeNo.ToString("0000")
                    'Dim strSeriesId As String = strProgPrefix & Convert.ToInt64(dtDistinct.Rows(i)("seriesid")).ToString("00000000")
                    Dim strSeriesId As String = "SH" & Convert.ToInt64(dtDistinct.Rows(i)("seriesid")).ToString("00000000")

                    If strProgId = "MV00001540030000" Then
                        strProgId = strProgId
                    End If
                    Dim BiStarCastNode As XElement = New XElement("cast")
                    Dim strStarCast As String = dtDistinct.Rows(i)("starcast").ToString.Trim
                    If (strStarCast.Length > 1) Then

                        Dim arrStarCast As String() = strStarCast.Split(",")
                        'arrStarCast = arrStarCast.Distinct()

                        Dim arrListStartCast As New List(Of String)
                        For k As Integer = 0 To arrStarCast.Length - 1
                            arrListStartCast.Add(RTrim(LTrim(arrStarCast(k).ToString)))
                        Next
                        Dim arrListDistStartCast As List(Of String) = arrListStartCast.Distinct().ToList

                        Dim l As Integer = 0
                        For Each element As String In arrListDistStartCast
                            Dim strCurrStarCast As String = element
                            Dim strPersonId As String = intProgid.ToString("10000000")
                            Dim strOrderId As String = (l + 1).ToString("00")
                            Dim strFirstName As String = strCurrStarCast.Split(" ")(0)
                            Dim strLastName As String

                            If strCurrStarCast.Contains(" ") Then
                                strLastName = strCurrStarCast.Split(" ")(1)
                            Else
                                strLastName = ""
                            End If

                            Dim xStarCast As XElement = New XElement("member",
                                                                      New XAttribute("personId", strPersonId & strOrderId),
                                                                      New XAttribute("ord", strOrderId),
                                                                      New XElement("role", "Actor"),
                                                                      New XElement("name",
                                                                                   New XElement("first", strFirstName),
                                                                                   IIf(strLastName.Length > 1, New XElement("last", strLastName), New XElement("last", " "))
                                                                                   ))
                            BiStarCastNode.Add(xStarCast)
                            l = l + 1
                        Next

                        'For k As Integer = 0 To arrStarCast.Length - 1
                        '    Dim strCurrStarCast As String = arrStarCast(k).ToString.Trim
                        '    Dim strPersonId As String = intProgid.ToString("10000000")
                        '    Dim strOrderId As String = (k + 1).ToString("00")
                        '    Dim strFirstName As String = strCurrStarCast.Split(" ")(0)
                        '    Dim strLastName As String

                        '    If strCurrStarCast.Contains(" ") Then
                        '        strLastName = strCurrStarCast.Split(" ")(1)
                        '    Else
                        '        strLastName = ""
                        '    End If

                        '    Dim xStarCast As XElement = New XElement("member",
                        '                                              New XAttribute("personId", strPersonId & strOrderId),
                        '                                              New XAttribute("ord", strOrderId),
                        '                                              New XElement("role", "Actor"),
                        '                                              New XElement("name",
                        '                                                           New XElement("first", strFirstName),
                        '                                                           IIf(strLastName.Length > 1, New XElement("last", strLastName), New XElement("last", " "))
                        '                                                           ))
                        '    BiStarCastNode.Add(xStarCast)
                        'Next
                    End If


                    Dim BiCrewNode As XElement = New XElement("crew")
                    Dim strCrew As String = dtDistinct.Rows(i)("director").ToString.Trim

                    If (strCrew.Length > 1) Then
                        Dim arrCrew As String() = strCrew.Split(",")

                        Dim arrListCrew As New List(Of String)
                        For k As Integer = 0 To arrCrew.Length - 1
                            arrListCrew.Add(RTrim(LTrim(arrCrew(k).ToString)))
                        Next
                        Dim arrListDistCrew As List(Of String) = arrListCrew.Distinct().ToList

                        Dim l As Integer = 0

                        For Each element As String In arrListDistCrew
                            Dim strCurrCrew As String = element
                            Dim strPersonId As String = intProgid.ToString("20000000")
                            Dim strOrderId As String = (l + 1).ToString("00")
                            Dim strFirstName As String = strCurrCrew.Split(" ")(0)
                            Dim strLastName As String

                            If strCurrCrew.Contains(" ") Then
                                strLastName = strCurrCrew.Split(" ")(1)
                            Else
                                strLastName = ""
                            End If

                            Dim xCrew As XElement = New XElement("member",
                                                                      New XAttribute("personId", strPersonId & strOrderId),
                                                                      New XAttribute("ord", strOrderId),
                                                                      New XElement("role", "Director"),
                                                                      New XElement("name",
                                                                                   New XElement("first", strFirstName),
                                                                                   IIf(strLastName.Length > 1, New XElement("last", strLastName), New XElement("last", " "))
                                                                                   ))
                            BiCrewNode.Add(xCrew)
                            l = l + 1
                        Next
                    End If

                    strCrew = dtDistinct.Rows(i)("writer").ToString.Trim

                    If (strCrew.Length > 1) Then
                        Dim arrCrew As String() = strCrew.Split(",")

                        Dim arrListCrew As New List(Of String)
                        For k As Integer = 0 To arrCrew.Length - 1
                            arrListCrew.Add(RTrim(LTrim(arrCrew(k).ToString)))
                        Next
                        Dim arrListDistCrew As List(Of String) = arrListCrew.Distinct().ToList

                        Dim l As Integer = 0

                        For Each element As String In arrListDistCrew
                            Dim strCurrCrew As String = element
                            Dim strPersonId As String = intProgid.ToString  'intProgid.ToString("30000000")
                            Dim strOrderId As String = (l + 1).ToString("00")
                            Dim strFirstName As String = strCurrCrew.Split(" ")(0)
                            Dim strLastName As String

                            If strCurrCrew.Contains(" ") Then
                                strLastName = strCurrCrew.Split(" ")(1)
                            Else
                                strLastName = ""
                            End If

                            Dim xCrew As XElement = New XElement("member",
                                                                      New XAttribute("personId", strPersonId & strOrderId),
                                                                      New XAttribute("ord", strOrderId),
                                                                      New XElement("role", "Writer"),
                                                                      New XElement("name",
                                                                                   New XElement("first", strFirstName),
                                                                                   IIf(strLastName.Length > 1, New XElement("last", strLastName), New XElement("last", " "))
                                                                                   ))
                            BiCrewNode.Add(xCrew)
                            l = l + 1
                        Next
                    End If
                    strCrew = dtDistinct.Rows(i)("director").ToString.Trim & dtDistinct.Rows(i)("writer").ToString.Trim

                    ',
                    'IIf(dtDistinct.Rows(i)("imdb_rating").ToString <> "-",
                    'New XElement("rating",
                    '    New XAttribute("code", dtDistinct.Rows(i)("imdb_rating").ToString & "/" & "10"),
                    '    New XAttribute("ratingsBody", "IMDB Rating")), Nothing)

                    Dim siChannelNode As XElement
                    siChannelNode = New XElement("program",
                                    New XAttribute("ProgramId", strProgId),
                                    IIf(strShowType = "Series", New XAttribute("seriesId", strSeriesId), Nothing),
                                    IIf(strShowType = "Series", New XAttribute("seasonId", strSeasonId), Nothing),
                                    New XElement("titles",
                                                New XElement("title",
                                                                New XAttribute("type", "full"),
                                                                dtDistinct.Rows(i)("progname").ToString)),
                                    New XElement("descriptions",
                                        New XElement("desc",
                                                        New XAttribute("type", "series summary"),
                                                        dtDistinct.Rows(i)("synopsis").ToString),
                                        New XElement("desc",
                                                        New XAttribute("type", "plot"),
                                                        dtDistinct.Rows(i)("synopsis").ToString)
                                                    ),
                                    IIf(strStarCast.Length > 1, BiStarCastNode, Nothing),
                                    IIf(strCrew.Length > 2, BiCrewNode, Nothing),
                                    New XElement("progType", strShowType),
                                    New XElement("genres",
                                                New XElement("genre", dtDistinct.Rows(i)("genre").ToString)),
                                    New XElement("ratings",
                                        New XElement("rating",
                                            New XAttribute("code", dtDistinct.Rows(i)("parentalrating").ToString),
                                            New XAttribute("ratingsBody", "USA Parental Rating"))),
                                    IIf(strShowType = "Series", New XElement("episodeInfo",
                                                                             New XAttribute("season", IIf(dtDistinct.Rows(i)("seasonno").ToString = "", "1", dtDistinct.Rows(i)("seasonno").ToString)),
                                                                             New XAttribute("number", IIf(dtDistinct.Rows(i)("episodeno").ToString = "0", "1", dtDistinct.Rows(i)("episodeno").ToString)),
                                                                            New XElement("title", dtDistinct.Rows(i)("episodic_title").ToString)
                                                                             ), Nothing),
                                    New XElement("images",
                                        New XElement("image",
                                                New XAttribute("imageId", "1" & dtDistinct.Rows(i)("progid").ToString),
                                                New XAttribute("action", "add"),
                                                New XAttribute("tier", strTier),
                                                New XAttribute("category", strCategory),
                                                New XAttribute("layout", "Cover"),
                                                New XAttribute("type", "image/png"),
                                                New XAttribute("width", "480"),
                                                New XAttribute("height", "720"),
                                                New XAttribute("primary", "true"),
                                                New XElement("URI", strImgCoverURL)),
                                        New XElement("image",
                                                New XAttribute("imageId", "2" & dtDistinct.Rows(i)("progid").ToString),
                                                New XAttribute("action", "add"),
                                                New XAttribute("tier", strTier),
                                                New XAttribute("category", strCategory),
                                                New XAttribute("layout", "Scene"),
                                                New XAttribute("type", "image/png"),
                                                New XAttribute("width", "1300"),
                                                New XAttribute("height", "731"),
                                                New XAttribute("primary", "true"),
                                                New XElement("URI", strImgSceneURL)),
                                        New XElement("image",
                                                New XAttribute("imageId", "3" & dtDistinct.Rows(i)("progid").ToString),
                                                New XAttribute("action", "add"),
                                                New XAttribute("tier", strTier),
                                                New XAttribute("category", strCategory),
                                                New XAttribute("layout", "Detail"),
                                                New XAttribute("type", "image/png"),
                                                New XAttribute("width", "542"),
                                                New XAttribute("height", "813"),
                                                New XAttribute("primary", "true"),
                                                    New XElement("URI", strImgDetailURL))
                                            ))
                    BiProgramsNode.Add(siChannelNode)
                Next
            End If
            BiNode.Add(BiHeaderNode)
            BiNode.Add(BiProgramsNode)
            xDoc.Add(BiNode)
        ElseIf vType = "sources" Then
            Dim BiHeaderNode As XElement = New XElement("header",
                                                   New XElement("content", "Program List"),
                                                   New XElement("created", DateTime.Now.ToString("yyyy-MM-dd")),
                                                   New XElement("copyright", "Copyright NDTV. All rights reserved")
                                                   )

            Dim BiProgramsNode As XElement = New XElement("schedules")
            Dim TobeDistinct As String() = {"channel_no", "progid", "startdatetime", "duration", "episodeno", "showtype", "genre", "seriesid"}
            Dim dtDistinct As DataTable = GetDistinctRecords(dt, TobeDistinct)
            If dtDistinct.Rows.Count > 0 Then

                Dim strOldChannelNo As String = ""
                Dim siScheduleNode As XElement
                For i As Integer = 0 To dtDistinct.Rows.Count - 1
                    Dim ts As TimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(dtDistinct.Rows(i)("duration").ToString))

                    Dim dur As String = String.Format("{0:00}{1:00}", Math.Floor(ts.TotalHours), ts.Minutes)

                    Dim strShowType As String = dtDistinct.Rows(i)("showtype").ToString
                    Dim strGenre As String = dtDistinct.Rows(i)("genre").ToString.ToLower

                    Dim strProgPrefix As String
                    If strShowType = "Feature Film" Or strShowType = "Movie" Then
                        strProgPrefix = "MV"
                    ElseIf strShowType = "Event" And (strGenre = "basketball" Or strGenre = "cricket" Or strGenre = "football" Or strGenre = "sports" Or strGenre = "wrestling") Then
                        strProgPrefix = "SP"
                    ElseIf strShowType = "Series" Then
                        If dtDistinct.Rows(i)("seriesid").ToString = "0" Then
                            strProgPrefix = "SH"
                        Else
                            strProgPrefix = "EP"
                        End If
                    Else
                        strProgPrefix = "SH"
                    End If

                    Dim intProgid As Integer = dtDistinct.Rows(i)("progid").ToString
                    Dim intEpisodeNo As Integer = dtDistinct.Rows(i)("episodeno").ToString
                    Dim strProgId As String = intProgid.ToString(strProgPrefix & "0000000000") & intEpisodeNo.ToString("0000")
                    'Dim strSeriesId As String = strProgPrefix & Convert.ToInt64(dtDistinct.Rows(i)("seriesid")).ToString("00000000")
                    'Dim strSeriesId As String = "SH" & Convert.ToInt64(dtDistinct.Rows(i)("seriesid")).ToString("00000000")

                    If strOldChannelNo <> dtDistinct.Rows(i)("channel_no").ToString Then
                        siScheduleNode = New XElement("schedule",
                                        New XAttribute("channelId", dtDistinct.Rows(i)("channel_no").ToString))

                    End If
                    Dim siEventNode As XElement = New XElement("event",
                                                                        New XAttribute("programId", strProgId),
                                                                        New XAttribute("date", Convert.ToDateTime(dtDistinct.Rows(i)("startdatetime").ToString).AddMinutes(intAddMins).ToString("yyyy-MM-dd")),
                                                                        New XElement("times",
                                                                            New XElement("time", Convert.ToDateTime(dtDistinct.Rows(i)("startdatetime").ToString).AddMinutes(intAddMins).ToString("HH:mm"))),
                                                                            New XElement("duration", dur))

                    strOldChannelNo = dtDistinct.Rows(i)("channel_no").ToString

                    If strOldChannelNo = dtDistinct.Rows(i)("channel_no").ToString Then
                        siScheduleNode.Add(siEventNode)
                    End If


                    If (i = dtDistinct.Rows.Count - 1) Then
                        BiProgramsNode.Add(siScheduleNode)
                    Else
                        If (strOldChannelNo <> dtDistinct.Rows(i + 1)("channel_no").ToString) Then
                            BiProgramsNode.Add(siScheduleNode)
                        End If
                    End If



                Next
            End If
            BiNode.Add(BiHeaderNode)
            BiNode.Add(BiProgramsNode)
            xDoc.Add(BiNode)
        End If




        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str


    End Function

#End Region

#Region "Basic-Data"

#Region "XML"
#Region "English"


    Public Function gen_DVB_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean, vTZ As String, vBoolChannelPrice As Boolean) As String
        Dim doc As XmlDocument = New XmlDocument()

        If vTZ = "" Then
            vTZ = "ist"
        End If

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
        If vBoolChannelPrice = True Then
            channelAttribute.Value = vServiceID & " Rs " & dt.Rows(0)("channelprice").ToString
        Else
            channelAttribute.Value = vServiceID
        End If

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
        Dim j As Integer = 0
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
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



                    idAttribute.Value = j
                    j = j + 1
                    nameAttribute.Value = dt.Rows(i)("progname").ToString
                    If vTZ = "utc" Then
                        startAttribute.Value = Convert.ToDateTime(dt.Rows(i)("utcstartdatetime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                        stopAttribute.Value = Convert.ToDateTime(dt.Rows(i)("utcenddatetime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                        epgstartAttribute.Value = Convert.ToDateTime(dt.Rows(i)("utcstartdatetime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                        epgstopAttribute.Value = Convert.ToDateTime(dt.Rows(i)("utcenddatetime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                    Else
                        startAttribute.Value = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                        stopAttribute.Value = Convert.ToDateTime(dt.Rows(i)("enddatetime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                        epgstartAttribute.Value = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                        epgstopAttribute.Value = Convert.ToDateTime(dt.Rows(i)("enddatetime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")
                    End If

                    serviceOffAirAttribute.Value = "false"
                    scrambledAttribute.Value = "true"
                    langAttribute.Value = "eng"
                    langEAttribute.Value = "eng"
                    nl1Attribute.Value = dt.Rows(i)("nibble1").ToString
                    nl2Attribute.Value = dt.Rows(i)("nibble2").ToString
                    un1Attribute.Value = dt.Rows(i)("nibble1").ToString
                    un2Attribute.Value = dt.Rows(i)("nibble2").ToString

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
                    textNode.AppendChild(doc.CreateTextNode(IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")))
                    textENode.AppendChild(doc.CreateTextNode(IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")))

                    shortEventDescriptorNode.AppendChild(eventnameNode)
                    shortEventDescriptorNode.AppendChild(textNode)
                    extendedEventDescriptorNode.Attributes.Append(langEAttribute)
                    extendedEventDescriptorNode.AppendChild(textENode)



                    eventNode.AppendChild(shortEventDescriptorNode)
                    eventNode.AppendChild(extendedEventDescriptorNode)
                    eventNode.AppendChild(contentDescriptorNode)

                    serviceNode.AppendChild(eventNode)
                End If
            Next
        End If

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                doc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function

    Public Function gen_DVB_PSI_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean, vTZ As String, vBoolChannelPrice As Boolean) As String

        If vTZ = "" Then
            vTZ = "ist"
        End If


        Dim xDoc As XDocument = New XDocument()

        '<DVB-EPG xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" version="1">



        Dim BiNode As XElement = New XElement(
            "DVB-EPG",
            New XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            New XAttribute("version", "1")
        )



        Dim siEventSNode As XElement = New XElement("Service",
                                                    New XAttribute("id", vServiceID)
                                                    )

        Dim j As Integer = 1
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString

                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))

                    Dim strSynopsis As String
                    If vBoolSynopsis Then
                        strSynopsis = dt.Rows(i)("synopsis").ToString
                    Else
                        strSynopsis = dt.Rows(i)("progname").ToString
                    End If

                    Dim siEventNode As XElement = New XElement("Event",
                                                           New XAttribute("id", j),
                                                           New XAttribute("name", dt.Rows(i)("progname").ToString),
                                                           New XAttribute("start", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy-MM-ddTHH:mm:ssZ")),
                                                           New XAttribute("stop", Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("yyyy-MM-ddTHH:mm:ssZ")),
                                                           New XAttribute("serviceOffAir", "4"),
                                                           New XAttribute("scrambled", "true"),
                                                           New XAttribute("rating", "10"),
                                                           New XAttribute("countryCode", "IND"),
                                                           New XElement("ShortEventDescriptor",
                                                                        New XAttribute("SlanguageCode", "eng"),
                                                                        New XElement("EventName", dt.Rows(i)("progname").ToString,
                                                                            New XAttribute("characterEncoding", "22")),
                                                                        New XElement("Text", strSynopsis,
                                                                            New XAttribute("characterEncoding", "222"))
                                                                        ),
                                                            New XElement("ExtendedEventDescriptor",
                                                                        New XAttribute("ElanguageCode", "eng"),
                                                                        New XElement("Text", strSynopsis,
                                                                            New XAttribute("characterEncoding", "254"))
                                                                        ),
                                                           New XElement("ContentDescriptor",
                                                                        New XAttribute("nibbleLevel1", "0"),
                                                                        New XAttribute("nibbleLevel2", "0")
                                                                        ),
                                                           New XElement("item",
                                                                        New XElement("description", "description"),
                                                                        New XElement("text", "text")
                                                           ))
                    siEventSNode.Add(siEventNode)
                    j = j + 1
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function

    Public Function gen_Barrowa_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String

        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = "http://barrowa.com/common/epg/format/barrowa/v6"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"


        Dim BiNode As XElement = New XElement(
            xNamespace + "epg_import",
            New XAttribute("xmlns", xNamespace),
            New XAttribute("version", "6")
        )

        Dim siEventSNode As XElement = New XElement(xNamespace + "schedule",
                                                    New XAttribute("id", vServiceID)
                                                    )

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))

                    Dim siEventNode As XElement = New XElement(xNamespace + "event",
                                                           New XAttribute("start_time", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XAttribute("end_time", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XElement(xNamespace + "description",
                                                                        New XAttribute("language", "eng"),
                                                                        New XAttribute("title", dt.Rows(i)("progname").ToString),
                                                                        New XAttribute("short_synopsis", IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")),
                                                                        New XAttribute("extended_synopsis", IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, ""))
                                                                        ),
                                                           New XElement(xNamespace + "content",
                                                                        New XAttribute("nibble1", dt.Rows(i)("nibble1").ToString),
                                                                        New XAttribute("nibble2", dt.Rows(i)("nibble2").ToString))
                                                           )
                    siEventSNode.Add(siEventNode)
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function

    Public Function gen_BarrowaBex_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String

        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = "http://barrowa.com/common/epg/format/barrowa/v6"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"


        Dim BiNode As XElement = New XElement(
            xNamespace + "epg_import",
            New XAttribute("xmlns", xNamespace),
            New XAttribute("version", "6")
        )

        Dim siEventSNode As XElement = New XElement(xNamespace + "schedule",
                                                    New XAttribute("id", vServiceID)
                                                    )

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                    Dim strSynopsis As String = IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")
                    If Not (dt.Rows(i)("episodictitle").ToString = "") Then
                        strSynopsis = dt.Rows(i)("episodictitle").ToString & ": " & strSynopsis.Replace(dt.Rows(i)("episodictitle").ToString & ": ", "").Replace(dt.Rows(i)("episodictitle").ToString & " : ", "").Replace(dt.Rows(i)("episodictitle").ToString & " :", "").Replace(dt.Rows(i)("episodictitle").ToString & ":", "")
                    End If


                    Dim siEventNode As XElement = New XElement(xNamespace + "event",
                                                           New XAttribute("start_time", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XAttribute("end_time", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XElement(xNamespace + "description",
                                                                        New XAttribute("language", "eng"),
                                                                        New XAttribute("title", dt.Rows(i)("progname").ToString),
                                                                        New XAttribute("short_synopsis", strSynopsis),
                                                                        New XAttribute("extended_synopsis", strSynopsis)
                                                                        ),
                                                           New XElement(xNamespace + "content",
                                                                        New XAttribute("nibble1", dt.Rows(i)("channelnibble").ToString),
                                                                        New XAttribute("nibble2", dt.Rows(i)("nibble1").ToString))
                                                           )
                    siEventSNode.Add(siEventNode)
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function

    Public Function gen_XMLTV_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean, vBoolChannelPrice As Boolean, vTZ As Integer) As String
        If dt.Rows.Count > 0 Then
            Dim doc As XmlDocument = New XmlDocument()
            'Dim docNode As XmlNode = doc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
            'doc.AppendChild(docNode)

            Dim tvNode As XmlNode = doc.CreateElement("tv")
            doc.AppendChild(tvNode)

            Dim channelNode As XmlNode = doc.CreateElement("channel")
            Dim channelAttribute As XmlAttribute = doc.CreateAttribute("id")
            If vChannelno = "" Then
                channelAttribute.Value = vServiceID
            Else
                channelAttribute.Value = vChannelno & "." & vServiceID.Replace(" ", "") & ".in"
            End If

            channelNode.Attributes.Append(channelAttribute)
            tvNode.AppendChild(channelNode)

            Dim nameNode As XmlNode = doc.CreateElement("display-name")
            If vBoolChannelPrice Then
                nameNode.AppendChild(doc.CreateTextNode(vServiceID & " Rs " & dt.Rows(0)("channelprice").ToString))
            Else
                nameNode.AppendChild(doc.CreateTextNode(vServiceID))
            End If
            channelNode.AppendChild(nameNode)

            Dim programmeNode As XmlNode
            Dim titleNode As XmlNode
            Dim descNode As XmlNode
            Dim dateNode As XmlNode
            Dim startAttribute As XmlAttribute
            Dim stopAttribute As XmlAttribute
            Dim clumpidxAttribute As XmlAttribute

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then


                    programmeNode = doc.CreateElement("programme")
                    titleNode = doc.CreateElement("title")
                    descNode = doc.CreateElement("desc")
                    dateNode = doc.CreateElement("date")

                    'Dim dStartDateTime As DateTime = Convert.ToDateTime(dt.Rows(i)("startdate").ToString).ToString("yyyy-MM-dd") & " " & Convert.ToDateTime(dt.Rows(i)("starttime").ToString).ToString("HH:mm:ss")
                    'Dim dEndDateTime As DateTime = Convert.ToDateTime(dt.Rows(i)("enddate").ToString).ToString("yyyy-MM-dd") & " " & Convert.ToDateTime(dt.Rows(i)("endtime").ToString).ToString("HH:mm:ss")

                    Dim dStartDateTime As DateTime = Convert.ToDateTime(dt.Rows(i)("startdate").ToString).ToString("yyyy-MM-dd") & " " & Convert.ToDateTime(dt.Rows(i)("starttime").ToString).ToString("HH:mm")
                    Dim dEndDateTime As DateTime = Convert.ToDateTime(dt.Rows(i)("enddate").ToString).ToString("yyyy-MM-dd") & " " & Convert.ToDateTime(dt.Rows(i)("endtime").ToString).ToString("HH:mm")

                    dStartDateTime = dStartDateTime.AddMinutes(vTZ)
                    dEndDateTime = dEndDateTime.AddMinutes(vTZ)

                    startAttribute = doc.CreateAttribute("start")
                    startAttribute.Value = dStartDateTime.ToString("yyyyMMdd") & dStartDateTime.ToString("HHmmss") & " +0530"
                    stopAttribute = doc.CreateAttribute("stop")
                    stopAttribute.Value = dEndDateTime.ToString("yyyyMMdd") & dEndDateTime.ToString("HHmmss") & " +0530"
                    channelAttribute = doc.CreateAttribute("channel")
                    If vChannelno = "" Then
                        channelAttribute.Value = vServiceID
                    Else
                        channelAttribute.Value = vChannelno & "." & vServiceID.Replace(" ", "") & ".in"
                    End If
                    clumpidxAttribute = doc.CreateAttribute("clumpidx")
                    clumpidxAttribute.Value = "0/1"

                    programmeNode.Attributes.Append(startAttribute)
                    programmeNode.Attributes.Append(stopAttribute)
                    programmeNode.Attributes.Append(channelAttribute)
                    programmeNode.Attributes.Append(clumpidxAttribute)

                    titleNode.AppendChild(doc.CreateTextNode(dt.Rows(i)("progname").ToString))
                    descNode.AppendChild(doc.CreateTextNode(IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")))
                    dateNode.AppendChild(doc.CreateTextNode(dStartDateTime.ToString("yyyyMMdd")))



                    programmeNode.AppendChild(titleNode)
                    programmeNode.AppendChild(descNode)
                    programmeNode.AppendChild(dateNode)

                    tvNode.AppendChild(programmeNode)
                End If
            Next
            Dim str As String
            Using stringWriter As New Utf8StringWriter()
                Using xmlTextWriter = XmlWriter.Create(stringWriter)
                    doc.WriteTo(xmlTextWriter)
                    xmlTextWriter.Flush()
                    str = stringWriter.GetStringBuilder().ToString()
                End Using
            End Using
            Return str
        Else
            Return ""
        End If


    End Function

    Public Function gen_XMLTV_Single_XML(ByVal dt As DataTable, vStartDate As DateTime, vEndDate As DateTime, vTZ As String) As String
        If vTZ = "" Then
            vTZ = "ist"
        End If


        Dim xDoc As XDocument = New XDocument()



        '<DVB-EPG xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" version="1">



        Dim BiNode As XElement = New XElement("tv")
        Dim strCurrChannel As String = ""
        Dim strLastChannel As String = ""
        Dim j As Integer = 1
        If dt.Rows.Count > 0 Then

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).Date
                strCurrChannel = dt.Rows(i)("serviceid")
                Dim strChannelDisplay As String
                Dim strChannelno As String = dt.Rows(i)("channelno").ToString
                If strChannelno = "" Then
                    strChannelDisplay = strCurrChannel
                Else
                    strChannelDisplay = strChannelno & "." & strCurrChannel.Replace(" ", "") & ".in"
                End If


                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                    If strCurrChannel <> strLastChannel Then
                        Dim siChannelNode As XElement = New XElement("channel",
                                                               New XAttribute("id", strChannelDisplay),
                                                               New XElement("display-name",
                                                                            New XAttribute("lang", "en"), strCurrChannel))
                        BiNode.Add(siChannelNode)
                    End If

                    Dim siEventNode As XElement = New XElement("programme",
                                                                   New XAttribute("start", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyyMMddHHmmss") & " +0530"),
                                                                   New XAttribute("stop", Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("yyyyMMddHHmmss") & " +0530"),
                                                                   New XAttribute("channel", strChannelDisplay),
                                                                   New XElement("title",
                                                                                New XAttribute("lang", "en"),
                                                                                dt.Rows(i)("progname").ToString))
                    BiNode.Add(siEventNode)
                    j = j + 1
                End If
                strLastChannel = strCurrChannel
            Next
        End If
        'BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)


        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str


    End Function

    Public Function gen_XMLTV_Single1_XML(ByVal dt As DataTable, vStartDate As DateTime, vEndDate As DateTime, vTZ As String) As String
        Dim intTimeDiff As Integer
        If vTZ = "ist" Then
            intTimeDiff = 0
        ElseIf vTZ = "utc" Then
            intTimeDiff = -330
        ElseIf vTZ = "nst" Then
            intTimeDiff = 15
        Else
            intTimeDiff = 0
            vTZ = "ist"
        End If


        Dim xDoc As XDocument = New XDocument()



        Dim BiNode As XElement = New XElement("tv")
        Dim strOperatorChannelId As String = ""
        Dim strCurrChannel As String = ""
        Dim strLastChannel As String = ""
        Dim j As Integer = 1
        Dim siBroadCasterNode As XElement = New XElement("broadcaster",
                                                               New XAttribute("id", "2"),
                                                               New XElement("broadcaster-name", "SubisuXML"))
        BiNode.Add(siBroadCasterNode)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).Date
                strCurrChannel = dt.Rows(i)("serviceid")
                strOperatorChannelId = dt.Rows(i)("operatorchannelid")
                Dim strChannelDisplay As String
                Dim strChannelno As String = dt.Rows(i)("channelno").ToString
                If strChannelno = "" Then
                    strChannelDisplay = strCurrChannel
                Else
                    strChannelDisplay = strChannelno & "." & strCurrChannel.Replace(" ", "") & ".in"
                End If

                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                    If strCurrChannel <> strLastChannel Then

                        Dim siChannelNode As XElement = New XElement("channel",
                                                               New XAttribute("broadcaster", "2"),
                                                               New XAttribute("id", strChannelDisplay),
                                                               New XElement("channel-name", strOperatorChannelId))
                        BiNode.Add(siChannelNode)
                    End If
                    strLastChannel = strCurrChannel
                End If

            Next
        End If

        If dt.Rows.Count > 0 Then
            Dim intEventID As Integer = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).Date
                strCurrChannel = dt.Rows(i)("serviceid")
                strOperatorChannelId = dt.Rows(i)("operatorchannelid")
                Dim strChannelDisplay As String
                Dim strChannelno As String = dt.Rows(i)("channelno").ToString
                If strChannelno = "" Then
                    strChannelDisplay = strCurrChannel
                Else
                    strChannelDisplay = strChannelno & "." & strCurrChannel.Replace(" ", "") & ".in"
                End If

                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))

                    Dim siEventNode As XElement = New XElement("programme",
                                                                   New XAttribute("start", Convert.ToDateTime(dt.Rows(i)("startdatetime")).AddMinutes(intTimeDiff).ToString("yyyyMMddHHmmss")),
                                                                   New XAttribute("stop", Convert.ToDateTime(dt.Rows(i)("enddatetime")).AddMinutes(intTimeDiff).ToString("yyyyMMddHHmmss")),
                                                                   New XAttribute("channel", strChannelDisplay),
                                                                   New XAttribute("eventId", intEventID),
                                                                   New XAttribute("ev-free", "0"),
                                                                   New XAttribute("parental-code", "0"),
                                                                   New XElement("title",
                                                                                New XAttribute("lang", "en"),
                                                                                dt.Rows(i)("progname").ToString),
                                                                    New XElement("desc",
                                                                                New XAttribute("lang", "en"),
                                                                                dt.Rows(i)("synopsis").ToString),
                                                                    New XElement("credits", ""),
                                                                    New XElement("category",
                                                                                New XAttribute("lang", "en"), "1"),
                                                                    New XElement("sub-category",
                                                                                New XAttribute("lang", "en"), "16"),
                                                                    New XElement("preview",
                                                                                New XAttribute("type", "0"),
                                                                                New XElement("length",
                                                                                             New XAttribute("units", "minutes"), "0")))
                    BiNode.Add(siEventNode)
                    j = j + 1
                    intEventID = intEventID + 1
                End If
                strLastChannel = strCurrChannel
            Next
        End If
        'BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)


        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str


    End Function


    Public Function gen_BroadcastData_XML(ByVal dtNew As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean, ByVal vBoolTag As String, ByVal vTZ As String) As String
        Dim xDoc As XDocument = New XDocument()


        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"

        'Dim BiNode As XElement = New XElement(
        '    "BroadcastData",
        '    New XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
        '    New XElement("ProviderInfo",
        '        New XElement("ProviderId", "EPG"),
        '        New XElement("ProviderName", "NDTV")
        '))

        Dim BiNode As XElement = New XElement(
            "BroadcastData",
            New XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            New XAttribute("creationDate", IIf(vTZ = "utc", DateTime.Now.AddMinutes(-330).ToString("yyyyMMddHHmmss"), DateTime.Now.ToString("yyyyMMddHHmmss"))),
            New XElement("ProviderInfo",
                New XElement("ProviderId", "EPG"),
                New XElement("ProviderName", "NDTV")
        ))

        Dim dtMin As DateTime, dtMax As DateTime, intLastDur As Integer


        Dim dt As DataTable = dtNew.Clone
        For Each sourceRow As DataRow In dtNew.Rows
            If Convert.ToDateTime(sourceRow("startdate")).Date >= vStartDate And Convert.ToDateTime(sourceRow("enddate")).Date <= vEndDate Then
                dt.ImportRow(sourceRow)
            End If

        Next


        'Dim dt As DataTable = dtNew.Select("startdate >= '" + vStartDate.Date.ToString("MM'/'dd'/'yyyy") + "' AND startdate <= '" + vEndDate.Date.ToString("MM'/'dd'/'yyyy") + "'").CopyToDataTable
        dtMin = dt.AsEnumerable().Select(Function(r) Convert.ToDateTime(r.Field(Of String)("startdatetime"))).First
        dtMax = dt.AsEnumerable().Select(Function(r) Convert.ToDateTime(r.Field(Of String)("startdatetime"))).Last
        intLastDur = dt.AsEnumerable().Select(Function(r) r.Field(Of Integer)("duration")).Last
        dtMax = dtMax.AddMinutes(intLastDur)

        Dim siEventSNode As XElement = New XElement("ScheduleData")
        Dim siChannelNode As XElement = New XElement("ChannelPeriod",
                                        New XAttribute("beginTime", IIf(vTZ = "utc", dtMin.AddMinutes(-330).ToString("yyyyMMddHHmm00"), dtMin.ToString("yyyyMMddHHmm00"))),
                                        New XAttribute("endTime", IIf(vTZ = "utc", dtMax.AddMinutes(-330).ToString("yyyyMMddHHmm00"), dtMax.ToString("yyyyMMddHHmm00"))))
        Dim siChannelIdNode As XElement = New XElement("ChannelId", vServiceID)
        siChannelNode.Add(siChannelIdNode)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    'Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration")) * 60
                    Dim intDuration As Integer = (Convert.ToDateTime(Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("yyyy-MM-dd HH:mm")) - Convert.ToDateTime(Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy-MM-dd HH:mm"))).TotalSeconds
                    Dim intHours As Integer = Math.Floor(intDuration / 60)
                    Dim intMinutes As Integer = intDuration Mod 60



                    Dim siEventNode As XElement
                    'New XElement("Description", IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis"), ""))),
                    'IIf(vBoolSynopsis = True, New XElement("Description", dt.Rows(i)("synopsis")), Nothing)),
                    Dim dtBeginTime As DateTime = IIf(vTZ = "utc", Convert.ToDateTime(dt.Rows(i)("startdatetime")).AddMinutes(-330).ToString("yyyy-MM-dd HH:mm"), Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy-MM-dd HH:mm"))
                    siEventNode = New XElement("Event",
                                New XAttribute("beginTime", dtBeginTime.ToString("yyyyMMddHHmm00")),
                                New XAttribute("duration", intDuration),
                                New XElement("EventType", "S"),
                                New XElement("EpgProduction",
                                    New XElement("EpgText",
                                        New XAttribute("language", "eng"),
                                        New XElement("Name", dt.Rows(i)("progname")),
                                        IIf(vBoolSynopsis = True, New XElement("ShortDescription", dt.Rows(i)("synopsis")), IIf(vBoolTag = True, New XElement("ShortDescription", ""), Nothing)),
                                        IIf(vBoolSynopsis = True, New XElement("Description", dt.Rows(i)("synopsis")), IIf(vBoolTag = True, New XElement("Description", ""), Nothing))),
                                    New XElement("AudioInfo",
                                        New XElement("Stereo", "1"),
                                        New XElement("Dolby", "2"),
                                        New XElement("Surround", "1"),
                                        New XElement("Soundtrack", "2")),
                                    New XElement("VideoInfo",
                                        New XElement("BlackAndWhite", "1"),
                                        New XElement("WideScreen", "1")),
                                    New XElement("DvbContent",
                                        New XElement("Content",
                                            New XAttribute("nibble1", dt.Rows(i)("nibble1").ToString),
                                            New XAttribute("nibble2", dt.Rows(i)("nibble2").ToString)),
                                        New XElement("User",
                                            New XAttribute("nibble1", dt.Rows(i)("nibble1").ToString),
                                            New XAttribute("nibble2", dt.Rows(i)("nibble2").ToString)))
                                    )
                                )

                    siChannelNode.Add(siEventNode)
                End If
            Next
        End If
        siEventSNode.Add(siChannelNode)
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)



        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function
    Public Function gen_MediaNet_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean, ByVal vTZ As String) As String
        Dim xDoc As XDocument = New XDocument()

        Dim siChannelNode As XElement = New XElement("schedule")

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim siEventNode As XElement
                    Dim strBeginTime As String = IIf(vTZ = "utc", Convert.ToDateTime(dt.Rows(i)("startdatetime")).AddMinutes(-330).ToString("dd.MM.yyyy HH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("dd.MM.yyyy HH:mm:ss"))
                    Dim strEndTime As String = IIf(vTZ = "utc", Convert.ToDateTime(dt.Rows(i)("enddatetime")).AddMinutes(-330).ToString("dd.MM.yyyy HH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("dd.MM.yyyy HH:mm:ss"))
                    Dim strChangeDateTime As String = IIf(vTZ = "utc", DateTime.Now.AddMinutes(-330).ToString("dd.MM.yyyy HH:mm:ss"), DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"))

                    siEventNode = New XElement("event",
                                    New XElement("id", dt.Rows(i)("traffickey").ToString),
                                    New XElement("status", "update"),
                                    New XElement("change_date", strChangeDateTime),
                                    New XElement("start", strBeginTime),
                                    New XElement("finish", strEndTime),
                                    New XElement("channel_id", vServiceID),
                                    New XElement("title", dt.Rows(i)("progname").ToString),
                                    New XElement("details",
                                        New XElement("description", dt.Rows(i)("synopsis").ToString))
                                )

                    siChannelNode.Add(siEventNode)
                End If
            Next
        End If
        xDoc.Add(siChannelNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function

    Public Function gen_MediaNetRich_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, ByVal vTZ As String) As String
        Dim xDoc As XDocument = New XDocument()

        Dim siChannelNode As XElement = New XElement("schedule")

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim siEventNode As XElement
                    ' '' '' -330 =-30
                    ' '' '' Changes done to -30 to -330 on 15 Oct 2020
                    Dim strBeginTime As String = IIf(vTZ = "utc", Convert.ToDateTime(dt.Rows(i)("startdatetime")).AddMinutes(-330).ToString("dd.MM.yyyy HH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("dd.MM.yyyy HH:mm:ss"))
                    Dim strEndTime As String = IIf(vTZ = "utc", Convert.ToDateTime(dt.Rows(i)("enddatetime")).AddMinutes(-330).ToString("dd.MM.yyyy HH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("dd.MM.yyyy HH:mm:ss"))
                    Dim strChangeDateTime As String = IIf(vTZ = "utc", DateTime.Now.AddMinutes(-330).ToString("dd.MM.yyyy HH:mm:ss"), DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"))

                    '-------------------------StarCast Starts------------------------------
                    Dim BiTeamNode As XElement = New XElement("team")
                    Dim strStarCast As String = dt.Rows(i)("starcast").ToString.Trim
                    If (strStarCast.Length > 1) Then

                        Dim arrStarCast As String() = strStarCast.Split(",")

                        Dim arrListStartCast As New List(Of String)
                        For k As Integer = 0 To arrStarCast.Length - 1
                            arrListStartCast.Add(RTrim(LTrim(arrStarCast(k).ToString)))
                        Next

                        Dim arrListDistStartCast As List(Of String) = arrListStartCast.Distinct().ToList

                        Dim l As Integer = 0
                        For Each element As String In arrListDistStartCast
                            Dim strCurrStarCast As String = element.Trim

                            Dim xStarCast As XElement = New XElement("member",
                                                                      New XElement("role", "Actor"),
                                                                      New XElement("name", strCurrStarCast))
                            BiTeamNode.Add(xStarCast)
                            l = l + 1
                        Next

                    End If
                    '-------------------------StarCast Ends------------------------------
                    '-------------------------Crew Starts------------------------------

                    'Dim BiCrewNode As XElement = New XElement("crew")
                    Dim strCrew As String = dt.Rows(i)("director").ToString.Trim

                    If (strCrew.Length > 1) Then
                        Dim arrCrew As String() = strCrew.Split(",")

                        Dim arrListCrew As New List(Of String)
                        For k As Integer = 0 To arrCrew.Length - 1
                            arrListCrew.Add(RTrim(LTrim(arrCrew(k).ToString)))
                        Next
                        Dim arrListDistCrew As List(Of String) = arrListCrew.Distinct().ToList

                        Dim l As Integer = 0

                        For Each element As String In arrListDistCrew
                            Dim strCurrCrew As String = element.Trim

                            Dim xCrew As XElement = New XElement("member",
                                                                      New XElement("role", "Director"),
                                                                      New XElement("name", strCurrCrew)
                                                                                   )
                            BiTeamNode.Add(xCrew)
                            l = l + 1
                        Next
                    End If

                    strCrew = dt.Rows(i)("writer").ToString.Trim

                    If (strCrew.Length > 1) Then
                        Dim arrCrew As String() = strCrew.Split(",")

                        Dim arrListCrew As New List(Of String)
                        For k As Integer = 0 To arrCrew.Length - 1
                            arrListCrew.Add(RTrim(LTrim(arrCrew(k).ToString)))
                        Next
                        Dim arrListDistCrew As List(Of String) = arrListCrew.Distinct().ToList

                        Dim l As Integer = 0

                        For Each element As String In arrListDistCrew
                            Dim strCurrCrew As String = element.Trim

                            Dim xCrew As XElement = New XElement("member",
                                                                      New XElement("role", "Writer"),
                                                                      New XElement("name", strCurrCrew))
                            BiTeamNode.Add(xCrew)
                            l = l + 1
                        Next
                    End If
                    '-------------------------Crew Ends------------------------------
                    strCrew = dt.Rows(i)("director").ToString.Trim & dt.Rows(i)("writer").ToString.Trim
                    Dim strSeriesId As String = Convert.ToInt64(dt.Rows(i)("seriesid")).ToString("00000000")

                    siEventNode = New XElement("event",
                                    New XElement("id", dt.Rows(i)("traffickey").ToString),
                                    New XElement("status", "update"),
                                    New XElement("change_date", strChangeDateTime),
                                    New XElement("start", strBeginTime),
                                    New XElement("finish", strEndTime),
                                    New XElement("channel_id", vServiceID),
                                    New XElement("recomended", "0"),
                                    New XElement("recordable", IIf(dt.Rows(i)("originalrepeat").ToString = "O", "1", "0")),
                                    New XElement("title", dt.Rows(i)("progname").ToString),
                                    New XElement("details",
                                        New XElement("subtitle", dt.Rows(i)("episodictitle").ToString),
                                        New XElement("description", dt.Rows(i)("synopsis").ToString),
                                        New XElement("genre", dt.Rows(i)("genre").ToString),
                                        IIf(dt.Rows(i)("subgenre").ToString <> "", New XElement("category", dt.Rows(i)("subgenre").ToString), Nothing),
                                        IIf(dt.Rows(i)("countryorigin").ToString <> "", New XElement("country", dt.Rows(i)("countryorigin").ToString), Nothing),
                                        IIf(dt.Rows(i)("release_year").ToString <> "", New XElement("year", dt.Rows(i)("release_year").ToString), Nothing),
                                        New XElement("image", dt.Rows(i)("programlogo").ToString),
                                        New XElement("age_restriction", "16+"),
                                        IIf((strStarCast & strCrew).Length > 1, BiTeamNode, Nothing),
                                        IIf(dt.Rows(i)("seriesid").ToString.Trim <> "0", New XElement("series_id", strSeriesId), Nothing),
                                        New XElement("gallery",
                                                New XElement("image", dt.Rows(i)("programlogo").ToString))
                                        )
                                )

                    siChannelNode.Add(siEventNode)
                End If
            Next
        End If
        xDoc.Add(siChannelNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function

    Public Function gen_Listings_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim xDoc As XDocument = New XDocument()


        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"

        Dim BiNode As XElement = New XElement("listings",
                                                New XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                                                New XAttribute(xsi + "noNamespaceSchemaLocation", "mheg_pvr0_5.xsd")
                                                )

        Dim siChannelNode As XElement = New XElement("channel",
                                                        New XAttribute("name", vServiceID),
                                                        New XAttribute("name_abbreviated", vServiceID))
        Dim siEventSNode As XElement = New XElement("programmes")
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim siEventNode As XElement
                siEventNode = New XElement("programme",
                                    New XAttribute("datetime_start", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("dd'/'MM'/'yyyy HH:mm:ss")),
                                    New XAttribute("datetime_end", Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("dd'/'MM'/'yyyy HH:mm:ss")),
                                    New XAttribute("captioned", "N"),
                                    New XElement("title", dt.Rows(i)("progname").ToString),
                                    New XElement("synopsis", IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")))

                siEventSNode.Add(siEventNode)
            Next
        End If
        siChannelNode.Add(siEventSNode)
        BiNode.Add(siChannelNode)
        xDoc.Add(BiNode)



        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function

    Public Function gen_BasicImport_XML(ByVal dtNew As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String

        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = "http://www.uk.nds.com/SSR/XTI/Traffic/0011"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"

        Dim BiNode As XElement = New XElement(
            xNamespace + "BasicImport",
            New XAttribute("xmlns", "http://www.uk.nds.com/SSR/XTI/Traffic/0011"),
            New XAttribute(xNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            New XAttribute(xsi + "schemaLocation", "http://www.uk.nds.com/SSR/XTI/Traffic/0011 0011.xsd"),
            New XAttribute("utcOffset", "+05:30"),
            New XAttribute("frameRate", "25")
        )

        'For x As Integer = dt.Rows.Count - 1 To 0 Step -1
        '    Dim vCurrdate As DateTime = Convert.ToDateTime(dt.Rows(x)("startdatetime").ToString).Date
        '    If Not (vCurrdate >= vStartDate And vCurrdate <= vEndDate) Then
        '        dt.Rows.RemoveAt(x)
        '    End If
        'Next x


        Dim dtMin As DateTime, dtMax As DateTime, intLastDur As Integer
        Dim dt As DataTable = dtNew.Select("startdate >= '" + vStartDate.Date.ToString("MM'/'dd'/'yyyy") + "' AND startdate <= '" + vEndDate.Date.ToString("MM'/'dd'/'yyyy") + "'").CopyToDataTable
        dtMin = dt.AsEnumerable().Select(Function(r) Convert.ToDateTime(r.Field(Of String)("startdatetime"))).First
        dtMax = dt.AsEnumerable().Select(Function(r) Convert.ToDateTime(r.Field(Of String)("startdatetime"))).Last
        intLastDur = dt.AsEnumerable().Select(Function(r) r.Field(Of Integer)("duration")).Last
        dtMax = dtMax.AddMinutes(intLastDur)
        dtMax = dtMax.AddSeconds(-1)

        Dim siEventSNode As XElement = New XElement(xNamespace + "SiEventSchedule",
                                New XAttribute("deleteStart", dtMin.ToString("yyyy'/'MM'/'dd HH:mm:ss")),
                                New XAttribute("deleteEnd", dtMax.ToString("yyyy'/'MM'/'dd HH:mm:ss")),
                                New XElement(xNamespace + "siService", vServiceID),
                                New XElement(xNamespace + "playoutSource", vServiceID),
                                New XElement(xNamespace + "activationSourceId", "0"),
                                New XElement(xNamespace + "caMode", "Scrambled")
                                           )

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                Dim intHours As Integer = Math.Floor(intDuration / 60)
                Dim intMinutes As Integer = intDuration Mod 60

                Dim dtDuration As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, intHours, intMinutes, 0)
                Dim siEventNode As XElement = New XElement(xNamespace + "SiEvent",
                                                       New XElement(xNamespace + "displayDateTime", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy'/'MM'/'dd HH:mm:ss")),
                                                       New XElement(xNamespace + "activationDateTime", Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy'/'MM'/'dd HH:mm:ss")),
                                                       New XElement(xNamespace + "displayDuration", dtDuration.ToString("HH:mm:ss")),
                                                       New XElement(xNamespace + "siTrafficKey", dt.Rows(i)("traffickey")),
                                                       New XElement(xNamespace + "detailKey", dt.Rows(i)("traffickey") & " " & Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy'-'MM'-'dd"))
                                                       )

                Dim siEDNode As XElement = New XElement(xNamespace + "SiEventDetail",
                                                        New XAttribute("action", "set"),
                                                            New XElement(xNamespace + "parentalRating", "Undefined"),
                                                            New XElement(xNamespace + "genreId", dt.Rows(i)("dengenreid")),
                                                            New XElement(xNamespace + "subGenreId", dt.Rows(i)("densubgenreid"))
                                                            )

                Dim siEDescNode As XElement = New XElement(xNamespace + "SiEventDescription",
                                                                New XElement(xNamespace + "displayLanguage", "eng"),
                                                                New XElement(xNamespace + "eventName", dt.Rows(i)("progname")),
                                                                New XElement(xNamespace + "eventDescription", IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis"), ""))
                                                                )

                'If dt.Rows(i)("episodeno") > 0 Then
                '    Dim siPKNode As XElement = New XElement("programKey", dt.Rows(i)("programKey"))


                '    siEDNode.Add(siPKNode)

                'End If

                siEDNode.Add(siEDescNode)

                'If dt.Rows(i)("episode") > 0 Then
                '    Dim siPGLNode As XElement = New XElement("SiProgramGroupLink",
                '                                           New XElement("detailKey", dt.Rows(i)("traffickey") & " " & Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy'-'MM'-'dd")),
                '                                           New XElement("groupKey", dt.Rows(i)("groupkey")),
                '                                           New XElement("groupType", "Series")
                '                                           )
                '    siEDNode.Add(siPGLNode)
                'End If


                siEventNode.Add(siEDNode)
                siEventSNode.Add(siEventNode)
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)



        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)
                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function

    Public Function gen_ProgramGuide_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean, vBoolChannelPrice As Boolean, ByVal vTZ As String) As String
        Dim xDoc As XDocument = New XDocument()
        Dim xNamespace As XNamespace = ""
        Dim BiNode As XElement = New XElement(
            xNamespace + "ProgramGuide"
        )
        Dim siEventSNode As XElement = New XElement(xNamespace + "Service",
                                                    New XAttribute("id", vChannelno),
                                                    New XAttribute("name", vServiceID)
                                                    )

        'New XElement(xNamespace + "ExtendedEventDescription",
        '                                                                    New XElement(xNamespace + "LanguageCode", "eng"),
        '                                                                    New XElement(xNamespace + "LanguageName", "English"),
        '                                                                    New XElement(xNamespace + "EventName", dt.Rows(i)("progname").ToString),
        '                                                                    New XElement(xNamespace + "EventDescription", IIf(dt.Rows(i)("synopsis").ToString = "", dt.Rows(i)("progname").ToString, dt.Rows(i)("synopsis").ToString))
        '                                                                    ),
        Dim j As Integer = 1
        Dim prevDate As DateTime
        If dt.Rows.Count > 0 Then
            Dim siScheduleNode As XElement = Nothing
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                    Dim strSynopsis As String
                    If vBoolSynopsis Then
                        strSynopsis = IIf(dt.Rows(i)("synopsis").ToString = "", dt.Rows(i)("progname").ToString, dt.Rows(i)("synopsis").ToString)
                    Else
                        strSynopsis = ""
                    End If
                    Dim dtStartDateTime As DateTime = IIf(vTZ = "utc", Convert.ToDateTime(dt.Rows(i)("startdatetime")).AddMinutes(-330).ToString("yyyy-MM-dd HH:mm"), Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy-MM-dd HH:mm"))
                    Dim dtEndDateTime As DateTime = IIf(vTZ = "utc", Convert.ToDateTime(dt.Rows(i)("enddatetime")).AddMinutes(-330).ToString("yyyy-MM-dd HH:mm"), Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("yyyy-MM-dd HH:mm"))
                    'IIf(vTZ = "utc", Convert.ToDateTime(dt.Rows(i)("enddatetime")).AddMinutes(-330).ToString("yyyy-MM-ddTHH:mm:ss"), Convert.ToDateTime(dt.Rows(i)("enddatetime")).ToString("yyyy-MM-ddTHH:mm:ss"))

                    Dim siEventNode As XElement = New XElement(xNamespace + "Event",
                        New XAttribute("id", dt.Rows(i)("traffickey").ToString),
                        New XElement(xNamespace + "IdentificationName", dt.Rows(i)("progname").ToString),
                        New XElement(xNamespace + "StartDateTime", dtStartDateTime.ToString("yyyy-MM-dd HH:mm:ss")),
                        New XElement(xNamespace + "EndDateTime", dtEndDateTime.ToString("yyyy-MM-dd HH:mm:ss")),
                        New XElement(xNamespace + "ShortEventDescription",
                            New XElement(xNamespace + "LanguageCode", "eng"),
                            New XElement(xNamespace + "LanguageName", "English"),
                            New XElement(xNamespace + "EventName", dt.Rows(i)("progname").ToString),
                            IIf(vBoolSynopsis = True, New XElement(xNamespace + "EventDescription", strSynopsis), Nothing)
                        ),
                        New XElement(xNamespace + "EventContent",
                                        New XElement(xNamespace + "ContentNibble1", dt.Rows(i)("nibble1").ToString),
                                        New XElement(xNamespace + "ContentNibble2", dt.Rows(i)("nibble2").ToString),
                                        New XElement(xNamespace + "UserNibble1", dt.Rows(i)("nibble1").ToString),
                                        New XElement(xNamespace + "UserNibble2", dt.Rows(i)("nibble2").ToString)
                                        ),
                        New XElement(xNamespace + "EventRating",
                            New XElement(xNamespace + "CountryCode", "IND"),
                            New XElement(xNamespace + "CountryName", "INDIA"),
                            New XElement(xNamespace + "Rating", "0")
                            )
                        )



                    If prevDate.Date <> dCurrDate.Date Then
                        If Not (Year(prevDate.Date) < Year(DateTime.Now.Date) - 2) Then
                            siEventSNode.Add(siScheduleNode)
                        End If

                        siScheduleNode = New XElement(xNamespace + "ScheduleDay",
                                                           New XAttribute("date", IIf(vTZ = "utc", Convert.ToDateTime(dt.Rows(i)("startdatetime")).AddMinutes(-330).ToString("yyyy/MM/dd"), Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy/MM/dd"))))
                        siScheduleNode.Add(siEventNode)
                    Else
                        siScheduleNode.Add(siEventNode)
                    End If
                    prevDate = dCurrDate.Date

                End If

                j = j + 1
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function


    'For Independent TV
    Public Function gen_CEPG_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean, vTZ As String) As String

        If vTZ = "" Then
            vTZ = "ist"
        End If


        Dim xDoc As XDocument = New XDocument()

        '<DVB-EPG xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" version="1">

        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"

        Dim BiNode As XElement = New XElement(
            "CEPG",
            New XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            New XAttribute(XNamespace.Xmlns + "epgtype", "urn:epgtype"),
            New XAttribute(xsi + "schemaLocation", "http://shumashixun.org/xs/cepg cepg.xsd")
        )

        Dim NETWORKSNode As XElement = New XElement("NETWORKS",
                                            New XElement("orgnetwork",
                                                 New XAttribute("id", "1"),
                                                 New XAttribute("name", "1"),
                                                 New XAttribute("desc", ""),
                                                 New XAttribute("lang", ""),
                                                 New XAttribute("location", ""),
                                                 New XAttribute("link", "")))

        Dim BOUQUETSNode As XElement = New XElement("BOUQUETS",
                                            New XElement("bouquet",
                                                 New XAttribute("id", ""),
                                                 New XAttribute("name", ""),
                                                 New XAttribute("desc", ""),
                                                 New XAttribute("lang", ""),
                                                 New XAttribute("location", ""),
                                                 New XAttribute("link", "")))

        Dim SERVICESNode As XElement = New XElement("SERVICES",
                                           New XElement("service",
                                                New XAttribute("on", "1"),
                                                New XAttribute("n", "1"),
                                                New XAttribute("b", ""),
                                                New XAttribute("t", "1"),
                                                New XAttribute("s", "1"),
                                                New XAttribute("name", vServiceID),
                                                New XAttribute("desc", ""),
                                                New XAttribute("type", ""),
                                                New XAttribute("ca_mode", "true"),
                                                New XAttribute("lang", ""),
                                                New XAttribute("link", "")),
                                            New XElement("service",
                                                New XAttribute("on", "2"),
                                                New XAttribute("n", "1"),
                                                New XAttribute("b", ""),
                                                New XAttribute("t", "2"),
                                                New XAttribute("s", "2"),
                                                New XAttribute("name", "22"),
                                                New XAttribute("desc", ""),
                                                New XAttribute("type", ""),
                                                New XAttribute("ca_mode", "true"),
                                                New XAttribute("lang", ""),
                                                New XAttribute("link", "")
                                                ))

        Dim PROGRAMLISTNode As XElement = New XElement("PROGRAMLIST",
                                           New XElement("CATEGORIES",
                                                New XElement("c", "",
                                                    New XAttribute("id", ""),
                                                    New XAttribute("name", ""),
                                                    New XAttribute("pname", ""),
                                                    New XAttribute("desc", ""),
                                                    New XAttribute("lang", ""))),
                                        New XElement("GENRES",
                                                New XElement("g", "",
                                                    New XAttribute("id", ""),
                                                    New XAttribute("name", ""),
                                                    New XAttribute("pname", ""),
                                                    New XAttribute("desc", ""),
                                                    New XAttribute("lang", ""))),
                                        New XElement("ROLES",
                                                New XElement("names"),
                                                New XElement("values")),
                                        New XElement("RATINGS",
                                                New XElement("countries"),
                                                New XElement("ages")))
        Dim PROGRAMSNode As XElement = New XElement("PROGRAMS")

        Dim j As Integer = 1

        j = 1

        Dim dtnew As New DataTable

        Dim dc1 As New DataColumn("id", System.Type.GetType("System.String"))
        Dim dc2 As New DataColumn("name", System.Type.GetType("System.String"))
        Dim dc3 As New DataColumn("desc", System.Type.GetType("System.String"))

        dtnew.Columns.Add(dc1)
        dtnew.Columns.Add(dc2)
        dtnew.Columns.Add(dc3)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                    Dim dr As DataRow
                    dr = dtnew.NewRow
                    dr.Item("id") = dt.Rows(i)("progid").ToString
                    dr.Item("name") = dt.Rows(i)("progname").ToString
                    dr.Item("desc") = dt.Rows(i)("genericsynopsis").ToString

                    dtnew.Rows.Add(dr)
                    j = j + 1
                End If
            Next
        End If

        Dim dv As New DataView(dtnew)
        Dim ddt As DataTable = dv.ToTable(True)
        If ddt.Rows.Count > 0 Then
            For i As Integer = 0 To ddt.Rows.Count - 1

                Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))

                Dim siProgramNode As XElement = New XElement("p",
                                                New XAttribute("id", ddt.Rows(i)("id").ToString),
                                                New XAttribute("name", ddt.Rows(i)("name").ToString),
                                                New XAttribute("shortname", ""),
                                                New XAttribute("shortdesc", ddt.Rows(i)("desc").ToString),
                                                New XAttribute("lang", ""),
                                                New XAttribute("link", ""))
                PROGRAMSNode.Add(siProgramNode)
            Next
        End If



        PROGRAMLISTNode.Add(PROGRAMSNode)


        Dim siEventSNode As XElement = New XElement("SCHEDULES")

        j = 1
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString

                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                    Dim siEventNode As XElement = New XElement("s",
                                                           New XAttribute("on", "1"),
                                                           New XAttribute("t", vChannelno),
                                                           New XAttribute("c", "1"),
                                                           New XAttribute("id", j),
                                                           New XAttribute("s", Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).ToString("yyyy-MM-dd HH:mm:ss")),
                                                           New XAttribute("d", Convert.ToInt32(dt.Rows(i)("duration").ToString) * 60),
                                                           New XAttribute("p", dt.Rows(i)("progid").ToString),
                                                           New XAttribute("ca_mode", "true")
                                                           )
                    siEventSNode.Add(siEventNode)
                    j = j + 1
                End If

            Next
        End If
        BiNode.Add(NETWORKSNode)
        BiNode.Add(BOUQUETSNode)
        BiNode.Add(SERVICESNode)
        BiNode.Add(PROGRAMLISTNode)
        BiNode.Add(siEventSNode)


        Dim PROVIDERSnode As XElement = New XElement("PROVIDERS",
                                                New XAttribute("id", ""),
                                                New XAttribute("name", ""),
                                                New XAttribute("link", ""),
                                                New XAttribute("language", ""))

        Dim LINKSnode As XElement = New XElement("LINKS",
                                                 New XElement("link", "link"))


        BiNode.Add(PROVIDERSnode)
        BiNode.Add(LINKSnode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New GB2312StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function


#End Region

#Region "Tamil"
    Public Function gen_DVBTamil_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = ""
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"


        Dim BiNode As XElement = New XElement(
            xNamespace + "DVB-EPG",
            New XAttribute(xNamespace.Xmlns + "xsi", xsi),
            New XAttribute("version", "1")
        )

        Dim siEventSNode As XElement = New XElement(xNamespace + "Service",
                                                    New XAttribute("id", vServiceID)
                                                    )

        If dt.Rows.Count > 0 Then
            Dim j As Integer = 0

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))

                    Dim siEventNode As XElement = New XElement(xNamespace + "Event",
                                                           New XAttribute("id", j),
                                                           New XAttribute("name", dt.Rows(i)("progname").ToString),
                                                           New XAttribute("start", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime").ToString).ToString("yyyy-MM-ddTHH:mm:ssZ")),
                                                           New XAttribute("stop", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyy-MM-ddTHH:mm:ssZ")),
                                                           New XAttribute("epgStart", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("yyyy-MM-ddTHH:mm:ssZ")),
                                                           New XAttribute("epgStop", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyy-MM-ddTHH:mm:ssZ")),
                                                           New XAttribute("serviceOffAir", "false"),
                                                           New XAttribute("scrambled", "true"),
                                                           New XElement(xNamespace + "ShortEventDescriptor",
                                                                        New XAttribute("languageCode", "eng"),
                                                                        New XElement(xNamespace + "EventName", dt.Rows(i)("progname").ToString),
                                                                        New XElement(xNamespace + "Text", IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, ""))
                                                                        ),
                                                           New XElement(xNamespace + "ExtendedEventDescriptor",
                                                                        New XAttribute("languageCode", "eng"),
                                                                        New XElement(xNamespace + "Text", IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, ""))),
                                                            New XElement(xNamespace + "ShortEventDescriptor",
                                                                        New XAttribute("languageCode", "tam"),
                                                                        New XElement(xNamespace + "EventName", dt.Rows(i)("progname_tam").ToString),
                                                                        New XElement(xNamespace + "Text", IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis_tam").ToString, ""))
                                                                        ),
                                                           New XElement(xNamespace + "ExtendedEventDescriptor",
                                                                        New XAttribute("languageCode", "tam"),
                                                                        New XElement(xNamespace + "Text", IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis_tam").ToString, ""))),
                                                            New XElement(xNamespace + "ContentDescriptor",
                                                                New XAttribute("nibbleLevel1", dt.Rows(i)("nibble1").ToString),
                                                                New XAttribute("nibbleLevel2", dt.Rows(i)("nibble2").ToString),
                                                                New XAttribute("userNibble1", dt.Rows(i)("nibble1").ToString),
                                                                New XAttribute("userNibble2", dt.Rows(i)("nibble2").ToString))
                                                                        )
                    siEventSNode.Add(siEventNode)
                    j = j + 1
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function

#End Region

#Region "Bengali"
    Public Function gen_BarrowaBexEnglish_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim xDoc As XDocument = New XDocument()
        Dim xNamespace As XNamespace = "http://barrowa.com/common/epg/format/barrowa/v6"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"
        Dim BiNode As XElement = New XElement(
            xNamespace + "epg_import",
            New XAttribute("xmlns", xNamespace),
            New XAttribute("version", "6")
        )
        Dim siEventSNode As XElement = New XElement(xNamespace + "schedule",
                                                    New XAttribute("id", vServiceID)
                                                    )
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                    Dim strSynopsis As String = IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")
                    If Not (dt.Rows(i)("episodictitle").ToString = "") Then
                        strSynopsis = dt.Rows(i)("episodictitle").ToString & ": " & strSynopsis.Replace(dt.Rows(i)("episodictitle").ToString & ": ", "").Replace(dt.Rows(i)("episodictitle").ToString & " : ", "").Replace(dt.Rows(i)("episodictitle").ToString & " :", "").Replace(dt.Rows(i)("episodictitle").ToString & ":", "")
                    End If

                    Dim strNibble1 As String = dt.Rows(i)("bexnibble1").ToString
                    Dim strNibble2 As String = dt.Rows(i)("bexnibble2").ToString
                    Dim strUserNibble1 As String = dt.Rows(i)("bexnibble3").ToString
                    Dim strUserNibble2 As String = dt.Rows(i)("bexnibble4").ToString

                    Dim siEventNode As XElement = New XElement(xNamespace + "event",
                                New XAttribute("start_time", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                New XAttribute("end_time", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                New XElement(xNamespace + "description",
                                            New XAttribute("language", "eng"),
                                            New XAttribute("title", dt.Rows(i)("progname").ToString),
                                            New XAttribute("short_synopsis", strSynopsis),
                                            New XAttribute("extended_synopsis", "")
                                            ),
                                New XElement(xNamespace + "content",
                                            New XAttribute("nibble1", strNibble1),
                                            New XAttribute("nibble2", strNibble2),
                                            New XAttribute("user_nibble1", strUserNibble1),
                                            New XAttribute("user_nibble2", strUserNibble2)
                                            )
                                )
                    siEventSNode.Add(siEventNode)
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function

    Public Function gen_BarrowaBexEnglishBengali_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim xDoc As XDocument = New XDocument()
        Dim xNamespace As XNamespace = "http://barrowa.com/common/epg/format/barrowa/v6"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"
        Dim BiNode As XElement = New XElement(
            xNamespace + "epg_import",
            New XAttribute("xmlns", xNamespace),
            New XAttribute("version", "6")
        )
        Dim siEventSNode As XElement = New XElement(xNamespace + "schedule",
                                                    New XAttribute("id", vServiceID)
                                                    )
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                    Dim strSynopsis As String = IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")
                    If Not (dt.Rows(i)("episodictitle").ToString = "") Then
                        strSynopsis = dt.Rows(i)("episodictitle").ToString & ": " & strSynopsis.Replace(dt.Rows(i)("episodictitle").ToString & ": ", "").Replace(dt.Rows(i)("episodictitle").ToString & " : ", "").Replace(dt.Rows(i)("episodictitle").ToString & " :", "").Replace(dt.Rows(i)("episodictitle").ToString & ":", "")
                    End If

                    Dim strNibble1 As String = dt.Rows(i)("bexnibble1").ToString
                    Dim strNibble2 As String = dt.Rows(i)("bexnibble2").ToString
                    Dim strUserNibble1 As String = dt.Rows(i)("bexnibble3").ToString
                    Dim strUserNibble2 As String = dt.Rows(i)("bexnibble4").ToString

                    Dim siEventNode As XElement = New XElement(xNamespace + "event",
                                New XAttribute("start_time", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                New XAttribute("end_time", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                New XElement(xNamespace + "description",
                                            New XAttribute("language", "eng"),
                                            New XAttribute("title", dt.Rows(i)("progname").ToString),
                                            New XAttribute("short_synopsis", strSynopsis),
                                            New XAttribute("extended_synopsis", strSynopsis)
                                            ),
                            New XElement(xNamespace + "description",
                                            New XAttribute("language", "ben"),
                                            New XAttribute("title", dt.Rows(i)("progname_ben").ToString),
                                            New XAttribute("short_synopsis", dt.Rows(i)("synopsis_ben").ToString),
                                            New XAttribute("extended_synopsis", dt.Rows(i)("synopsis_ben").ToString)
                                            ),
                                New XElement(xNamespace + "content",
                                            New XAttribute("nibble1", strNibble1),
                                            New XAttribute("nibble2", strNibble2),
                                            New XAttribute("user_nibble1", strUserNibble1),
                                            New XAttribute("user_nibble2", strUserNibble2)
                                            )
                                )
                    siEventSNode.Add(siEventNode)
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function
    Public Function gen_BarrowaBexBengali_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim xDoc As XDocument = New XDocument()
        Dim xNamespace As XNamespace = "http://barrowa.com/common/epg/format/barrowa/v6"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"
        Dim BiNode As XElement = New XElement(
            xNamespace + "epg_import",
            New XAttribute("xmlns", xNamespace),
            New XAttribute("version", "6")
        )
        Dim siEventSNode As XElement = New XElement(xNamespace + "schedule",
                                                    New XAttribute("id", vServiceID)
                                                    )
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                    Dim strSynopsis As String = IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")
                    If Not (dt.Rows(i)("episodictitle").ToString = "") Then
                        strSynopsis = dt.Rows(i)("episodictitle").ToString & ": " & strSynopsis.Replace(dt.Rows(i)("episodictitle").ToString & ": ", "").Replace(dt.Rows(i)("episodictitle").ToString & " : ", "").Replace(dt.Rows(i)("episodictitle").ToString & " :", "").Replace(dt.Rows(i)("episodictitle").ToString & ":", "")
                    End If

                    Dim strNibble1 As String = dt.Rows(i)("bexnibble1").ToString
                    Dim strNibble2 As String = dt.Rows(i)("bexnibble2").ToString
                    Dim strUserNibble1 As String = dt.Rows(i)("bexnibble3").ToString
                    Dim strUserNibble2 As String = dt.Rows(i)("bexnibble4").ToString

                    Dim siEventNode As XElement = New XElement(xNamespace + "event",
                                New XAttribute("start_time", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                New XAttribute("end_time", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyyMMdd HH:mm:ss")),
                            New XElement(xNamespace + "description",
                                            New XAttribute("language", "ben"),
                                            New XAttribute("title", dt.Rows(i)("progname_ben").ToString),
                                            New XAttribute("short_synopsis", dt.Rows(i)("synopsis_ben").ToString),
                                            New XAttribute("extended_synopsis", dt.Rows(i)("synopsis_ben").ToString)
                                            ),
                                New XElement(xNamespace + "content",
                                            New XAttribute("nibble1", strNibble1),
                                            New XAttribute("nibble2", strNibble2),
                                            New XAttribute("user_nibble1", strUserNibble1),
                                            New XAttribute("user_nibble2", strUserNibble2)
                                            )
                                )
                    siEventSNode.Add(siEventNode)
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str

    End Function
#End Region
#Region "Kannada"
    Public Function gen_BarrowaKannada_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = "http://barrowa.com/common/epg/format/barrowa/v6"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"


        Dim BiNode As XElement = New XElement(
            xNamespace + "epg_import",
            New XAttribute("xmlns", xNamespace),
            New XAttribute("version", "6")
        )

        Dim siEventSNode As XElement = New XElement(xNamespace + "schedule",
                                                    New XAttribute("id", vServiceID)
                                                    )

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                    Dim strSynopsis As String = IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")
                    If Not (dt.Rows(i)("episodictitle").ToString = "") Then
                        strSynopsis = dt.Rows(i)("episodictitle").ToString & ": " & strSynopsis.Replace(dt.Rows(i)("episodictitle").ToString & ": ", "").Replace(dt.Rows(i)("episodictitle").ToString & " : ", "").Replace(dt.Rows(i)("episodictitle").ToString & " :", "").Replace(dt.Rows(i)("episodictitle").ToString & ":", "")
                    End If


                    Dim siEventNode As XElement = New XElement(xNamespace + "event",
                                                           New XAttribute("start_time", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XAttribute("end_time", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XElement(xNamespace + "description",
                                                                        New XAttribute("language", "eng"),
                                                                        New XAttribute("title", dt.Rows(i)("progname_ben").ToString),
                                                                        New XAttribute("short_synopsis", dt.Rows(i)("synopsis_kan").ToString),
                                                                        New XAttribute("extended_synopsis", dt.Rows(i)("synopsis_kan").ToString)
                                                                        ),
                                                           New XElement(xNamespace + "content",
                                                                        New XAttribute("nibble1", dt.Rows(i)("channelnibble").ToString),
                                                                        New XAttribute("nibble2", dt.Rows(i)("nibble1").ToString))
                                                           )
                    siEventSNode.Add(siEventNode)
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function
    Public Function gen_BarrowaTamil_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = "http://barrowa.com/common/epg/format/barrowa/v6"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"


        Dim BiNode As XElement = New XElement(
            xNamespace + "epg_import",
            New XAttribute("xmlns", xNamespace),
            New XAttribute("version", "6")
        )

        Dim siEventSNode As XElement = New XElement(xNamespace + "schedule",
                                                    New XAttribute("id", vServiceID)
                                                    )

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))
                    'Dim strSynopsis As String = IIf(vBoolSynopsis = True, dt.Rows(i)("synopsis").ToString, "")
                    'If Not (dt.Rows(i)("episodictitle").ToString = "") Then
                    '    strSynopsis = dt.Rows(i)("episodictitle").ToString & ": " & strSynopsis.Replace(dt.Rows(i)("episodictitle").ToString & ": ", "").Replace(dt.Rows(i)("episodictitle").ToString & " : ", "").Replace(dt.Rows(i)("episodictitle").ToString & " :", "").Replace(dt.Rows(i)("episodictitle").ToString & ":", "")
                    'End If


                    Dim siEventNode As XElement = New XElement(xNamespace + "event",
                                                           New XAttribute("start_time", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XAttribute("end_time", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XElement(xNamespace + "description",
                                                                        New XAttribute("language", "tam"),
                                                                        New XAttribute("title", dt.Rows(i)("progname_tam").ToString),
                                                                        New XAttribute("short_synopsis", dt.Rows(i)("synopsis_tam").ToString),
                                                                        New XAttribute("extended_synopsis", dt.Rows(i)("synopsis_tam").ToString)
                                                                        ),
                                                           New XElement(xNamespace + "content",
                                                                        New XAttribute("nibble1", dt.Rows(i)("channelnibble").ToString),
                                                                        New XAttribute("nibble2", dt.Rows(i)("nibble1").ToString))
                                                           )
                    siEventSNode.Add(siEventNode)
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function
    Public Function gen_BarrowaBengali_XML(ByVal dt As DataTable, ByVal vChannel As String, ByVal vServiceID As String, vChannelno As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim xDoc As XDocument = New XDocument()

        Dim xNamespace As XNamespace = "http://barrowa.com/common/epg/format/barrowa/v6"
        Dim xsi As XNamespace = "http://www.w3.org/2001/XMLSchema-instance"


        Dim BiNode As XElement = New XElement(
            xNamespace + "epg_import",
            New XAttribute("xmlns", xNamespace),
            New XAttribute("version", "6")
        )

        Dim siEventSNode As XElement = New XElement(xNamespace + "schedule",
                                                    New XAttribute("id", vServiceID)
                                                    )

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = dt.Rows(i)("startdate").ToString
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then

                    Dim intDuration As Integer = Convert.ToInt16(dt.Rows(i)("duration"))


                    Dim siEventNode As XElement = New XElement(xNamespace + "event",
                                                           New XAttribute("start_time", Convert.ToDateTime(dt.Rows(i)("utcstartdatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XAttribute("end_time", Convert.ToDateTime(dt.Rows(i)("utcenddatetime")).ToString("yyyyMMdd HH:mm:ss")),
                                                           New XElement(xNamespace + "description",
                                                                        New XAttribute("language", "ben"),
                                                                        New XAttribute("title", dt.Rows(i)("progname_ben").ToString),
                                                                        New XAttribute("short_synopsis", dt.Rows(i)("synopsis_ben").ToString),
                                                                        New XAttribute("extended_synopsis", dt.Rows(i)("synopsis_").ToString)
                                                                        ),
                                                           New XElement(xNamespace + "content",
                                                                        New XAttribute("nibble1", dt.Rows(i)("channelnibble").ToString),
                                                                        New XAttribute("nibble2", dt.Rows(i)("nibble1").ToString))
                                                           )
                    siEventSNode.Add(siEventNode)
                End If
            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function
#End Region

#End Region

#Region "SGI"
    Public Function gen_SGI(ByVal dt As DataTable, vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim strB As New StringBuilder
        If dt.Rows.Count > 0 Then
            Dim dtPrevDate As Date = Nothing
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strProgram As String = dt.Rows(i)("progname")
                Dim strSynopsis As String = dt.Rows(i)("synopsis")
                Dim dtCurrDate As Date = dt.Rows(i)("startdate")
                Dim dtCurrTime As Date = dt.Rows(i)("starttime")
                If dtCurrDate.Date >= vStartDate.Date And dtCurrDate.Date <= vEndDate.Date Then

                    If IsNothing(dtPrevDate) Or dtPrevDate <> dtCurrDate Then
                        If i > 0 Then
                            strB.AppendLine("")
                        End If
                        strB.AppendLine("5~0530~~~")
                        strB.AppendLine("1~Service " & vServiceID & "~" & dtCurrDate.ToString("ddMMyyyy") & "~00000000~24000000~eng~0~0")
                        'strB.AppendLine("")
                        If i > 0 Then
                            If dtCurrTime.ToString("HH:mm") <> "00:00" Then

                                strB.AppendLine("2~" & dtCurrDate.ToString("ddMMyyyy") & "~" & "00000000" & "~" & dtCurrTime.ToString("HHmm") & "0000" & "~" & strProgram & "~~eng~0~~0~0~0~0")
                            End If
                        End If
                    End If
                    strB.AppendLine("2~" & dtCurrDate.ToString("ddMMyyyy") & "~" & dtCurrTime.ToString("HHmm") & "0000" & "~" & Convert.ToDateTime(dt.Rows(i)("durationtime")).ToString("HHmm") & "0000" & "~" & strProgram & "~~eng~0~~0~0~0~0")
                    '2~23012019~00000000~00200000~Crime Hour~~eng~0~~0~0~0~0
                    'strB.AppendLine(dtCurrTime.ToString("HH:mm") & " " & strProgram & vbCrLf & IIf(vBoolSynopsis = True, strSynopsis, ""))
                    dtPrevDate = dtCurrDate
                End If
            Next
        End If
        Return strB.ToString

    End Function
#End Region

#Region "TXT"
    Public Function gen_TXT1(ByVal dt As DataTable, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean, vTZ As Integer) As String
        Dim strB As New StringBuilder
        If dt.Rows.Count > 0 Then
            Dim dtPrevDate As Date = Nothing
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strProgram As String = dt.Rows(i)("progname")
                Dim strSynopsis As String = dt.Rows(i)("synopsis")
                Dim dtCurrDate As Date = Convert.ToDateTime(dt.Rows(i)("startdate").ToString).AddMinutes(vTZ)
                Dim dtCurrTime As Date = Convert.ToDateTime(dt.Rows(i)("starttime").ToString).AddMinutes(vTZ)
                If dtCurrDate.Date >= vStartDate.Date And dtCurrDate.Date <= vEndDate.Date Then

                    If IsNothing(dtPrevDate) Or dtPrevDate <> dtCurrDate Then
                        If i > 0 Then
                            strB.AppendLine("")
                        End If
                        strB.AppendLine(dtCurrDate.ToString("yyyy'/'MM'/'dd"))
                        strB.AppendLine("")
                        If i > 0 Then
                            If dtCurrTime.ToString("HH:mm") <> "00:00" Then
                                'strB.AppendLine("00:00{[()]}" & dt.Rows(i - 1)("progname") & "{[()]}{[()]}{[()]}[start]" & dt.Rows(i - 1)("synopsis") & "[end]")
                                strB.AppendLine("00:00 " & dt.Rows(i - 1)("progname") & vbCrLf & IIf(vBoolSynopsis = True, dt.Rows(i - 1)("synopsis").ToString, ""))
                            End If
                        End If
                    End If
                    strB.AppendLine(dtCurrTime.ToString("HH:mm") & " " & strProgram & vbCrLf & IIf(vBoolSynopsis = True, strSynopsis, ""))
                    dtPrevDate = dtCurrDate
                End If
            Next
        End If
        Return strB.ToString

    End Function

    Public Function gen_TXT2(ByVal dt As DataTable, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim strB As New StringBuilder
        If dt.Rows.Count > 0 Then
            Dim dtPrevDate As Date = Nothing
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strProgram As String = dt.Rows(i)("progname")
                Dim strSynopsis As String = dt.Rows(i)("synopsis")
                Dim dtCurrDate As Date = dt.Rows(i)("startdate")
                Dim dtCurrTime As Date = dt.Rows(i)("starttime")
                If dtCurrDate.Date >= vStartDate.Date And dtCurrDate.Date <= vEndDate.Date Then

                    If IsNothing(dtPrevDate) Or dtPrevDate <> dtCurrDate Then
                        If i > 0 Then
                            strB.AppendLine("")
                        End If
                        strB.AppendLine(dtCurrDate.ToString("yy'/'MM'/'dd"))

                        If i > 0 Then
                            If dtCurrTime.ToString("HH:mm:ss") <> "00:00:00" Then
                                strB.AppendLine("00:00:00" & vbTab & dt.Rows(i - 1)("progname") & vbTab & IIf(vBoolSynopsis = True, dt.Rows(i - 1)("synopsis").ToString, ""))
                            End If
                        End If
                    End If

                    strB.AppendLine(dtCurrTime.ToString("HH:mm:ss") & vbTab & strProgram & vbTab & IIf(vBoolSynopsis = True, strSynopsis, ""))
                    dtPrevDate = dtCurrDate
                End If
            Next
        End If
        Return strB.ToString

    End Function

    Public Function gen_TXT3_BCDATE(ByVal dt As DataTable, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim strB As New StringBuilder
        If dt.Rows.Count > 0 Then
            Dim dtPrevDate As Date '= "1900-01-01"
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strProgram As String = dt.Rows(i)("progname")
                Dim strSynopsis As String = dt.Rows(i)("synopsis")
                Dim dtCurrDate As Date = dt.Rows(i)("startdate")
                Dim dtCurrTime As Date = dt.Rows(i)("starttime")
                If dtCurrDate.Date >= vStartDate.Date And dtCurrDate.Date <= vEndDate.Date Then

                    If Not (dtPrevDate.Year = 1) And dtPrevDate <> dtCurrDate Then
                        strB.AppendLine("00:00:00 END")
                    End If
                    If IsNothing(dtPrevDate) Or dtPrevDate <> dtCurrDate Then

                        strB.AppendLine("BCDATE  " & dtCurrDate.ToString("yy'/'MM'/'dd"))

                        If i > 0 Then
                            If dtCurrTime.ToString("HH:mm:ss") <> "00:00:00" Then
                                strB.AppendLine("00:00:00 " & dt.Rows(i - 1)("progname") & vbCrLf & "         SHORTDESC  " & IIf(vBoolSynopsis = True, dt.Rows(i - 1)("synopsis").ToString, ""))
                            End If
                        End If
                    End If
                    strB.AppendLine(dtCurrTime.ToString("HH:mm:ss") & " " & strProgram & vbCrLf & "         SHORTDESC  " & IIf(vBoolSynopsis = True, strSynopsis, ""))

                    dtPrevDate = dtCurrDate
                End If

            Next
            strB.AppendLine("00:00:00 END")
        End If
        Return strB.ToString

    End Function

    Public Function gen_TXT4_IPTV(ByVal dt As DataTable, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim strB As New StringBuilder
        If dt.Rows.Count > 0 Then
            Dim dtPrevDate As Date '= "1900-01-01"
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strProgram As String = dt.Rows(i)("progname").ToString
                Dim strSynopsis As String = dt.Rows(i)("synopsis").ToString
                Dim dtCurrDate As Date = dt.Rows(i)("startdate").ToString
                Dim dtStartTime As Date = dt.Rows(i)("starttime").ToString
                Dim dtEndTime As Date = dt.Rows(i)("endtime").ToString
                If dtCurrDate.Date >= vStartDate.Date And dtCurrDate.Date <= vEndDate.Date Then

                    If IsNothing(dtPrevDate) Or dtPrevDate <> dtCurrDate Then
                        strB.AppendLine("")
                        strB.AppendLine("Channel:" & vServiceID)
                        strB.AppendLine("Date:" & dtCurrDate.ToString("yyyyMMdd"))
                    End If
                    strB.AppendLine(dtStartTime.ToString("HHmmss") & "-" & dtEndTime.ToString("HHmmss") & "|" & strProgram & "|" & IIf(vBoolSynopsis = True, strSynopsis, "") & "|")

                    dtPrevDate = dtCurrDate
                End If
            Next
        End If
        Return strB.ToString

    End Function

    Public Function gen_TXT5(ByVal dt As DataTable, ByVal vServiceID As String, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim strB As New StringBuilder
        Dim myCulture As System.Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture

        If dt.Rows.Count > 0 Then
            Dim dtPrevDate As Date '= "1900-01-01"
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strProgram As String = dt.Rows(i)("progname").ToString
                Dim strSynopsis As String = dt.Rows(i)("synopsis").ToString
                Dim dtCurrDate As Date = dt.Rows(i)("startdate").ToString
                Dim dtStartTime As Date = dt.Rows(i)("starttime").ToString
                Dim dtEndTime As Date = dt.Rows(i)("endtime").ToString
                If dtCurrDate.Date >= vStartDate.Date And dtCurrDate.Date <= vEndDate.Date Then
                    Dim dayOfWeek As DayOfWeek = myCulture.Calendar.GetDayOfWeek(dtCurrDate)
                    Dim dayName As String = myCulture.DateTimeFormat.GetDayName(dayOfWeek)
                    If IsNothing(dtPrevDate) Or dtPrevDate <> dtCurrDate Then
                        strB.AppendLine("")
                        strB.AppendLine(dtCurrDate.ToString("yy'/'MM'/'dd") & "   " & dayName)
                        strB.AppendLine("")
                    End If
                    If strProgram.Length > 29 Then
                        strProgram = strProgram.Substring(0, 28) & " "
                    Else
                        strProgram = strProgram.PadRight(29, " ")
                    End If
                    strB.AppendLine(dtStartTime.ToString("HH:mm") & "   " & strProgram & dayName & ";;" & IIf(vBoolSynopsis = True, strSynopsis, ""))

                    dtPrevDate = dtCurrDate
                End If
            Next
        End If
        Return strB.ToString

    End Function

    Public Function gen_OSN_TXT(ByVal dt As DataTable, vServiceId As String, vStartDate As DateTime, vEndDate As DateTime, vtz As Integer, vBoolSynopsis As Boolean) As String
        Dim strB As New StringBuilder
        Dim myCulture As System.Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture
        If IsNothing(vtz) Then
            vtz = 0
        End If
        If dt.Rows.Count > 0 Then
            Dim dtPrevDate As Date '= "1900-01-01"
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strProgram As String

                ' '' '' Code Comment by Sachin on 28 Sept 2020
                ' '' '' Changes for OSN format Disney ME format 
                ' '' '' Dim strProgram As String = dt.Rows(i)("progname").ToString
                ' '' '' Comment End

                ' '' '' Code Added by Sachin on 28 Sept 2020
                If vServiceId = "STM" Then
                    strProgram = dt.Rows(i)("episodictitle").ToString
                    If strProgram.Trim = "" Then
                        strProgram = dt.Rows(i)("progname").ToString
                    End If
                Else
                    strProgram = dt.Rows(i)("progname").ToString
                End If

                If vServiceId = "STW" Then
                    strProgram = dt.Rows(i)("episodictitle").ToString
                End If

                ' '' ''
                Dim strSeasonNum As String = dt.Rows(i)("seasonno").ToString
                Dim strEpisodeNum As String = dt.Rows(i)("episodeno").ToString

                ' '' ''
                Dim strSynopsis As String = dt.Rows(i)("synopsis").ToString

                Dim strRating As String = "PG15"
                Dim strYear As String = ""
                If vServiceId = "STW" Then
                    strYear = dt.Rows(i)("shorttitle").ToString
                Else
                    strYear = dt.Rows(i)("release_year").ToString
                End If

                Dim dtTimeZone As String = dt.Rows(i)("timezone").ToString
                If vServiceId = "STM" Or vServiceId = "STW" Then
                    If (dtTimeZone = "ist") Then
                        dtTimeZone = "gmt"
                    End If
                End If

                Dim dtCurrDate As Date
                Dim dtStartTime As Date

                If vServiceId = "STM" Or vServiceId = "STW" Then
                    dtCurrDate = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).AddMinutes(vtz)
                    dtStartTime = Convert.ToDateTime(dt.Rows(i)("startdatetime")).AddMinutes(vtz).ToString("HH:mm")
                    Dim dtEndTime As String = Convert.ToDateTime(dt.Rows(i)("enddatetime")).AddMinutes(vtz).ToString("HH:mm:ss")

                Else
                    dtCurrDate = dt.Rows(i)("startdatetime").ToString
                    dtStartTime = dt.Rows(i)("startdatetime").ToString
                    Dim dtEndTime As Date = dt.Rows(i)("enddatetime").ToString
                End If

                If dtCurrDate.Date >= vStartDate.Date And dtCurrDate.Date <= vEndDate.Date Then

                    strB.AppendLine(vServiceId & vbTab & dtCurrDate.ToString("dd/MM/yyyy") & vbTab & dtStartTime.ToString("HH:mm") & vbTab & vbTab & _
                                    strProgram & vbTab & strYear & vbTab & vbTab & strRating & vbTab & vbTab & IIf(vBoolSynopsis = True, strSynopsis, "") & _
                                     vbTab & strSeasonNum & vbTab & strEpisodeNum & vbTab & vbTab & vbTab & vbTab & vbTab & "N" & vbTab & "Y" & vbTab & "N" & vbTab & "Y")
                    dtPrevDate = dtCurrDate
                End If
            Next
        End If
        Return strB.ToString

    End Function
#End Region

#Region "PSC"
    Public Function gen_PSC(ByVal dt As DataTable, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim strB As New StringBuilder
        If dt.Rows.Count > 0 Then
            Dim dtPrevDate As Date = Nothing
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strProgram As String = dt.Rows(i)("progname")
                Dim strSynopsis As String = dt.Rows(i)("synopsis")
                Dim dtCurrDate As Date = dt.Rows(i)("startdate")
                Dim dtCurrTime As Date = dt.Rows(i)("starttime")
                If dtCurrDate.Date >= vStartDate.Date And dtCurrDate.Date <= vEndDate.Date Then
                    If IsNothing(dtPrevDate) Or dtPrevDate <> dtCurrDate Then
                        If i > 0 Then
                            strB.AppendLine("")
                        End If
                        strB.AppendLine(dtCurrDate.ToString("yyyy'/'MM'/'dd"))
                        strB.AppendLine("")
                        If i > 0 Then
                            If dtCurrTime.ToString("HH:mm") <> "00:00" Then
                                strB.AppendLine("00:00{[()]}" & dt.Rows(i - 1)("progname") & "{[()]}{[()]}{[()]}[start]" & IIf(vBoolSynopsis = True, dt.Rows(i - 1)("synopsis").ToString, "") & "[end]")
                            End If
                        End If

                    End If
                    'If i = 0 Then
                    '    If dtCurrTime.ToString("HH:mm") <> "00:00" Then
                    '        strB.AppendLine("00:00{[()]}" & strProgram & "{[()]}{[()]}{[()]}[start]" & strSynopsis & "[end]")
                    '    Else
                    '        strB.AppendLine(dtCurrTime.ToString("HH:mm") & "{[()]}" & strProgram & "{[()]}{[()]}{[()]}[start]" & strSynopsis & "[end]")
                    '    End If
                    'Else
                    strB.AppendLine(dtCurrTime.ToString("HH:mm") & "{[()]}" & strProgram & "{[()]}{[()]}{[()]}[start]" & IIf(vBoolSynopsis = True, strSynopsis, "") & "[end]")
                    'End If

                    dtPrevDate = dtCurrDate
                End If
            Next
        End If
        Return strB.ToString

    End Function
#End Region

#Region "SDF"
    Public Function gen_SDF(ByVal dt As DataTable, vServiceid As Integer, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim strB As New StringBuilder
        If dt.Rows.Count > 0 Then
            strB.AppendLine("SOF")
            strB.AppendLine("VERSION=2;")
            strB.AppendLine("TIMEOFFSET=+5.50;")
            strB.AppendLine("Service=" & vServiceid & ";")

            Dim dtPrevDate As Date = Nothing
            Dim j As Integer = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strProgram As String = dt.Rows(i)("progname").ToString
                Dim strSynopsis As String = dt.Rows(i)("synopsis").ToString
                Dim dtCurrDate As Date = dt.Rows(i)("startdate").ToString
                Dim dtCurrTime As Date = dt.Rows(i)("starttime").ToString
                Dim strDuration As String = dt.Rows(i)("durationtime").ToString
                If dtPrevDate <> dtCurrDate Then
                    j = 1
                End If
                If dtCurrDate.Date >= vStartDate.Date And dtCurrDate.Date <= vEndDate.Date Then
                    'j
                    strB.AppendLine("SME")
                    strB.AppendLine("EventID=" & vServiceid.ToString("0000") & dtCurrDate.ToString("ddMM") & "A" & j.ToString("0000000") & ";")
                    'SET @eventid = dbo.lpad(@SID, 4, '0') + dbo.lpad(cast((datepart(dd, @epgdate)) AS VARCHAR(2)), 2, '0') + dbo.lpad(cast((datepart(mm, @epgdate)) AS VARCHAR(2)), 2, '0') + 'A' + dbo.lpad(cast(@eventidcounter AS VARCHAR(7)), 7, '0')
                    strB.AppendLine("Start=" & dtCurrDate.ToString("yyyy'/'MM'/'dd") & "," & dtCurrTime.ToString("HH:mm:ss") & ";")
                    strB.AppendLine("SEPG")
                    strB.AppendLine("DURATION=" & strDuration & ";")
                    strB.AppendLine("EPG=HIN,""" & strProgram & """,""" & IIf(vBoolSynopsis = True, strSynopsis, "") & ""","""";")
                    strB.AppendLine("THEME=0;")
                    strB.AppendLine("RATING=IND,0;")
                    strB.AppendLine("EEPG")
                    strB.AppendLine("SCA")
                    strB.AppendLine("SACSET")
                    strB.AppendLine("PRODUCT=<DEFAULT>;")
                    strB.AppendLine("BLOCKOUT=<DEFAULT>;")
                    strB.AppendLine("EACSET")
                    strB.AppendLine("ECA")
                    strB.AppendLine("EME")
                    dtPrevDate = dtCurrDate
                    j = j + 1
                End If
            Next
            strB.AppendLine("EOF")
        End If
        Return strB.ToString

    End Function
#End Region

#Region "CSV"
    Public Function gen_Single_CSV(ByVal dt As DataTable, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean, vBoolChannelList As Boolean) As String
        Dim strB As New StringBuilder
        Dim strCurrChannel As String = "", strLastChannel As String = ""
        If vBoolChannelList Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).Date
                strCurrChannel = dt.Rows(i)("serviceid")
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                    If strCurrChannel <> strLastChannel Then
                        If i = 0 Then
                            strB.AppendLine("sid,tsid,channelname,onid")
                        End If
                        strB.AppendLine(dt.Rows(i)("serviceid") & "," & dt.Rows(i)("tsid") & "," & dt.Rows(i)("operatorchannelid") & ",6534")
                        strLastChannel = strCurrChannel
                    End If
                End If
            Next
        Else
            Dim j As Integer
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dCurrDate As DateTime = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).Date
                strCurrChannel = dt.Rows(i)("serviceid")
                If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                    If strCurrChannel <> strLastChannel Then
                        j = 1
                    End If
                    If i = 0 Then
                        strB.AppendLine("tsid,channelname,sid,eventid,eventname,description,date,time,duration")
                    End If
                    strB.AppendLine(dt.Rows(i)("tsid") & "," & dt.Rows(i)("operatorchannelid") & "," & dt.Rows(i)("serviceid") & "," & j & ",""" & dt.Rows(i)("progname").replace(",", "") & """,""" & dt.Rows(i)("synopsis").ToString.Replace(",", "") & """," & Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy-MM-dd") & "," & Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("HH:mm:ss") & "," & dt.Rows(i)("durationtime"))
                    strLastChannel = strCurrChannel
                    j = j + 1

                End If
            Next
        End If

        Return strB.ToString
    End Function

    Public Function gen_Single_CSV2(ByVal dt As DataTable, vStartDate As DateTime, vEndDate As DateTime, vBoolSynopsis As Boolean) As String
        Dim strB As New StringBuilder
        Dim strCurrChannel As String = "", strLastChannel As String = ""

        Dim j As Integer
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim dCurrDate As DateTime = Convert.ToDateTime(dt.Rows(i)("startdatetime").ToString).Date
            strCurrChannel = dt.Rows(i)("serviceid")
            If dCurrDate.Date >= vStartDate.Date And dCurrDate.Date <= vEndDate.Date Then
                If strCurrChannel <> strLastChannel Then
                    j = 1
                End If
                If i = 0 Then
                    strB.AppendLine("channelname,eventid,eventname,description,date,time,duration")
                End If
                strB.AppendLine(dt.Rows(i)("operatorchannelid") & "," & j & ",""" & dt.Rows(i)("progname").replace(",", "") & """,""" & dt.Rows(i)("synopsis").ToString.Replace(",", "") & """," & Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("yyyy-MM-dd") & "," & Convert.ToDateTime(dt.Rows(i)("startdatetime")).ToString("HH:mm:ss") & "," & dt.Rows(i)("durationtime"))
                strLastChannel = strCurrChannel
                j = j + 1

            End If
        Next

        Return strB.ToString
    End Function
#End Region

#End Region

#Region "Client-API"
    Public Function get_ClientChannels_XML(ByVal vOperatorid As Integer, ByVal vAll As Boolean, ByVal dt As DataTable) As String

        Dim xDoc As XDocument = New XDocument()

        Dim BiNode As XElement = New XElement(
            "NDTV-EPG"
        )

        Dim siEventSNode As XElement = New XElement("items")

        Dim j As Integer = 1
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                Dim siEventNode As XElement
                If vAll Then
                    siEventNode = New XElement("channel",
                                               New XElement("name", dt.Rows(i)("serviceid").ToString))
                    siEventSNode.Add(siEventNode)
                Else
                    siEventNode = New XElement("channel",
                                               New XElement("name", dt.Rows(i)("serviceid").ToString),
                                               New XElement("lastupdate", dt.Rows(i)("lastupdate").ToString))
                    siEventSNode.Add(siEventNode)
                End If


                j = j + 1

            Next
        End If
        BiNode.Add(siEventSNode)
        xDoc.Add(BiNode)

        Dim str As String
        Using stringWriter As New Utf8StringWriter()
            Using xmlTextWriter = XmlWriter.Create(stringWriter)

                xDoc.WriteTo(xmlTextWriter)
                xmlTextWriter.Flush()
                str = stringWriter.GetStringBuilder().ToString()
            End Using
        End Using
        Return str
    End Function
#End Region


    Public Class Utf8StringWriter
        Inherits StringWriter

        Public Overrides ReadOnly Property Encoding As Encoding
            Get
                Return Encoding.UTF8
            End Get
        End Property
    End Class

    Public Class GB2312StringWriter
        Inherits StringWriter

        Public Overrides ReadOnly Property Encoding As Encoding
            Get
                Return Encoding.GetEncoding("GB2312")
            End Get
        End Property
    End Class


End Class
