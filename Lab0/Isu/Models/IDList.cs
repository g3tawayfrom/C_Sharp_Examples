using Isu.Exceptions;

namespace Isu.Models
{
    public class IDList
    {
        private static List<int> idList = new List<int>();

        public static int GenerateID()
        {
            var rnd = new Random();
            int id = rnd.Next(1, 400000);

            if (Validation(id))
            {
                idList.Add(id);
                return id;
            }
            else
            {
                throw new TakenIDException();
            }
        }

        private static bool Validation(int id)
        {
            IEnumerable<int> id_temp = idList.Where(x => x == id);

            if (!id_temp.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
