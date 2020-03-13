using LibForCore.DataMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel
{

    /// <summary>
    /// Basic Reqeust
    /// </summary>
    public class ReqBase
    {
        [DataNames("ID")]
        public int ID { get; set; }
        [DataNames("Name")]
        public string Name { get; set; }
        
        
    }
    
}
