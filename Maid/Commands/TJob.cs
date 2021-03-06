﻿using Discord.WebSocket;
using Maid.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maid.Commands
{
    class TJob : ITrigger
    {
        public TriggerMethod Activator { get; set; } = TriggerUtil.ByName("choosemyjob");
        public string HelpLine { get; set; } = "**choosemyjob** - Get a random job to help choose what you want to play.";
        public MaidCore Bot { get; set; }
        public string[] Examples { get; set; } = new string[]
        {
            "!choosemyjob",
            "!choosemyjob crafters gatherers",
            "!choosemyjob ranged",
            "!choosemyjob tanks"
        };

        // Classes
        private readonly string[] Casters = new string[]{
            "RDM",
            "BLM",
            "SMN",
        };
        private readonly string[] Melee = new string[]
        {
            "NIN",
            "SAM",
            "DRG",
            "MNK"
        };
        private readonly string[] Tanks = new string[]
        {
            "DRK",
            "PLD",
            "WAR"
        };
        private readonly string[] Healers = new string[]
        {
            "WHM",
            "SCH",
            "AST"
        };
        private readonly string[] Ranged = new string[]
        {
            "BRD",
            "MCH"
        };

        private readonly string[] Gatherers = new string[]
        {
            "FSH",
            "MIN",
            "BTN"
        };
        private readonly string[] Crafters = new string[]
        {
            "CRP",
            "BSM",
            "ARM",
            "GSM",
            "LTW",
            "WVR",
            "ALC",
            "CUL"
        };

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("Choosemyjob had an unexpected error, Strange.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            string[] specifications = Message.Content.Split(' ').Skip(1).ToArray();
            List<string> classes = new List<string>();

            if (specifications.Length == 0)
            {
                classes.AddRange(Casters);
                classes.AddRange(Healers);
                classes.AddRange(Melee);
                classes.AddRange(Tanks);
            }
            else
            {
                foreach(string spec in specifications)
                {
                    switch (spec.ToLower())
                    {
                        case "crafter":
                        case "crafting":
                        case "craft":
                        case "crafters":
                            classes.AddRange(Crafters);
                            break;

                        case "gatherer":
                        case "gathering":
                        case "gather":
                        case "gatherers":
                            classes.AddRange(Gatherers);
                            break;

                        case "caster":
                        case "casters":
                        case "cast":
                        case "mage":
                        case "mages":
                            classes.AddRange(Casters);
                            break;

                        case "melee":
                        case "mdps":
                        case "physical":
                            classes.AddRange(Melee);
                            break;

                        case "tank":
                        case "tanks":
                            classes.AddRange(Tanks);
                            break;

                        case "ranged":
                        case "ranger":
                        case "range":
                        case "rdps":
                            classes.AddRange(Ranged);
                            break;

                        case "dps":
                            classes.AddRange(Ranged);
                            classes.AddRange(Melee);
                            classes.AddRange(Casters);
                            break;

                        case "heal":
                        case "healer":
                        case "heals":
                            classes.AddRange(Healers);
                            break;

                        default:
                            break;
                    }
                }
            }
            try
            {
                string job = Rng.Choice(classes);
                await Message.Channel.SendMessageAsync("You are to be a: **" + job + "**");
            }
            catch
            {
                await Message.Channel.SendMessageAsync("You specified no valid job categories. Try again with no specifications or actual specifications like 'healer' 'dps' 'caster' 'ranged'");
            }
        }
    }
}
