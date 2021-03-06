﻿using System;
using Riders.Common.Model;

namespace Riders.BL.ExtensionMethods
{
    public static class RiderExtensions
    {
        private static Random r = new Random();

        public static int GetAvarageScore(this Rider rider)
        {
            var riderScore = rider.Control + rider.Fitness + (99 - rider.Weight);
            var horse = rider.Horse;
            var horseScore = horse.Endurance + horse.Speed + horse.Health;

            return riderScore + horseScore;
        }

        public static int GetRaceScore(this Rider rider)
        {
            var luck = r.NextDouble();

            return (int)(rider.GetAvarageScore() * luck);
        }
    }
}
