using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueViewer.Entities
{
    public class League
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Foundation { get; set; }

        public string Organizer { get; set; }
        #endregion

        public League() { }

        public League(string name, DateTime foundation, 
            string organizer) : this()
        {
            Name = name;
            Foundation = foundation;
            Organizer = organizer;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is League league))
                return false;
            return this.Id == league.Id;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
