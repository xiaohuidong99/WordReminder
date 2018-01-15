using System;
using System.Collections.Generic;
using System.Text;

namespace WordReminder.Data.Model
{
    public class UserWord
    {
        public int UserWordId { get; set; }
        public int UserId { get; set; }
        public int KeywordId { get; set; }
    }
}
