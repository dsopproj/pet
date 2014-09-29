using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public class GameTime
    {

        // 摘要: 
        //     Creates a new instance of GameTime.
        public GameTime()
        {
        }

        //
        // 摘要: 
        //     Creates a new instance of GameTime.
        //
        // 参数: 
        //   totalGameTime:
        //     The amount of game time since the start of the game.
        //
        //   elapsedGameTime:
        //     The amount of elapsed game time since the last update.
        public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
        {
            TotalGameTime = totalGameTime;
            ElapsedGameTime = elapsedGameTime;
        }

        // 摘要: 
        //     The amount of elapsed game time since the last update.
        public TimeSpan ElapsedGameTime { get; internal set; }

        //
        // 摘要: 
        //     The amount of game time since the start of the game.
        public TimeSpan TotalGameTime { get; internal set; }
    }
}
