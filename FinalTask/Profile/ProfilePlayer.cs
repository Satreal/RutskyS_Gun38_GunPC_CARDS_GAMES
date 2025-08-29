using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask.Profile
{
    public class ProfilePlayer
    {
        public string Name { get; }
        public int Bank { get; set; }
        public ProfilePlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Name = "Player";
            }
            else
            {
                Name = name;

            }
            Bank = 1000;
        }

    }
}
