using System;
using System.Collections.Generic;

namespace Bnny.Scripts
{
    public class Cupcake
    {
        public static Cupcake Instance
        {
            get { return _instance != null ? _instance : new Cupcake(); }
        }

        private static Cupcake _instance;
    }
}
