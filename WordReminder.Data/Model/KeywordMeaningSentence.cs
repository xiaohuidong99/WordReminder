using System;
using System.Collections.Generic;
using System.Text;

namespace WordReminder.Data.Model
{
    public class KeywordMeaningSentence
    {
        public int KeywordMeaningSentenceId { get; set; }
        public int KeywordMeaningId { get; set; }
        public string Sentence { get; set; }

        public virtual KeywordMeaning KeywordMeaning { get; set; }
    }
}
