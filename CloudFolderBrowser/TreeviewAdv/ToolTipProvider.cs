using Aga.Controls.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexDiskSharp.Models;

namespace CloudFolderBrowser
{
    class ToolTipProvider : IToolTipProvider
    {
        public string GetToolTip(TreeNodeAdv node, Aga.Controls.Tree.NodeControls.NodeControl nodeControl)
        {
            object a = ((Node)(node.Tag)).Tag;
            if (a == null)
                return "";
            if (a.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
                return ((CloudFolder)a).Path;
            else
                return ((CloudFile)a).Path;
        }
    }
}
