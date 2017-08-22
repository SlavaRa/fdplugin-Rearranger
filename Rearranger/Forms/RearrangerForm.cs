// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Drawing;
using System.Windows.Forms;
using ASCompletion.Context;
using JetBrains.Annotations;
using PluginCore;

namespace Rearranger.Forms
{
    public sealed partial class RearrangerForm : Form
    {
        [NotNull] readonly Settings settings;

        /// <summary>
        /// Initializes a new instance of the Rearranger.Forms.RearrangerForm
        /// </summary>
        /// <param name="settings"></param>
        public RearrangerForm([NotNull] Settings settings)
        {
            this.settings = settings;
            InitializeComponent();
            InitializeTree();
            InitializeTheme();
            RefreshTree();
            input.LostFocus += (sender, args) => input.Focus();
        }

        void InitializeTree()
        {
            tree.ImageList = ASContext.Panel.TreeIcons;
            tree.ItemHeight = tree.ImageList.ImageSize.Height;
        }

        void InitializeTheme()
        {
            input.BackColor = PluginBase.MainForm.GetThemeColor("TextBox.BackColor", SystemColors.Window);
            input.ForeColor = PluginBase.MainForm.GetThemeColor("TextBox.ForeColor", SystemColors.WindowText);
            tree.BackColor = PluginBase.MainForm.GetThemeColor("TreeView.BackColor", SystemColors.Window);
            tree.ForeColor = PluginBase.MainForm.GetThemeColor("TreeView.ForeColor", SystemColors.WindowText);
            BackColor = PluginBase.MainForm.GetThemeColor("TreeView.BackColor", SystemColors.Window);
            ForeColor = PluginBase.MainForm.GetThemeColor("TreeView.ForeColor", SystemColors.WindowText);
        }

        void RefreshTree()
        {
            tree.BeginUpdate();
            tree.Nodes.Clear();
            FillTree(input.Text.Trim());
            tree.ExpandAll();
            tree.EndUpdate();
        }

        void FillTree([NotNull] string search)
        {
        }

        void Navigate()
        {
        }

        #region Event Handlers

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (settings.RearrangerFormSize.Width > MinimumSize.Width) Size = settings.RearrangerFormSize;
            CenterToParent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e) => settings.RearrangerFormSize = Size;

        #endregion
    }
}