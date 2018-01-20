using System;
using Loki;
using Loki.Bot;
using log4net;
using Newtonsoft.Json;
using Loki.Common;
using UserControl = System.Windows.Controls.UserControl;
using System.Threading.Tasks;

namespace CraftBot {
    public class CraftBot : IBot, ITaskManagerHolder {
        private Gui _gui;
        private readonly TaskManager _taskManager = new TaskManager();

        private static readonly ILog Log = Logger.GetLoggerInstanceForType();

        public void Start() {
            Log.Debug($"[Start {Name}] The Bot is starting");
        }

        public void Tick() {

        }

        public void Stop() {

        }

        public void Initialize() {
            BotManager.OnBotChanged += BotManagerOnOnBotChanged;
        }

        public void Deinitialize() {
            BotManager.OnBotChanged -= BotManagerOnOnBotChanged;
        }

        private void BotManagerOnOnBotChanged(object sender, BotChangedEventArgs botChangedEventArgs) {
            if (botChangedEventArgs.New == this) {
                ItemEvaluator.Instance = DefaultItemEvaluator.Instance;
            }
        }

        public MessageResult Message(Loki.Bot.Message message) {
            return MessageResult.Processed;
        }

        public async Task<LogicResult> Logic(Logic logic) {
            return await _taskManager.ProvideLogic(TaskGroup.Enabled, RunBehavior.UntilHandled, logic);
        }

        public TaskManager GetTaskManager() {
            return _taskManager;
        }


        public string Name => "CraftBot";
        public string Description => "Bot for crafting items.";
        public string Author => "TheCoder";
        public string Version => ".1";
        public override string ToString() => $"{Name}: {Description}";
        public JsonSettings Settings => GeneralSettings.Instance;
        public UserControl Control => _gui ?? (_gui = new Gui());
    }

    public interface ITaskManagerHolder {
        TaskManager GetTaskManager();
    }
}