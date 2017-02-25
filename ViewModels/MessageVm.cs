using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class MessageVm
    {
        /// <summary>
        /// Gets or sets message text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets message topic
        /// </summary>
        public string Caption { get; set; }
        
        public bool Yes { get; set; }
        public bool No { get; set; }
        public bool Cancel { get; set; }

        public object DialogResult { get; set; }
    }
}
