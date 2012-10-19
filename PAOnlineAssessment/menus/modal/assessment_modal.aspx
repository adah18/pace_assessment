<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assessment_modal.aspx.cs" Inherits="PAOnlineAssessment.menus.modal.assessment_modal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    </head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                <table style="width:100%;">
                    <tr>
                        <td colspan="4">
                            <span class="PageHeader" 
                lang="en-ph">Assessment Tools</span></td>
                    </tr>
                    
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td><center>
                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                CssClass="LargeButtonTemplate" Height="128px" 
                                ImageUrl="~/images/dashboard_icons/folder_full.png" Width="128px" 
                                onclientclick="window.location = '../../assessment/quizcreation_main.aspx';return false;" />
                            <br />
                            <span class="GridPagerButtons" lang="en-ph">Assessment<br /> Maintenance</span></center>
                            </td>
                        <td>
                            <center>
                                <asp:ImageButton ID="imgAdmin" runat="server" 
                                    CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                                    ImageUrl="~/images/dashboard_icons/add_to_folder.png"
                                    onclientclick="window.location = '../../assessment/assessment_admin_add.aspx';return false;"
                                    Width="128px" Visible="false" />
                                <asp:ImageButton ID="imgTeacher" runat="server" 
                                    CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                                    ImageUrl="~/images/dashboard_icons/add_to_folder.png"
                                    onclientclick="window.location = '../../assessment/quizcreation_addupdate.aspx';return false;"
                                    Width="128px" Visible="false" />
                                <br />
                                <span class="GridPagerButtons" lang="en-ph">Create
                                <br />
                                New Assessment</span></center>
                        </td>
                        <td>
                            <center>
                                <asp:ImageButton ID="ImageButton3" runat="server" 
                        CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                        ImageUrl="~/images/dashboard_icons/add_page.png" Width="128px" 
                        
                        
                                    onclientclick="window.location = '../../assessment/questionpool_maintenance_manual.aspx';return false;" />
                                <br />
                                <span class="GridPagerButtons" lang="en-ph">Create Questions<br />
                                Manually</span></center>
                        </td>
                        <td>
                            <center>
                                <asp:ImageButton ID="ImageButton4" runat="server" 
                        CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                        ImageUrl="~/images/dashboard_icons/full_page.png" Width="128px" 
                        onclientclick="window.location='../../assessment/questionpool_maintenance_main.aspx'; return false;" />
                                <br />
                                <span class="GridPagerButtons">Question Pool<br />
                                Maintenance
                                </span></center>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td><center>
                            <asp:ImageButton ID="imgAssessmentTypeMaintenance" runat="server" 
                                CssClass="LargeButtonTemplate" Height="128px" 
                                ImageUrl="~/images/dashboard_icons/page_process.png" Width="128px" 
                                onclientclick="window.location = '../../maintenance/topic_maintenance_main.aspx';return false;" />
                            <br />
                            <span class="GridPagerButtons" lang="en-ph">Topic Maintenance<br />&nbsp;
                            </span></center>
                            </td>
                        <td>
                            <center>
                                <asp:ImageButton ID="imgUploadQuestions" runat="server" 
                                    CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                                    ImageUrl="~/images/dashboard_icons/folder_full_settings.png" 
                                    onclientclick="window.location = '../../assessment/assessmenttype_maintenance_main.aspx';return false;" 
                                    Width="128px" />
                                <br />
                                <span class="GridPagerButtons" lang="en-ph">Type Maintenance<br />
                                &nbsp;</span></center>
                        </td>
                        <td>
                            <center>
                                <asp:ImageButton ID="imgQuestionsPool" runat="server" 
                        CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                        ImageUrl="~/images/dashboard_icons/upload.png" Width="128px" 
                                    onclientclick="window.location = '../../assessment/questionpool_maintenance_upload.aspx';return false;" />
                                <br />
                                <span class="GridPagerButtons" lang="en-ph">Upload Questions<br />
                                &nbsp;</span></center>
                        </td>
                        <td>
                            <center>
                               &nbsp;
                               </center>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <p>
            <span lang="en-ph">&nbsp;</span></p>
    </form>
</body>
</html>
