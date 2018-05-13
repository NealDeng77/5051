using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5051.Backend
{
    public class AvatarBackend
    {
        // Get the Datasource to use
        public IAvatarInterface DataSource = AvatarDataSourceMock.Instance;


    }
}