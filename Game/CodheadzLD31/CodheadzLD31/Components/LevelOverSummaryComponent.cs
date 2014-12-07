using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodheadzLD31.Components
{
    public class LevelOverSummaryComponent:ComponentBase
    {
        public LevelOverSummaryComponent(LDGame game):base(game)
        {
        
        }

        protected override void OnGameStateChange(Messages.GameStateChangeMessage obj)
        {
            base.OnGameStateChange(obj);
            if(obj.Content == GameStates.GameStates.LevelOver)
            {
                this.Visible = true;
            }
            else 
            {
                this.Visible = false;
            }
        }
    }
}
