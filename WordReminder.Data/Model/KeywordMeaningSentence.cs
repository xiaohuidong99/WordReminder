using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WordReminder.Data.Model
{
    public class KeywordMeaningSentence
    {
        [Display(Name = "Keyword Meaning Sentence Id")]
        public int KeywordMeaningSentenceId { get; set; }

        [Display(Name = "Keyword Meaning Id")]
        public int KeywordMeaningId { get; set; }

        [Required]
        public string Sentence { get; set; }

        public virtual KeywordMeaning KeywordMeaning { get; set; }
    }
}
