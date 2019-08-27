using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueViewer.Entities
{
    public class Player
    {
        #region Properties
        public int Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public int ShirtNumber { get; set; }

        public int TeamId { get; set; }
        #endregion

        public Player() { }

        public Player(string name, int age, 
            int number, int teamId)
        {
            FullName = name;
            Age = age;
            ShirtNumber = number;
            TeamId = teamId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Player player))
                return false;
            return this.Id == player.Id;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
