using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities
{
    public class Flow
    {
        public Flow(int flowNumber, int limit, SubjectName subjectName)
        {
            Timetable = new Timetable();
            FlowNumber = flowNumber;
            Limit = limit;
            GroupCounter = 0;
            SubjectName = subjectName;
            GroupExtraList = new List<GroupExtra>();
        }

        public Timetable Timetable { get; }

        public int FlowNumber { get; }

        public int Limit { get; }

        public int Amount { get; private set; }

        public int GroupCounter { get; private set; }

        public SubjectName SubjectName { get; }

        public List<GroupExtra> GroupExtraList { get; }

        public GroupExtra AddGroupExtra(Teacher teacher, int auditorium, int limit)
        {
            if (limit <= 0 || limit > Limit)
            {
                throw new IncorrectInfoException();
            }

            GroupCounter++;
            Amount += limit;
            var groupExtra = new GroupExtra(SubjectName, FlowNumber, GroupCounter, limit, teacher, auditorium);
            GroupExtraList.Add(groupExtra);

            return groupExtra;
        }
    }
}
