using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DataConnectors
{
    public interface IDataConnector
    {
        bool CheckConn();
        bool CheckData();
        void Initialize();
        void Insert(string query);
        void Update(string query);
        void Delete(string query);
        List<string> Select(string query);
        int Count(string query);
    }
}
