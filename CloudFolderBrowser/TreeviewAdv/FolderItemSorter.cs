using Aga.Controls.Tree;
using System;
using System.Collections;
using System.Windows.Forms;

namespace CloudFolderBrowser
{
	public class FolderItemSorter : IComparer
	{
		private string _mode;
		private SortOrder _order;

		public FolderItemSorter(string mode, SortOrder order)
		{
			_mode = mode;
			_order = order;
		}

		public int Compare(object x, object y)
		{            
            //string g = x.GetType().ToString();
            ColumnNode a = (ColumnNode) x;
            ColumnNode b = (ColumnNode) y;

            if ((a.Tag == null || a.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder") && (b.Tag != null && b.Tag.GetType().ToString() != "CloudFolderBrowser.CloudFolder"))
                return -1;
            if ((b.Tag == null || b.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder") && (a.Tag != null && a.Tag.GetType().ToString() != "CloudFolderBrowser.CloudFolder"))
                return 1;

            int res = 0;

			if (_mode == "Created")
				res = DateTime.Compare(DateTime.Parse(a.NodeControl2), DateTime.Parse(b.NodeControl2));
            if (_mode == "Modified")
                res = DateTime.Compare(DateTime.Parse(a.NodeControl3), DateTime.Parse(b.NodeControl3));
            if (_mode == "Size")
            {
                if (Double.Parse(a.NodeControl4.Replace(" MB", "")) < Double.Parse(b.NodeControl4.Replace(" MB", "")))
                    res = -1;
                else if (Double.Parse(a.NodeControl4.Replace(" MB", "")) > Double.Parse(b.NodeControl4.Replace(" MB", "")))
                    res = 1;
            }
            if (_mode == "Name")
                res = string.Compare(a.NodeControl1, b.NodeControl1);

			if (_order == SortOrder.Ascending)
				return -res;
			else
				return res;
		}

		private string GetData(object x)
		{
			return (x as ColumnNode).NodeControl1;
		}
	}
}
