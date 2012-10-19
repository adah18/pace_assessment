<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view_image_modal.aspx.cs" Inherits="PAOnlineAssessment.menus.modal.view_image_modal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width:100%;">
            <tr>
                <td><span class="PageHeader" 
                lang="en-ph">Image Preview</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="img" runat="server" ImageAlign="Middle" Height="300px" 
                        Width="400px" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div>
    
    </div>
    </form>
</body>
</html>
