using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components
{
    public class LevelManagerComponent:ComponentBase
    {
        public LevelManagerComponent(LDGame game):base(game)
        {

        }

        protected override void OnGameStateChange(Messages.GameStateChangeMessage obj)
        {
            base.OnGameStateChange(obj);
        }

        public void NextLevel()
        {
            SetLevel(CurrentLevel + 1);
        }

        public  void SetLevel(int nextLevel)
        {
            this.CurrentLevel = nextLevel;
            Messages.Messenger.Default.Publish(new Messages.LevelStartMessage(this, this.CurrentLevel));
        }

        public int CurrentLevel { get; set; }
    }
}
