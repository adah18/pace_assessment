using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAOnlineAssessment
{
    public partial class SiteMap : System.Web.UI.UserControl
    {
        public string RootNode, ParentNode, CurrentNode;
        public string RootNodeURL, RootNodeToolTip,
                      ParentNodeURL, ParentNodeToolTip;

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkRoot.Text = RootNode;
            lnkRoot.ToolTip = RootNodeToolTip;
            lnkRoot.NavigateUrl = RootNodeURL;
            lnkParentNode.Text = ParentNode;
            lnkParentNode.ToolTip = ParentNodeToolTip;
            if (ParentNodeURL != string.Empty)
            {
                lnkParentNode.NavigateUrl = ParentNodeURL;
                lnkParentNode.Enabled = true;
            }
            else
            {
                lnkParentNode.NavigateUrl = string.Empty;
                lnkParentNode.Enabled = false;
            }
            lnkCurrentNode.Text = CurrentNode;
        }
    }
}