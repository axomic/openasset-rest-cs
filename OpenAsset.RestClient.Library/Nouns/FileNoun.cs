using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OARestClientLib.NounObject;

namespace OARestClientLib
{
    public class FileNoun : RestAPI<FileObject>
    {

        public FileNoun(string baseURL, string username, string password)
            : base(baseURL, username, password)
        {
            _nounURL += FILES_ENDPOINT;
        }
    }
}
