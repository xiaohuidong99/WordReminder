using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WordReminder.Data.Model
{
    public class KeywordMeaning
    {
        public KeywordMeaning()
        {
            KeywordMeaningSentences = new List<KeywordMeaningSentence>();
        }

        [Required]
        [Display(Name = "Keyword Meaning Id")]
        public int KeywordMeaningId { get; set; }

        [Required]
        [Display(Name = "Keyword Id")]
        public int KeywordId { get; set; }

        [Required]
        public string Word { get; set; }

        [Display(Name = "Keyword Type")]
        public KeywordType KeywordType { get; set; }

        
        public virtual Keyword Keyword { get; set; }
        public virtual List<KeywordMeaningSentence> KeywordMeaningSentences { get; set; }
    }
}
