using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WordReminder.Data.Model
{
    public class Keyword
    {
        public Keyword()
        {
            Meanings = new List<KeywordMeaning>();
        }

        [Display(Name ="Keyword Id")]
        public int KeywordId { get; set; }

        [Required]
        public string Word { get; set; }

        public virtual List<KeywordMeaning> Meanings { get; set; }
    }
}
