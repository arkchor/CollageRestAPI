using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageRestAPI
{
    public class ReferencesResolver
    {
        private static readonly Lazy<ReferencesResolver> lazy =
        new Lazy<ReferencesResolver>(() => new ReferencesResolver());
        public static ReferencesResolver Instance { get { return lazy.Value; } }
        private ReferencesResolver(){}
    }
}