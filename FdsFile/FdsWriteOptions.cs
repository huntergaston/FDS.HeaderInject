using System;
using System.Collections.Generic;
using System.Text;

namespace Fds
{
    [Flags]
    public enum FdsWriteOptions
    {
        WriteNewFile = 1,
        ModifyInPlace = 1 << 1,
        BackupOriginal = 1 << 2

    }
}
