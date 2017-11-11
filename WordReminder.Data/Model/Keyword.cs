using System;
using System.Collections.Generic;
using System.Text;

namespace WordReminder.Data.Model
{
    public class Keyword
    {
        public Keyword()
        {
            Meanings = new List<KeywordMeaning>();
        }
        public int KeywordId { get; set; }
        public string Word { get; set; }

        public virtual List<KeywordMeaning> Meanings { get; set; }
    }
}
