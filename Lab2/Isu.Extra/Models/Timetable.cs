using Isu.Extra.Exceptions;

namespace Isu.Extra.Models
{
    // davaite sdelaem vid, cho vi etogo ne videli
    public class Timetable
    {
        public Timetable()
        {
            Days = new List<List<Lesson>>();

            MondayEven = new List<Lesson>();
            MondayOdd = new List<Lesson>();

            Days.Add(MondayEven);
            Days.Add(MondayOdd);

            TuesdayEven = new List<Lesson>();
            TuesdayOdd = new List<Lesson>();

            Days.Add(TuesdayEven);
            Days.Add(TuesdayOdd);

            WednesdayEven = new List<Lesson>();
            WednesdayOdd = new List<Lesson>();

            Days.Add(WednesdayEven);
            Days.Add(WednesdayOdd);

            ThursdayEven = new List<Lesson>();
            ThursdayOdd = new List<Lesson>();

            Days.Add(ThursdayEven);
            Days.Add(ThursdayOdd);

            FridayEven = new List<Lesson>();
            FridayOdd = new List<Lesson>();

            Days.Add(FridayEven);
            Days.Add(FridayOdd);

            SaturdayEven = new List<Lesson>();
            SaturdayOdd = new List<Lesson>();

            Days.Add(SaturdayEven);
            Days.Add(SaturdayOdd);
        }

        public List<List<Lesson>> Days { get; }

        public List<Lesson> MondayEven { get; }

        public List<Lesson> MondayOdd { get; }

        public List<Lesson> TuesdayEven { get; }

        public List<Lesson> TuesdayOdd { get; }

        public List<Lesson> WednesdayEven { get; }

        public List<Lesson> WednesdayOdd { get; }

        public List<Lesson> ThursdayEven { get; }

        public List<Lesson> ThursdayOdd { get; }

        public List<Lesson> FridayEven { get; }

        public List<Lesson> FridayOdd { get; }

        public List<Lesson> SaturdayEven { get; }

        public List<Lesson> SaturdayOdd { get; }

        public void AddLesson(Lesson lesson, Day day, Week week)
        {
            switch (week)
            {
                case Week.Even:
                    switch (day)
                    {
                        case Day.Monday:
                            if (MondayEven.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            MondayEven.Add(lesson);
                            break;
                        case Day.Tuesday:
                            if (TuesdayEven.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            TuesdayEven.Add(lesson);
                            break;
                        case Day.Wednesday:
                            if (WednesdayEven.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            WednesdayEven.Add(lesson);
                            break;
                        case Day.Thursday:
                            if (ThursdayEven.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            ThursdayEven.Add(lesson);
                            break;
                        case Day.Friday:
                            if (FridayEven.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            FridayEven.Add(lesson);
                            break;
                        case Day.Saturday:
                            if (SaturdayEven.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            SaturdayEven.Add(lesson);
                            break;
                    }

                    break;
                case Week.Odd:
                    switch (day)
                    {
                        case Day.Monday:
                            if (MondayOdd.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            MondayOdd.Add(lesson);
                            break;
                        case Day.Tuesday:
                            if (TuesdayOdd.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            TuesdayOdd.Add(lesson);
                            break;
                        case Day.Wednesday:
                            if (WednesdayOdd.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            WednesdayOdd.Add(lesson);
                            break;
                        case Day.Thursday:
                            if (ThursdayOdd.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            ThursdayOdd.Add(lesson);
                            break;
                        case Day.Friday:
                            if (FridayOdd.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            FridayOdd.Add(lesson);
                            break;
                        case Day.Saturday:
                            if (SaturdayOdd.Contains(lesson))
                            {
                                throw new AlreadyExistException();
                            }

                            SaturdayOdd.Add(lesson);
                            break;
                    }

                    break;
            }
        }
    }
}
