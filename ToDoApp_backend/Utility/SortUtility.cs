using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.Utility
{
    public class SortUtility
    {
        public static IList<T> Renumbering<T>(IList<T> dataList)
        {
            List<T> result = new List<T>();

            int index = 1;
            foreach(var data in dataList)
            {
                data.GetType().GetProperty("SortNo").SetValue(data, index);
                result.Add(data);
                index++;
            }

            return result;
        }
    }
}
