using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library1.Cor
{
        public class Res<T>
        {
            public bool IsSuccess { get; set; }
            public string? Message { get; set; }
            public List<string>? Errors { get; set; }
            public T? MyProperty { get; set; }

        }
    
}
