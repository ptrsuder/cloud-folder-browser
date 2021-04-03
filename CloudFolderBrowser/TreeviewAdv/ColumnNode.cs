﻿using System;
using Aga.Controls.Tree;

namespace CloudFolderBrowser
{
    public class ColumnNode : Node
    {
        //public bool CheckState = false;
        public bool CheckBoxEnabled = true;
        public string NodeControl1 = "";  // This sould make the DataPropertyName specified in the Node Collection.
        public string NodeControl2 = "";
        public string NodeControl3 = "";
        public string NodeControl4 = "";
        long _size;
        public ColumnNode LinkedNode;
        public long Size
        {
            get => _size;
            set
            {
                _size = value;
                NodeControl4 = Math.Round(_size / 1024000.0, 2) + " MB";
            }
        }
        public string Path = "";

        public ColumnNode(string name, DateTime created, DateTime modified, long size)
        {
            NodeControl1 = name;
            NodeControl2 = created.ToShortDateString();
            NodeControl3 = modified.ToShortDateString();
            //NodeControl4 = Math.Round(size / 1024000.0, 2) + " MB";
            Size = size;
            this.Text = name;
        }

        void SetSize(long size)
        {
            //NodeControl4 = Math.Round(size / 1024000.0, 2) + " MB";
            Size = size;
        }

        public ColumnNode(ColumnNode node)
        {
            NodeControl1 = node.NodeControl1;
            NodeControl2 = node.NodeControl2;
            NodeControl3 = node.NodeControl3;
            NodeControl4 = node.NodeControl4;
            Text = this.NodeControl1;
            Tag = node.Tag;            
        }
    }
}
