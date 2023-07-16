using Isu.Models;

namespace Isu.Entities
{
    public class Group
    {
        public Group(GroupName groupName)
        {
            GroupName = groupName;
            CourseNumber = new CourseNumber(groupName);
        }

        public GroupName GroupName { get; }

        public CourseNumber CourseNumber { get; }

        public int Counter { get; set; }

        public override string ToString()
        {
            return GroupName.ToString();
        }
    }
}