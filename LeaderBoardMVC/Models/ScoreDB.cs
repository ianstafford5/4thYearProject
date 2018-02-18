using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LeaderBoardMVC.Models
{
    public class ScoreDB : DbContext
    {
        public DbSet<ScoreModel> Scores { get; set; }
    }

    public class UserDB : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
    }
}