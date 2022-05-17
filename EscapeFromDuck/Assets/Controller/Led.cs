using System.Collections;
using System.Collections.Generic;

namespace ControllerSpace
{
    public class Led : GameController
    {
        public void statusLed(bool status)
        {
            if (status) write("L4ONN");
            else write("L4OFF");
        }
    }
}

