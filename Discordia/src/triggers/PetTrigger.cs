﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Discordia.src.triggers
{
    class PetTrigger : Trigger
    {
        public string Activator { get; set; } = "pet";
        public string HelpLine { get; set; } = "**!pet** - mention a dude and I'll pet him";

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("Petting malfunction. Removing petting capabilities.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            try
            {
                SocketUser pettee = Message.MentionedUsers.First();
                SocketUser petter = Message.Author;
                if(pettee.Id == 137267770537541632L)
                {
                    await Message.Channel.SendMessageAsync("I will not pet Floe.");
                    return;
                }
                await Message.Channel.SendMessageAsync("**"+ petter.Username + "** pets **" + pettee.Username +"**");
            }
             catch
            {
                await Message.Channel.SendMessageAsync("Mention someone in your message to pet them.");
            }
        }
    }
}