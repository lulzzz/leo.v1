using System;

namespace Leo.Core.Commands
{
    public delegate object HandlerFactory(Type messageType);
}