using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DataConnectors
{
    public class MemoryConnector : IDataConnector
    {
        public bool CheckConn()
        {
            throw new NotImplementedException();
        }

        public bool CheckData()
        {
            throw new NotImplementedException();
        }

        public int Count(string query)
        {
            throw new NotImplementedException();
        }

        public void Delete(string query)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Insert(string query)
        {
            throw new NotImplementedException();
        }

        public List<string> Select(string query)
        {
            throw new NotImplementedException();
        }

        public void Update(string query)
        {
            throw new NotImplementedException();
        }
    }
}
