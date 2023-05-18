﻿using MicroRabbit.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain
{
     public class TransferCommand:Command
    {
        public int From { get;protected set; }
        public int To { get;protected set; }
        public int Amount { get;protected set; }
    }
}
