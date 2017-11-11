using System;
using System.Collections.Generic;
using System.Text;

namespace WordReminder.Data.Model
{
    public class KeywordMeaning
    {
        public KeywordMeaning()
        {
            KeywordMeaningSentences = new List<KeywordMeaningSentence>();
        }
        public int MeaningId { get; set; }
        public int KeywordId { get; set; }
        public string Word { get; set; }
        public KeywordType KeywordType { get; set; }

        
        public virtual Keyword Keyword { get; set; }
        public virtual List<KeywordMeaningSentence> KeywordMeaningSentences { get; set; }
    }
}
