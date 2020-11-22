using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace chatServer.Models
{
    public class SignalR : Hub
    {

        public SignalR()
        {
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
 
        
    }
}