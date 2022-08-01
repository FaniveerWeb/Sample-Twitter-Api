using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterApiDotNet.Models
{
    public class TweetTag
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int TweetUniqID {get;set;}
        public string UserID {get;set;}
        public string Tag {get; set;}
    }
}