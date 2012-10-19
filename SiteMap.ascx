<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteMap.ascx.cs" Inherits="PAOnlineAssessment.SiteMap" %>
<link href="scripts/styles/Font%20Style.css" rel="stylesheet" type="text/css" />

<body class="ValidationNotice">
    
<asp:HyperLink ID="lnkRoot" runat="server" CssClass="GridPagerButtons" Text=''>Root Node</asp:HyperLink>
<span class="GridPagerButtons">
    <span lang="en-ph">&nbsp;</span><asp:Label ID="Label1" runat="server" Text=">" 
        CssClass="GridPagerButtons"></asp:Label>
    <span lang="en-ph">&nbsp;</span></span><asp:HyperLink ID="lnkParentNode" runat="server" CssClass="GridPagerButtons" Text=''>Parent Node</asp:HyperLink>
    <span class="GridPagerButtons">
    <span lang="en-ph">&nbsp;</span></span><asp:Label ID="Label2" runat="server" 
        Text=">" CssClass="GridPagerButtons"></asp:Label>
    <span class="GridPagerButtons">
    <span lang="en-ph">&nbsp;</span></span><asp:HyperLink ID="lnkCurrentNode" 
        runat="server" CssClass="GridPagerButtons">Current Node</asp:HyperLink>
</body>