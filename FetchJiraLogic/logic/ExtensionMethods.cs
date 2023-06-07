using System.Collections.Generic;
using System.Linq;

namespace FetchJiraLogic
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, int pageSize, int page)
        {
            return en.Skip(page * pageSize).Take(pageSize);
        }
    }
}
