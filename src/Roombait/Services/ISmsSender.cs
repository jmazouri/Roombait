﻿using System.Threading.Tasks;

namespace Roombait.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
