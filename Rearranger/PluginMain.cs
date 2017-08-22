// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using ASCompletion.Context;
using JetBrains.Annotations;
using PluginCore;
using PluginCore.Helpers;
using PluginCore.Managers;
using PluginCore.Utilities;

namespace Rearranger
{
    public class PluginMain : IPlugin, IDisposable
	{
        string settingFilename;
	    ToolStripMenuItem menuItem;

        #region Required Properties

        /// <summary>
        /// Api level of the plugin
        /// </summary>
        public int Api => 1;

        /// <summary>
        /// Name of the plugin
        /// </summary>
        public string Name => nameof(Rearranger);

        /// <summary>
        /// GUID of the plugin
        /// </summary>
        public string Guid => "2bb3088d-b732-4b34-ab21-4603e4741713";

        /// <summary>
        /// Author of the plugin
        /// </summary> 
        public string Author => "SlavaRa";

        /// <summary>
        /// Description of the plugin
        /// </summary>
        public string Description => "Rearrange plugin";

        /// <summary>
        /// Web address for help
        /// </summary>
        public string Help => "https://github.com/SlavaRa/fdplugin-Rearranger";

        /// <summary>
        /// Object that contains the settings
        /// </summary>
        [Browsable(false)]
        public object Settings { get; private set; }
		
		#endregion
		
		#region Required Methods
		
		/// <summary>
		/// Initializes the plugin
		/// </summary>
		public void Initialize()
		{
            InitBasics();
            LoadSettings();
            AddEventHandlers();
            CreateMenuItems();
            UpdateMenuItems();
		    RegisterShortcuts();
        }

	    /// <summary>
		/// Disposes the plugin
		/// </summary>
		public void Dispose() => SaveSettings();

	    /// <summary>
		/// Handles the incoming events
		/// </summary>
		public void HandleEvent(object sender, NotifyEvent e, HandlingPriority priority)
		{
            switch (e.Type)
            {
                case EventType.UIStarted:
                case EventType.FileSwitch:
                    UpdateMenuItems();
                    break;
            }
		}

        #endregion
        
        #region Custom Methods
       
        /// <summary>
        /// Initializes important variables
        /// </summary>
        void InitBasics()
        {
            var dataPath = Path.Combine(PathHelper.DataDir, Name);
            if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
            settingFilename = Path.Combine(dataPath, $"{nameof(Settings)}.fdb");
        }

        /// <summary>
        /// Loads the plugin settings
        /// </summary>
        void LoadSettings()
        {
            Settings = new Settings();
            if (!File.Exists(settingFilename)) SaveSettings();
            else Settings = (Settings) ObjectSerializer.Deserialize(settingFilename, Settings);
        }

	    /// <summary>
	    /// Saves the plugin settings
	    /// </summary>
	    void SaveSettings() => ObjectSerializer.Serialize(settingFilename, Settings);

        /// <summary>
        /// Adds the required event handlers
        /// </summary>
        void AddEventHandlers() => EventManager.AddEventHandler(this, EventType.UIStarted | EventType.FileSwitch);

	    /// <summary>
        /// Creates the required menu items
        /// </summary>
        void CreateMenuItems() => menuItem = CreateMenuItem("Rearranger", "", ShowForm, "CodeRefactor.Rearranger");

	    [NotNull]
	    static ToolStripMenuItem CreateMenuItem([NotNull] string text, [NotNull] string imageData, [NotNull] EventHandler onClick, [NotNull] string shortcutId)
	    {
	        var image = PluginBase.MainForm.FindImage(imageData);
	        var result = new ToolStripMenuItem(text, image, onClick);
	        PluginBase.MainForm.RegisterShortcutItem(shortcutId, result);
	        return result;
	    }

        /// <summary>
        /// Updates the state of the menu items
        /// </summary>
        void UpdateMenuItems()
        {
            var currentModel = ASContext.Context.CurrentModel;
            menuItem.Enabled = currentModel?.Classes?.Count > 0 || currentModel?.Members?.Count > 0;
        }

	    void RegisterShortcuts()
	    {
        }

        #endregion

	    void ShowForm(object sender, EventArgs eventArgs)
	    {
	        
	    }
    }
}