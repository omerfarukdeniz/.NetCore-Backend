﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Adapters.SmsService
{
    public class SmsServiceManager : ISmsService
    {
        public async Task<bool> Send(string password, string text, string cellPhone)
        {
            Thread.Sleep(1000);
            return await Task.FromResult(true);
        }

        public async Task<bool> SendAsist(string text, string cellPhone)
        {
            Thread.Sleep(1000);
            return await Task.FromResult(true);
        }
    }
}
