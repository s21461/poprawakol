using System.Collections.Generic;

namespace KolokwiumPoprawa.Models.DTO
{
    public class SomeSortOfTeam
    {

        public string TeamName { get; set; }
        public string TeamDescription { get; set; }

        public string OrganizationName { get; set; }

        public IEnumerable<SomeSortOfMember> Members { get; set; }
    }
}
