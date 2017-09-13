<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JobScheduling.Web.Models.ReportM>" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<script runat="server">
    private void Page_Load(object sender, System.EventArgs e)
    {
        ReportViewer1.LocalReport.ReportPath = Server.MapPath(Model.ReportPath);
        ReportViewer1.LocalReport.EnableExternalImages = true;
        if (Model.Parameters != null)
            ReportViewer1.LocalReport.SetParameters(Model.Parameters);
        if (Model.DataSource != null)
            ReportViewer1.LocalReport.DataSources.Add(Model.DataSource);
        ReportViewer1.LocalReport.Refresh();
    }
</script>

<div  style="word-break:break-all;">
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"
            ShowBackButton="False" ShowFindControls="False"
            ShowPageNavigationControls="false" Height="100%"
            AsyncRendering="false">
        </rsweb:ReportViewer>
    </form>
</div>


