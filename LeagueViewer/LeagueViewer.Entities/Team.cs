using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueViewer.Entities
{
    public class Team
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Foundation { get; set; }

        public string Stadium { get; set; }

        public League League { get; set; }
        #endregion

        public Team() { }

        public Team(string name, DateTime foundation, 
            string stadium, League league) : this()
        {
            Name = name;
            Foundation = foundation;
            Stadium = stadium;
            League = league;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Team team))
                return false;
            return this.Id == team.Id;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
