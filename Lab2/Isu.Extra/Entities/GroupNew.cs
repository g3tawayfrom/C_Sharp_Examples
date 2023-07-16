using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities
{
    public class GroupNew
    {
        public GroupNew(Group group)
        {
            Group = group;
            Timetable = new Timetable();
        }

        public Group Group { get; }

        public Timetable Timetable { get; }
    }
}
