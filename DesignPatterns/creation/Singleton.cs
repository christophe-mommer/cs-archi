using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.creation
{
    class WebServiceHelper
    {
        private static object s_ThreadSafety;
        private static WebServiceHelper s_Instance;

        public static WebServiceHelper Instance
        {
            get
            {
                if(s_Instance == null)
                {
                    lock(s_ThreadSafety)
                    {
                        if(s_ThreadSafety == null)
                        {
                            s_Instance = new WebServiceHelper();
                        }
                    }
                }
                return s_Instance;
            }
        }

        private WebServiceHelper()
        {

        }

    }
}
