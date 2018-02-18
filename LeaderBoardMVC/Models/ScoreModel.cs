using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaderBoardMVC.Models
{
    public class ScoreModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }

    public class FormModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }
    }

    public class UserModel
    {
        public int Email { get; set; }
        public string Password { get; set; }
    }
}