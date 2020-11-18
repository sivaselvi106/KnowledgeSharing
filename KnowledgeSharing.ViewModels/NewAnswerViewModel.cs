﻿using System;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeSharing.ViewModels
{
    public class NewAnswerViewModel
    {
        [Required]
        public string AnswerText { get; set; }

        [Required]
        public DateTime AnswerDateAndTime { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Required]
        public int VotesCount { get; set; }
    }
}
