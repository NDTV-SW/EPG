<%@ Page Title="Error Report" Language="vb" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="false" CodeBehind="ErrorReport1.aspx.vb" Inherits="EPG.ErrorReport1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
    <link rel="stylesheet" href="styles/jqx.base.css" type="text/css" />
    
    <script type="text/javascript" src="Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jqxcore.js"></script>
    <script type="text/javascript" src="Scripts/jqxdata.js"></script> 
    <script type="text/javascript" src="Scripts/jqxbuttons.js"></script>
    <script type="text/javascript" src="Scripts/jqxscrollbar.js"></script>
    <script type="text/javascript" src="Scripts/jqxlistbox.js"></script>
    <script type="text/javascript" src="Scripts/jqxdropdownlist.js"></script>
    <script type="text/javascript" src="Scripts/jqxmenu.js"></script>
    <script type="text/javascript" src="Scripts/jqxgrid.js"></script>
    <script type="text/javascript" src="Scripts/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="Scripts/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="Scripts/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="Scripts/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="Scripts/jqxgrid.selection.js"></script> 
    <script type="text/javascript" src="Scripts/jqxpanel.js"></script>
    <script type="text/javascript" src="Scripts/jqxdata.export.js"></script> 
    <script type="text/javascript" src="Scripts/jqxgrid.export.js"></script>
   

    <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            var source =
            {
                contenttype: 'json',
                datatype: 'json',
                url: 'GetError.ashx',
                pagenum: 1,
                pagesize: 20,
                columns: [
                  { text: 'ErrorDateTime', datafield: 'ErrorDateTime1', width: 180 },
                  { text: 'ErrorPage', datafield: 'ErrorPage', width: 150 },
                  { text: 'ErrorSource', datafield: 'ErrorSource', width: 150 },
                  { text: 'ErrorMessage', datafield: 'ErrorMessage', width: 450, editable:true },
                  { text: 'LoggedinUser', datafield: 'LoggedinUser', width: 100 }
                  
                ],
                pager: function (pagenum, pagesize, oldpagenum) {
                    // callback called when a page or page size is changed.
                },
                updaterow: function (rowid, rowdata) {
                    var data = $.param(rowdata);
                    $.ajax({
                        dataType: 'json',
                        url: 'UpdateError.ashx',
                        data: data,
                        //pagenum: 1,
                        pagesize: 23,
                        pager: function (pagenum, pagesize, oldpagenum) {
                            // callback called when a page or page size is changed.
                        },
                        success: function (data, status, xhr) {
                            // update command is executed.
                        }
                    });
                }
            };

            $("#jqxgrid").bind("pagechanged", function (event) {
                var args = event.args;
                var pagenumber = args.pagenum;
                var pagesize = args.pagesize;
            });

            $("#jqxgrid").bind("pagesizechanged", function (event) {
                var args = event.args;
                var pagenumber = args.pagenum;
                var pagesize = args.pagesize;
            });

            
            var pagerrenderer = function () {
            var element = $("<div style='margin-top: 5px; width: 100%; height: 100%;'></div>");
            var paginginfo = $("#jqxgrid").jqxGrid('getpaginginformation');
            for (i = 0; i < paginginfo.pagescount; i++) {
            // add anchor tag with the page number for each page.
            var anchor = $("<a style='padding: 5px;' href='#" + (i) + "'>" + (i) + "</a>");
            anchor.appendTo(element);
            anchor.click(function (event) {
            // go to a page.
            var pagenum = parseInt($(event.target).text());
            $("#jqxgrid").jqxGrid('gotopage', pagenum);
            });
            }
            return element;
            }
            
            var dataAdapter = new $.jqx.dataAdapter(source, {
                downloadComplete: function (data, status, xhr) { },
                loadComplete: function (data) { },
                loadError: function (xhr, status, error) { alert('load error ' + error + ' ' + xhr.statusText + ' ' + status); }
            });
            //$('#jqxgrid').jqxGrid({ pagesizeoptions: ['10', '20', '30'] }); 

            $("#jqxgrid").jqxGrid(
            {
                width: 1100,
                height: 600,
                //editable: true,
                theme: 'ui-redmond',
                //selectionmode: 'multiplerowsextended',
                source: dataAdapter,
                pageable: true,
                sortable: true,
                selectionmode: 'singlecell',
                //editable: true,
                editmode: 'click',
                //pagerrenderer: pagerrenderer,                
                columns: [
                  { text: 'ErrorDateTime', datafield: 'ErrorDateTime1', width: 160 },
                  { text: 'ErrorPage', datafield: 'ErrorPage', width: 160 },
                  { text: 'ErrorSource', datafield: 'ErrorSource', width: 150 },
                  { text: 'ErrorMessage', datafield: 'ErrorMessage', width: 500 },
                  { text: 'LoggedinUser', datafield: 'LoggedinUser', width: 100 }
                ]
            });
        });  
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
        <div id="jqxgrid">
        </div>
    </div>        <br />
    <div>
        <input type="button" onclick="$('#jqxgrid').jqxGrid('exportdata', 'xls', 'jqxGrid');" value="Export to Excel" style="background:LightBlue" />
    </div>
   
</asp:Content>
