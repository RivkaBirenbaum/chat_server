using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace chatServer.Models
{ 
    
     public class Messages
    {
        public string User { get; set; }
        public DateTime Date { get; set; } 
        public string Message { get; set; } 
    }
    
    public class Room 
    { 
        public string Name { get; set; }
        public List<Messages> Messages { get; set; }
    } 
  
}