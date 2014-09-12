﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input
{
    public interface InputDevice
    {
        event Action<int> OnSpecialAction;

        MovementState State { get; }
    }
}
